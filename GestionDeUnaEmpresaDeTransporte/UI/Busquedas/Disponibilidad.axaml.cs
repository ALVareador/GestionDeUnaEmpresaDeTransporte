using Avalonia.Controls;
namespace ProyectoIndividualDia{
    
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    public partial class Disponibilidad : Window
    {
        public Disponibilidad()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

         
            var btSalir = this.FindControl<Button>( "BtSalir" );


            btSalir.Click += (o, args) => this.OnClose();
            
        }
        
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