// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;

namespace GestionDeUnaEmpresaDeTransporte.UI.GestionDeClientes {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class InsertDialog : Window
    {
        
        public InsertDialog()
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

        
        public string Nif {
            get
            {
                if (this.FindControl<TextBox>("EdNif").Text != null)
                {
                    return this.FindControl<TextBox>( "EdNif" ).Text.Trim();
                }

                return "";
            } 
        }
        
        public string Nombre {
            get
            {
                if (this.FindControl<TextBox>("EdNombre").Text != null)
                {
                    return this.FindControl<TextBox>( "EdNombre" ).Text.Trim();
                }

                return "";
            } 
        }
        
        public string Tlf {
            get
            {
                if (this.FindControl<TextBox>("EdTelefono").Text != null)
                {
                    return this.FindControl<TextBox>( "EdTelefono" ).Text.Trim();
                }

                return "";
            } 
        }
        
        public string Mail {
            get
            {
                if (this.FindControl<TextBox>("EdMail").Text != null)
                {
                    return this.FindControl<TextBox>( "EdMail" ).Text.Trim();
                }

                return "";
            } 
        }
        
        public int Postal {
            get
            {
                if (this.FindControl<TextBox>("EdDirPostal").Text != null)
                {
                    return Int32.Parse(this.FindControl<TextBox>( "EdDirPostal" ).Text.Trim()); 
                }

                return 0;
            } 
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