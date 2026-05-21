using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Logica
    {
        private readonly FuenteDatos fuenteDatos;

        public Logica(FuenteDatos fuente)
        {
            this.fuenteDatos = fuente;
        }

        public async Task<bool> ProcesarNuevoPedido(string sku, int cantidad, string talla, string color)
        {
            // Validaciones de negocio básicas
            if (string.IsNullOrWhiteSpace(sku) || cantidad <= 0 || string.IsNullOrWhiteSpace(talla) || string.IsNullOrWhiteSpace(color))
            {
                return false;
            }

            // Creamos un objeto anónimo con la estructura exacta (minúsculas) que esperan los suscriptores
            var pedidoObj = new
            {
                sku = sku.Trim(),
                cantidad = cantidad,
                talla = talla.Trim(),
                color = color.Trim()
            };

            // Serializamos el objeto a un string JSON estándar
            string jsonMensaje = JsonSerializer.Serialize(pedidoObj);

            // Enviamos al exchange a través de RabbitMQ usando la Routing Key acordada
            string routingKey = "pedido.nuevo";
            return await this.fuenteDatos.enviarMensaje(routingKey, jsonMensaje);
        }
    }
}