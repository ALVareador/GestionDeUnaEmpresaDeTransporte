// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;
using System.Security.Cryptography.X509Certificates;

namespace GestionDeUnaEmpresaDeTransporte.UI.GestionDeClientes {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class EditDialog : Window
    {
        public EditDialog()
        {
            
        }
        public EditDialog(string nif,string name,string tlf,string mail,int postal):this()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );
            this.FindControl<TextBox>("EdNif").Text = nif;
            this.FindControl<TextBox>("EdNombre").Text = name;
            this.FindControl<TextBox>("EdTelefono").Text = tlf;
            this.FindControl<TextBox>("EdMail").Text = mail;
            this.FindControl<TextBox>("EdDirPostal").Text = postal.ToString();

            btOk.Click += (o, args) => this.OnClose();
            btCancel.Click += (o, args) => this.OnClose();
        }

        
        public string Nif {
            get => this.FindControl<TextBox>( "EdNif" ).Text.Trim();
        }
        
        public string Nombre {
            get => this.FindControl<TextBox>( "EdNombre" ).Text.Trim();
        }
        
        public string Tlf {
            get => this.FindControl<TextBox>( "EdTelefono" ).Text.Trim();
        }
        
        public string Mail {
            get => this.FindControl<TextBox>( "EdMail" ).Text.Trim();
        }
        
        public int Postal {
            get => Int32.Parse(this.FindControl<TextBox>( "EdDirPostal" ).Text.Trim()); 
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