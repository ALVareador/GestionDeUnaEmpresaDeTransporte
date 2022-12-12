using System;
using Avalonia.Controls;
namespace ProyectoIndividualDia{
    
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    public partial class MainWindowBusqueda : Window
    {
        public MainWindowBusqueda()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

         
            var btSalir = this.FindControl<Button>( "BtSalir" );
            var BtTransportesPendientes = this.FindControl<Button>( "BtTransportesPendientes" );
            var BtDisponibilidad = this.FindControl<Button>( "BtDisponibilidad" );
            var BtHistoricoCliente = this.FindControl<Button>( "BtHistoricoCliente" );
            var BtReservasPorCamion = this.FindControl<Button>( "BtReservasPorCamion" );
            var BtReservasPorCliente = this.FindControl<Button>( "BtReservasPorCliente" );
            var BtOcupacion = this.FindControl<Button>( "BtOcupacion" );
            
        
            btSalir.Click += (o, args) => this.OnClose();
            
            BtTransportesPendientes.Click += (_, _) => this.OnViewTransportesPendientes();
            BtDisponibilidad.Click += (_, _) => this.OnViewDisponibilidad();
            BtHistoricoCliente.Click += (_, _) => this.OnViewHistoricoCliente();
            BtReservasPorCamion.Click += (_, _) => this.OnViewReservasPorCamion();
            BtReservasPorCliente.Click += (_, _) => this.OnViewReservasPorCliente();
            BtOcupacion.Click += (_, _) => this.OnViewOcupacion();
        }

        private void OnViewOcupacion()
        {
           new Ocupacion().Show();
        }

        private void OnViewReservasPorCliente()
        {
            new ReservasPorCLiente().Show();
        }

        private void OnViewReservasPorCamion()
        {
            new ReservasPorCamion().Show();
        }

        private void OnViewHistoricoCliente()
        {
          new HistoricoCliente().Show();
        }

        private void OnViewDisponibilidad()
        {
            //string salida = comprobarCamion();
          // if (salida == null) salida = "No hay camiones disponibles";
           // var dlg = new MessageBox {
            //    Message = salida
          //  };
                
           // dlg.ShowDialog( this );
        }

       /*public string comprobarCamion()
        {
             string toret = null;
             string temp = null;
             temp = Program.camion1.Matricula + DateTime.Today;
            if (!temp.Equals(Program.primertransporte.IdTransporte1)){  &&
              //  !Program.primertransporte.IdTransporte1.Substring(7).Equals(DateTime.Today)) 
                toret += Program.camion1.Matricula;
            }
            
            return toret;
        }*/

        private void OnViewTransportesPendientes()
        {
            new TransportesPendientes().Show();
        }


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