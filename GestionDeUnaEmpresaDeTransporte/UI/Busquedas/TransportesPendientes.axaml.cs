using System;
using Avalonia.Controls;
namespace ProyectoIndividualDia{
    
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    public partial class TransportesPendientes : Window
    {
        public TransportesPendientes()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
         
            var btSalir = this.FindControl<Button>( "BtSalir" );
            var btBuscar = this.FindControl<Button>( "BtBuscar" );
            var textBox = this.FindControl<TextBox>("EdMatricula");
            btSalir.Click += (o, args) => this.OnClose();
            //btBuscar.Click += (o, args) => this.MostrarBusqueda(textBox.Text);
      
        }

         /* private void MostrarBusqueda(string? cadenaBusqueda)
           {
               string devolver = null;
              if (cadenaBusqueda == null || cadenaBusqueda==" ")
             {
                  devolver = Program.primertransporte.IdTransporte1;
               }
               else
              {
                  string matricula = Program.primertransporte.IdTransporte1.Substring(0, 7);
                   if (cadenaBusqueda.Equals(matricula))
                   {
                       devolver= Program.primertransporte.IdTransporte1;
                   }
               }
               var dlg = new MessageBox {
                   Message = devolver
               };
                   
               dlg.ShowDialog( this );
           } */
        private void OnClose()
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
    }
}