using System;
using System.Runtime.InteropServices.ComTypes;
using Avalonia.Media;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;

namespace GestionDeUnaEmpresaDeTransporte.UI.Graficos {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    

    public partial class ActividadGeneral : Window
    {
        public ActividadGeneral()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.Transportes = new RegistroTransportes();
            var fecha = Convert.ToDateTime($"1/1/{DateTime.Today.Year}");
                
            this.Chart = this.FindControl<Chart>( "ChGrf" );
            var opExit = this.FindControl<MenuItem>( "OpExit" );
            var rbBars = this.FindControl<RadioButton>( "RbBars" );
            var rbLine = this.FindControl<RadioButton>( "RbLine" );
            var edThickness = this.FindControl<NumericUpDown>( "EdThickness" );
            var rbMes = this.FindControl<RadioButton>( "RbMes" );
            var rbTotal = this.FindControl<RadioButton>( "RbTotal" );
            var dpDate = this.FindControl<DatePicker>("DpDate");

            dpDate.SelectedDate = new DateTimeOffset(fecha);
            
            opExit.Click += (_, _) => this.Close();
            rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            edThickness.ValueChanged += (_, evt) => this.OnChartThicknessChanged( evt.NewValue );
            rbMes.Checked += (_, _) => this.OnFontsStyleChanged1();
            rbTotal.Checked += (_, _) => this.OnFontsStyleChanged2();
            dpDate.SelectedDateChanged += (_, _) => this.OnDateChanged();
            
            this.Chart.LegendY = "Sells (in thousands)";
            this.Chart.LegendX = fecha.Year.ToString();
            this.Chart.Values = Transportes.getParaAño(fecha).ToArray();
            this.Chart.Labels = Transportes.getFechas(fecha).ToArray();
            
            //new []{ "En", "Fb", "Ma", "Ab", "My", "Jn", "Jl", "Ag", "Sp", "Oc", "Nv", "Dc" };

        }
        
        public ActividadGeneral(RegistroTransportes transportes)
                {
                    InitializeComponent();
        #if DEBUG
                    this.AttachDevTools();
        #endif
        
                    this.Transportes = transportes;
                    var fecha = Convert.ToDateTime($"1/1/{DateTime.Today.Year}");
                        
                    this.Chart = this.FindControl<Chart>( "ChGrf" );
                    var opExit = this.FindControl<MenuItem>( "OpExit" );
                    var rbBars = this.FindControl<RadioButton>( "RbBars" );
                    var rbLine = this.FindControl<RadioButton>( "RbLine" );
                    var edThickness = this.FindControl<NumericUpDown>( "EdThickness" );
                    var rbMes = this.FindControl<RadioButton>( "RbMes" );
                    var rbTotal = this.FindControl<RadioButton>( "RbTotal" );
                    var dpDate = this.FindControl<DatePicker>("DpDate");
        
                    dpDate.SelectedDate = new DateTimeOffset(fecha);
                    
                    opExit.Click += (_, _) => this.Close();
                    rbBars.Checked += (_, _) => this.OnChartFormatChanged();
                    rbLine.Checked += (_, _) => this.OnChartFormatChanged();
                    edThickness.ValueChanged += (_, evt) => this.OnChartThicknessChanged( evt.NewValue );
                    rbMes.Checked += (_, _) => this.OnFontsStyleChanged1();
                    rbTotal.Checked += (_, _) => this.OnFontsStyleChanged2();
                    dpDate.SelectedDateChanged += (_, _) => this.OnDateChanged();
                    
                    this.Chart.LegendY = "Sells (in thousands)";
                    this.Chart.LegendX = fecha.Year.ToString();
                    this.Chart.Values = Transportes.getParaAño(fecha).ToArray();
                    this.Chart.Labels = Transportes.getFechas(fecha).ToArray();
                    
                    //new []{ "En", "Fb", "Ma", "Ab", "My", "Jn", "Jl", "Ag", "Sp", "Oc", "Nv", "Dc" };
        
                }

       

        void OnChartFormatChanged()
        {
            var edThickness = this.FindControl<NumericUpDown>( "EdThickness" );
            
            if ( this.Chart.Type == Chart.ChartType.Bars ) {
                this.Chart.Type = Chart.ChartType.Lines;
                this.Chart.DataPen = new Pen( Brushes.Red, 2 * edThickness.Value );
            } else {
                this.Chart.Type = Chart.ChartType.Bars;
                this.Chart.DataPen = new Pen( Brushes.Navy, 20 * edThickness.Value );
            }
            
            this.Chart.Draw();
        }

        void OnChartThicknessChanged(double thickness)
        {
            if ( this.Chart.Type == Chart.ChartType.Bars ) {
                this.Chart.DataPen = new Pen( this.Chart.DataPen.Brush, 20 * thickness );
            } else {
                this.Chart.DataPen = new Pen( this.Chart.DataPen.Brush, 2 * thickness );
            }
            
            this.Chart.AxisPen = new Pen( this.Chart.AxisPen.Brush, 4 * thickness );
            this.Chart.Draw();
        }

        void OnFontsStyleChanged1()
        {
            var rbMes = this.FindControl<RadioButton>( "RbMes" );
            var dpDate = this.FindControl<DatePicker>("DpDate");

            if (rbMes.IsChecked.Value)
            {
                dpDate.IsEnabled = true;
                this.Chart.Values = Transportes.getParaAño(dpDate.SelectedDate.Value.DateTime).ToArray();
                this.Chart.Labels = Transportes.getFechas(dpDate.SelectedDate.Value.DateTime).ToArray();
                
            }

            this.Chart.Draw();
        }
        
        void OnFontsStyleChanged2()
        {
            var rbTotal = this.FindControl<RadioButton>( "RbTotal" );
            var dpDate = this.FindControl<DatePicker>("DpDate");
            
            if (rbTotal.IsChecked.Value){
                dpDate.IsEnabled = false;
                this.Chart.Values = Transportes.getDesdeComienzo().ToArray();
                this.Chart.Labels = Transportes.getAños().ToArray();
            }
            
            this.Chart.Draw();
        }

        void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        void OnDateChanged()
        {
            var dpDate = this.FindControl<DatePicker>("DpDate");

            this.Chart.Values = Transportes.getParaAño(dpDate.SelectedDate.Value.DateTime.Date);
            this.Chart.Labels = Transportes.getFechas(dpDate.SelectedDate.Value.DateTime.Date);
            this.Chart.Draw();
        }
        
        Chart Chart { get; }
        RegistroTransportes Transportes { get; }
    }
}