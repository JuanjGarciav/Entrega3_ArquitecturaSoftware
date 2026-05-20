import pika
import json
import pymssql 
import os
from dotenv import load_dotenv

# 0. Cargar las variables de entorno al inicio del archivo
load_dotenv()

# 1. Configuración de Conexiones
CLOUDAMQP_URL = os.environ.get("RABBIT_URL")
COLA = os.environ.get("RABBIT_COLA", "cola.db")

DB_CONFIG = {
    "server": os.environ.get("DB_SERVER", "127.0.0.1"), 
    "port" : int(os.environ.get("DB_PORT", 1433)),
    "user": os.environ.get("DB_USER"),
    "password": os.environ.get("DB_PASSWORD"), 
    "database": os.environ.get("DB_NAME"), 
    "tds_version": "7.3"
}

def guardar_en_db(sku, cantidad, talla, color):
    """Función para insertar los datos campo por campo en SQL Server"""
    try:
        # Conectamos a SQL Server
        conexion = pymssql.connect(**DB_CONFIG)
        cursor = conexion.cursor()
        
        # Query SQL estructurado apuntando de forma explícita a la DB y esquema dbo
        query = "INSERT INTO sistema_pedidos.dbo.pedidos (sku, cantidad, talla, color) VALUES (%s, %d, %s, %s)"
        valores = (sku, int(cantidad), talla, color)
        
        # CORREGIDO: Se usa la variable correcta 'query'
        cursor.execute(query, valores)
        conexion.commit()  # Guardamos los cambios en la BD
        print(f"Pedido guardado en SQL Server -> SKU: {sku}, Cantidad: {cantidad}")
        
    except Exception as e:
        print(f"Error al guardar en SQL Server: {e}")
    finally:
        if 'conexion' in locals():
            cursor.close()
            conexion.close()

def procesar_mensaje(ch, method, properties, body):
    """Función que se ejecuta automáticamente cuando llega un mensaje a RabbitMQ"""
    try:
        # El cuerpo del mensaje llega como bytes, lo decodificamos y pasamos a JSON (diccionario)
        mensaje_string = body.decode('utf-8')
        datos = json.loads(mensaje_string)
        
        print(f"\nMensaje recibido de RabbitMQ: {datos}")
        
        # Extraemos los campos uno a uno para cumplir el requerimiento del ejercicio
        sku = datos.get('sku')
        cantidad = datos.get('cantidad')
        talla = datos.get('talla')
        color = datos.get('color')
        
        # Validamos que no vengan vacíos y mandamos a guardar
        if sku is not None and cantidad is not None and talla and color:
            guardar_en_db(sku, cantidad, talla, color)
        else:
            print("El mensaje no contiene todos los campos requeridos (sku, cantidad, talla, color).")
            
    except json.JSONDecodeError:
        print("Error: El mensaje recibido no es un JSON válido.")
    except Exception as e:
        print(f"Error procesando el mensaje: {e}")

def iniciar_suscriptor():
    """Configuración e inicio del consumidor de RabbitMQ"""
    print("Iniciando Suscriptor de Base de Datos (Python + SQL Server)...")
    
    # Parseamos la URL de CloudAMQP para establecer la conexión
    params = pika.URLParameters(CLOUDAMQP_URL)
    conexion_rabbit = pika.BlockingConnection(params)
    channel = conexion_rabbit.channel()
    
    # Nos aseguramos de que la cola exista en el broker
    channel.queue_declare(queue=COLA, durable=True)
    
    # Configuramos para escuchar la cola y asignamos la función que procesará los mensajes
    channel.basic_consume(queue=COLA, on_message_callback=procesar_mensaje, auto_ack=True)
    
    print(f"Escuchando la cola '{COLA}'.")
    # El script se queda aquí en un bucle infinito esperando mensajes
    channel.start_consuming()

if __name__ == '__main__':
    try:
        iniciar_suscriptor()
    except KeyboardInterrupt:
        print("\nSuscriptor detenido por el usuario.")