// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;

namespace GestionDeUnaEmpresaDeTransporte.UI.GestionDeFlota {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class BusquedaDialogVehiculo : Window
    {
        
        public BusquedaDialogVehiculo()
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
                    case 0:
                        return "Matricula";
                    case 1: return "Marca";
                    case 2: return "Modelo";
                    case 3: return "Consumo KM";
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