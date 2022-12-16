using Avalonia.Controls;
using Avalonia;
using Avalonia.Markup.Xaml;
using System.Diagnostics;
using System;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;

namespace ProyDIA.UI
{
    public partial class TransporteDlg : Window
    {
                
        
        public TransporteDlg(Transporte t)
            :this()
        {
            var edCliente = this.FindControl<TextBox>("EdCliente");
            var edMatricula = this.FindControl<TextBox>("EdMatricula");
            var edKms = this.FindControl<NumericUpDown>("EdKms");
            var edTipo = this.FindControl<ComboBox>("EdTipo");
            var edFechaContra = this.FindControl<DatePicker>("EdFechaContra");
            var edFechaSal = this.FindControl<DatePicker>("EdFechaSal");
            var edFechaEntre = this.FindControl<DatePicker>("EdFechaEntre");
            var edSueldo = this.FindControl<NumericUpDown>("EdSueldo");
            var edPrecioLt = this.FindControl<NumericUpDown>("EdPrecioLt");
            var edCantLtKm = this.FindControl<NumericUpDown>("EdCantLtKm");

            
            edCliente.Text = t.Cliente;
            edMatricula.Text = t.Matricula;
            edKms.Value = t.Kms;
            edFechaContra.SelectedDate = t.FechaContra;
            edFechaEntre.SelectedDate = t.FechaEntre;
            edFechaSal.SelectedDate = t.FechaSal;
            edSueldo.Value = t.SueldoHora;
            edPrecioLt.Value = t.PrecioLitro;
            edCantLtKm.Value = t.CantLtKms;
        }
        

        public TransporteDlg()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>("BtOk");
            var btCancel = this.FindControl<Button>("BtCancel");
            var edCliente = this.FindControl<TextBox>("EdCliente");
            var edMatricula = this.FindControl<TextBox>("EdMatricula");
            var edKms = this.FindControl<NumericUpDown>("EdKms");
            var edTipo = this.FindControl<ComboBox>("EdTipo");
            var edFechaContra = this.FindControl<DatePicker>("EdFechaContra");
            var edFechaSal = this.FindControl<DatePicker>("EdFechaSal");
            var edFechaEntre = this.FindControl<DatePicker>("EdFechaEntre");
            var edSueldo = this.FindControl<NumericUpDown>("EdSueldo");
            var edPrecioLt = this.FindControl<NumericUpDown>("EdPrecioLt");
            var edCantLtKm = this.FindControl<NumericUpDown>("EdCantLtKm");


            Debug.Assert(btOk != null, "btOk not found in XAML!");
            Debug.Assert(btCancel != null, "btCancel not found in XAML!");
            Debug.Assert(edCliente != null, "edCliente not found in XAML!");
            Debug.Assert(edMatricula != null, "edMatricula not found in XAML!");
            Debug.Assert(edKms != null, "edKms not found in XAML!");
            Debug.Assert(edTipo != null, "edTipo not found in XAML!");
            Debug.Assert(edFechaContra != null, "edFechaContra not found in XAML!");
            Debug.Assert(edFechaSal != null, "edFechaSal not found in XAML!");
            Debug.Assert(edFechaEntre != null, "edFechaEntre not found in XAML!");
            Debug.Assert(edSueldo != null, "edSueldo not found in XAML!");
            Debug.Assert(edPrecioLt != null, "edPrecioLt not found in XAML!");
            Debug.Assert(edCantLtKm != null, "edCantLtKm not found in XAML!");


            btOk.Click += (_, _) => this.OnExit();
            btCancel.Click += (_, _) => this.OnCancelClicked();
            this.Closed += (_, _) => this.OnExit();

            edFechaContra.SelectedDate = DateTime.Now;
            
            this.IsCancelled = false;

        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
        }

        void OnExit()
        {
            this.Close();
        }

        void OnCancelClicked()
        {
            this.IsCancelled = true;
            this.OnExit();
        }

        public string Cliente
        {
            get
            {
                var edCliente = this.FindControl<TextBox>("EdCliente");

                Debug.Assert(edCliente != null, "edCliente not found in XAML!");

                return edCliente.Text.Trim();
            }
        }

        public string Matricula
        {
            get
            {
                var edMatricula = this.FindControl<TextBox>("EdMatricula");

                Debug.Assert(edMatricula != null, "edMatricula not found in XAML!");

                return edMatricula.Text.Trim();
            }
        }

        public double Kms
        {
            get
            {
                var edKms = this.FindControl<NumericUpDown>("EdKms");

                Debug.Assert(edKms != null, "edKms not found in XAML!");
                return (double)edKms.Value;
            }
        }

        public string Tipo
        {
            get
            {
                var edTipo = this.FindControl<ComboBox>("EdTipo").SelectedIndex;
                
                var toret = "No definido";

                Debug.Assert(edTipo != null, "edTipo not found in XAML!");
                
                switch (edTipo)
                {
                    case 0: return "Mudanza";
                    case 1: return "Transporte de mercancias";
                    case 2: return "Transporte de vehiculos";
                }

                return "Mudanza";
            }
        }

        public DateTime FechaContra
        {
            get
            {
                var edFechaContra = this.FindControl<DatePicker>("EdFechaContra");
                Debug.Assert(edFechaContra != null, "edFechaContra not found in XAML!");
                
                var toret = DateTime.Now;

                if (edFechaContra.SelectedDate != null)
                {
                    toret = edFechaContra.SelectedDate.Value.DateTime;
                }
                
                return toret;
            }
        }

        public DateTime FechaSal
        {
            get
            {
                var edFechaSal = this.FindControl<DatePicker>("EdFechaSal");
                Debug.Assert(edFechaSal != null, "edFechaSal not found in XAML!");

                var toret = DateTime.Now;

                if (edFechaSal.SelectedDate != null)
                {
                    toret = edFechaSal.SelectedDate.Value.DateTime;
                }

                return toret;
            }
        }

        public DateTime FechaEntre
        {
            get
            {
                var edFechaEntre = this.FindControl<DatePicker>("EdFechaEntre");
                Debug.Assert(edFechaEntre != null, "edFechaEntre not found in XAML!");

                var toret = DateTime.Now;

                if (edFechaEntre.SelectedDate != null)
                {
                    toret = edFechaEntre.SelectedDate.Value.DateTime;
                }

                return toret;
            }
        }

        public double SueldoHora
        {
            get
            {
                var edSueldo = this.FindControl<NumericUpDown>("EdSueldo");

                Debug.Assert(edSueldo != null, "edSueldo not found in XAML!");

                return (double)edSueldo.Value;
            }
        }

        public double PrecioLitro
        {
            get
            {
                var edPrecioLt = this.FindControl<NumericUpDown>("EdPrecioLt");

                Debug.Assert(edPrecioLt != null, "edPrecioLt not found in XAML!");

                return (double)edPrecioLt.Value;

            }
        }

        public double CantLtKms
        {
            get
            {
                var edCantLtKm = this.FindControl<NumericUpDown>("EdCantLtKm");

                Debug.Assert(edCantLtKm != null, "edCantLtKm not found in XAML!");

                return (double)edCantLtKm.Value;
            }
        }


        public bool IsCancelled
        {
            get;
            private set;
        }

        const double DefaultKms = 500;
    }
}
