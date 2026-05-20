require('dotenv').config(); // 1. Cargar las variables de entorno al inicio del archivo
const client = require('amqplib');
const express = require('express');
const http = require('http');
const { Server } = require('socket.io');
const fs = require('fs');
const path = require('path');

const app = express();
const server = http.createServer(app);
const io = new Server(server);

// Configuración de RabbitMQ protegida mediante variables de entorno
const CLOUDAMQP_URL = process.env.RABBIT_URL; 
const COLA = process.env.RABBIT_COLA || "cola.grafica"; // Usa "cola.grafica" por defecto si falla el .env

// Servir la página web de la interfaz gráfica
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'index.html'));
});

// Función para conectar a RabbitMQ y escuchar mensajes

async function iniciarSuscriptor() {
    try {
        console.log("Iniciando Suscriptor Gráfico (Node.js)...");
        
        const conexion = await client.connect(CLOUDAMQP_URL);
        const channel = await conexion.createChannel();
        
        await channel.assertQueue(COLA, { durable: true });
        
        console.log(`Escuchando la cola '${COLA}'...`);

        channel.consume(COLA, (msg) => {
            if (msg !== null) {
                const mensajeString = msg.content.toString();
                console.log(`\nMensaje recibido de RabbitMQ: ${mensajeString}`);

                try {
                    const datos = JSON.parse(mensajeString);

                    // REQUERIMIENTO ÚNICO: Mostrar en Interfaz Gráfica (Enviar al navegador vía WebSockets)
                    io.emit('nuevoPedido', datos);
                    console.log("Pedido renderizado en la interfaz web (Puerto 3000)");

                    // Confirmar a RabbitMQ que el mensaje fue procesado con éxito
                    channel.ack(msg);

                } catch (error) {
                    console.error("Error al procesar el mensaje:", error.message);
                    channel.ack(msg);
                }
            }
        });

    } catch (error) {
        console.error("Error de conexión en Node.js con RabbitMQ:", error.message);
    }
}
// Permitir cambiar el puerto dinámicamente si el 3000 se encuentra ocupado
const PUERTO = process.env.PORT || 3000;
server.listen(PUERTO, () => {
    console.log(`Interfaz gráfica disponible en: http://localhost:${PUERTO}`);
    iniciarSuscriptor();
});