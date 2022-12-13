// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;

namespace GestionDeUnaEmpresaDeTransporte.UI.Busquedas {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class BusquedaDialogUnico : Window
    {
        
        public BusquedaDialogUnico()
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
        
        public BusquedaDialogUnico(string text): base()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );
            var etqText = this.FindControl<Label>("LabelBusqueda");
            etqText.Content = text;

            btOk.Click += (o, args) => this.OnClose();
            btCancel.Click += (o, args) => this.OnClose();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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