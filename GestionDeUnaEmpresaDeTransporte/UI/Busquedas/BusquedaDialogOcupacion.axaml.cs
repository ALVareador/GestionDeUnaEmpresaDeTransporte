// DemoAvalonia (c) 2021 Baltasar MIT License <jbgarcia@uvigo.es>


using System;

namespace GestionDeUnaEmpresaDeTransporte.UI.Busquedas {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    
    
    public partial class BusquedaDialogOcupacion : Window
    {
        
        public BusquedaDialogOcupacion()
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
                    case 0: return "Fecha";
                    case 1: return "Año";

                }

                return null;
            }
        }
        
        public string ValorBusqueda {
            get
            {
                if (CampoBusqueda.Equals("Fecha"))
                {
                    if (this.FindControl<DatePicker>("EdBusqueda").SelectedDate != null)
                    {
                        return this.FindControl<DatePicker>( "EdBusqueda" ).SelectedDate.Value.Date.ToShortDateString();
                    }

                    return "";
                }
                else
                {
                    if (this.FindControl<DatePicker>("EdBusquedaAnho").SelectedDate != null)
                    {
                        return this.FindControl<DatePicker>( "EdBusquedaAnho" ).SelectedDate.Value.Date.Year.ToString();
                    }

                    return "";
                }
            } 
        }

        private void OnClose()
        {
            this.Close();
        }
        

    }
}