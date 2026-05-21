using CapaNegocio;
using RabbitMQ.Client;
using System.Configuration;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class ConexionRabbit : FuenteDatos
    {
        public IChannel channel;
        public static string exchange = ConfigurationManager.AppSettings["Exchange"]!;

        public static async Task<ConexionRabbit> crearConexion()
        {
            string user = ConfigurationManager.AppSettings["User"]!;
            string vhost = ConfigurationManager.AppSettings["VHost"]!;
            string password = ConfigurationManager.AppSettings["Password"]!;
            string url = ConfigurationManager.AppSettings["Url"]!;
            int port = int.Parse(ConfigurationManager.AppSettings["Port"]!);

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = url,
                UserName = user,
                Password = password,
                VirtualHost = vhost,
                Port = port
            };

            IConnection connection = await factory.CreateConnectionAsync();

            ConexionRabbit conexion = new ConexionRabbit();
            conexion.channel = await connection.CreateChannelAsync();
            return conexion;
        }

        // Adaptado a Async para cumplir con la nueva versión de la librería
        public async Task<bool> enviarMensaje(string routingKey, string mensaje)
        {
            try
            {
                byte[] body = Encoding.UTF8.GetBytes(mensaje);
                // Se agrega el await correspondiente
                await channel.BasicPublishAsync(exchange: exchange, routingKey: routingKey, body: body);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}