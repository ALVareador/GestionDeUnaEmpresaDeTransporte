using Avalonia.Controls;
namespace ProyectoIndividualDia{
    
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    public partial class ReservasPorCLiente : Window
    {
        public ReservasPorCLiente()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btSalir = this.FindControl<Button>( "BtSalir" );
            var btBuscar = this.FindControl<Button>( "BtBuscar" );
            var textBox = this.FindControl<TextBox>("EdDni");
            btSalir.Click += (o, args) => this.OnClose();
          //  btBuscar.Click += (o, args) => this.MostrarBusqueda(textBox.Text);
        }

      /*  private void MostrarBusqueda(string? cadenaBusqueda)
        {
            string? devolver = null;
            string transporte = Program.primertransporte.Dni;
            if (cadenaBusqueda == null || cadenaBusqueda==" ")
            {
                devolver = "No se ha introducido el Dni de usuario";
            }
            else
            {
                if (cadenaBusqueda.Equals(transporte))
                {
                    devolver= Program.primertransporte.IdTransporte1;
                }
            }
            var dlg = new MessageBox {
                Message = devolver
            };
                
            dlg.ShowDialog( this );
        }
*/
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnClose()
        {
            this.Close();
        }
    }
}