using System;
using System.Windows.Forms;
using CapaAccesoDatos;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private Logica _logica;
        private bool _conexionLista = false;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Inicializamos la conexión asíncrona con los AppSettings del App.config
                ConexionRabbit conexionRabbit = await ConexionRabbit.crearConexion();
                _logica = new Logica(conexionRabbit);
                _conexionLista = true;
                lblEstado.Text = "Conectado exitosamente a CloudAMQP";
                lblEstado.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con el bróker: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblEstado.Text = "Desconectado del bróker";
                lblEstado.ForeColor = System.Drawing.Color.Red;
            }
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!_conexionLista)
            {
                MessageBox.Show("Aún no se ha establecido la conexión con RabbitMQ.", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extraemos los valores de la interfaz gráfica
            string sku = txtSku.Text;
            int cantidad = Convert.ToInt32(nudCantidad.Value);
            string talla = txtTalla.Text;
            string color = txtColor.Text;

            // Enviamos los datos a la capa de negocio para ser procesados y transformados a JSON
            bool exito = await _logica.ProcesarNuevoPedido(sku, cantidad, talla, color);

            if (exito)
            {
                MessageBox.Show("¡Pedido publicado con éxito en RabbitMQ!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Opcional: Limpiar campos
                txtSku.Clear();
                nudCantidad.Value = 1;
                txtTalla.Clear();
                txtColor.Clear();
            }
            else
            {
                MessageBox.Show("Error al procesar el pedido. Verifica que todos los campos estén llenos.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}