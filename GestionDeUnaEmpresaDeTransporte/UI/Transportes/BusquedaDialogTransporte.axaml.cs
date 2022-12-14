// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;

namespace GestionDeUnaEmpresaDeTransporte.UI.Transportes {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class BusquedaDialogTransporte : Window
    {
        
        public BusquedaDialogTransporte()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );

            btOk.Click += (o, args) => this.OnClose();
            btCancel.Click += (o, args) => this.OnClose();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public string CampoBusqueda
        {
            get
            {
                var selectedIndex = this.FindControl<ComboBox>("valorBusqueda").SelectedIndex;
                switch (selectedIndex)
                {
                    case 0: return "Matricula";
                    case 1: return "Tipo transporte";
                    case 2: return "DNI cliente";
                    case 3: return "Fecha contrato";
                    case 4: return "Fecha salida";
                    case 5: return "Fecha entrega";
                    case 6: return "Importe por dia";
                    case 7: return "Importe por kilometro";
                    case 8: return "IVA aplicado";
                }

                return null;
            }
        }
        
        public string ValorBusqueda {
            get
            {
                if (this.FindControl<TextBox>("EdBusqueda").Text != null)
                {
                    return this.FindControl<TextBox>("EdBusqueda").Text.Trim();
                }

                return "";
            } 
        }

        private void OnClose()
        {
            this.Close();
        }
        

    }
}