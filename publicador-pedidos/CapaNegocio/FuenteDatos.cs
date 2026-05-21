using System.Threading.Tasks;

namespace CapaNegocio
{
    public interface FuenteDatos
    {
        // Contrato actualizado a Task para soporte asíncrono
        public Task<bool> enviarMensaje(string routingKey, string mensaje);
    }
}