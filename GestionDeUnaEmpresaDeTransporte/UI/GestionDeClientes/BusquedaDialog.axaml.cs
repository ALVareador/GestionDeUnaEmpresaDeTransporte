// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;

namespace GestionDeUnaEmpresaDeTransporte.UI.GestionDeClientes {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class BusquedaDialog : Window
    {
        
        public BusquedaDialog()
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
                        return "NIF";
                    case 1: return "Nombre";
                    case 2: return "Tlf";
                    case 3: return "Mail";
                    case 4: return "Postal";
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