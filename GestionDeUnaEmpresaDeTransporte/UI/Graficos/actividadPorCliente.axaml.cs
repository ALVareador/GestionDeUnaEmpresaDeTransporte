using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using Avalonia.Media;
using GestionDeUnaEmpresaDeTransporte.Core.GestionDeClientes;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GestionDeUnaEmpresaDeTransporte.UI.Graficos {
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    

    public partial class ActividadPorCliente : Window
    {
        public ActividadPorCliente()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            this.Transportes = new RegistroTransportes();
            this.Clientes = new RegistroClientes();
            var fecha = Convert.ToDateTime($"1/1/{DateTime.Today.Year}");
                
            this.Chart = this.FindControl<Chart>( "ChGrf" );
            var opExit = this.FindControl<MenuItem>( "OpExit" );
            /*var rbBars = this.FindControl<RadioButton>( "RbBars" );
            var rbLine = this.FindControl<RadioButton>( "RbLine" );
            var edThickness = this.FindControl<NumericUpDown>( "EdThickness" );*/
            var rbMes = this.FindControl<RadioButton>( "RbMes" );
            var rbTotal = this.FindControl<RadioButton>( "RbTotal" );
            var dpDate = this.FindControl<DatePicker>("DpDate");
            var cbUs = this.FindControl<ComboBox>("CbUs");
            /*var hola = this.FindControl<StackPanel>("Hola");
            var c = this.FindControl<Button>("c");
            ComboBox box = new ComboBox();
            box.Items.Add();*/
            List<String> it = new List<string>();
            foreach (var c in this.Clientes.Clientes)
            {
                it.Add(c.Nif);
            }
            cbUs.Items = it;
            cbUs.SelectedIndex = 0;
            dpDate.SelectedDate = new DateTimeOffset(fecha);
            
            opExit.Click += (_, _) => this.Close();
            /*rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            edThickness.ValueChanged += (_, evt) => this.OnChartThicknessChanged( evt.NewValue );*/
            rbMes.Checked += (_, _) => this.OnYear();
            rbTotal.Checked += (_, _) => this.OnAll();
            dpDate.SelectedDateChanged += (_, _) => this.OnDateChanged();
            cbUs.SelectionChanged += (_, _) => this.OnSelectionChanged();
            //c.Click += (_, _) => hola.IsVisible = false;
            
            this.Chart.LegendY = "Sells (in thousands)";
            this.Chart.LegendX = fecha.Year.ToString();
            this.Chart.Values = Transportes.getPorClienteAño(fecha,cbUs.SelectedItem.ToString()).ToArray();
            this.Chart.Labels = Transportes.getFechas(fecha).ToArray();
            
            //new []{ "En", "Fb", "Ma", "Ab", "My", "Jn", "Jl", "Ag", "Sp", "Oc", "Nv", "Dc" };

        }
        
        public ActividadPorCliente(RegistroClientes clientes, RegistroTransportes transportes)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            this.Transportes = transportes;
            this.Clientes = clientes;
            var fecha = Convert.ToDateTime($"1/1/{DateTime.Today.Year}");
                
            this.Chart = this.FindControl<Chart>( "ChGrf" );
            var opExit = this.FindControl<MenuItem>( "OpExit" );
            var rbMes = this.FindControl<RadioButton>( "RbMes" );
            var rbTotal = this.FindControl<RadioButton>( "RbTotal" );
            var dpDate = this.FindControl<DatePicker>("DpDate");
            var cbUs = this.FindControl<ComboBox>("CbUs");
            List<String> it = new List<string>();
            foreach (var c in this.Clientes.Clientes)
            {
                it.Add(c.Nif);
            }
            cbUs.Items = it;
            cbUs.SelectedIndex = 0;
            dpDate.SelectedDate = new DateTimeOffset(fecha);
            
            opExit.Click += (_, _) => this.Close();
            /*rbBars.Checked += (_, _) => this.OnChartFormatChanged();
            rbLine.Checked += (_, _) => this.OnChartFormatChanged();
            edThickness.ValueChanged += (_, evt) => this.OnChartThicknessChanged( evt.NewValue );*/
            rbMes.Checked += (_, _) => this.OnYear();
            rbTotal.Checked += (_, _) => this.OnAll();
            dpDate.SelectedDateChanged += (_, _) => this.OnDateChanged();
            cbUs.SelectionChanged += (_, _) => this.OnSelectionChanged();
            //c.Click += (_, _) => hola.IsVisible = false;
            
            this.Chart.LegendY = "Sells (in thousands)";
            this.Chart.LegendX = fecha.Year.ToString();
            this.Chart.Values = Transportes.getPorClienteAño(fecha,cbUs.SelectedItem.ToString()).ToArray();
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

        void OnYear()
        {
            var rbMes = this.FindControl<RadioButton>( "RbMes" );
            var dpDate = this.FindControl<DatePicker>("DpDate");
            var cbUs = this.FindControl<ComboBox>("CbUs");

            if (rbMes.IsChecked.Value)
            {
                dpDate.IsEnabled = true;
                this.Chart.Values = Transportes.getPorClienteAño(dpDate.SelectedDate.Value.DateTime,
                    cbUs.SelectedItem.ToString()).ToArray();
                this.Chart.LegendX = cbUs.SelectedItem.ToString();
                this.Chart.Labels = Transportes.getFechas(dpDate.SelectedDate.Value.DateTime).ToArray();
                
            }

            this.Chart.Draw();
        }
        
        void OnAll()
        {
            var rbTotal = this.FindControl<RadioButton>( "RbTotal" );
            var dpDate = this.FindControl<DatePicker>("DpDate");
            var cbUs = this.FindControl<ComboBox>("CbUs");
            
            if (rbTotal.IsChecked.Value){
                dpDate.IsEnabled = false;
                this.Chart.Values = Transportes.getDesdeComienzoCliente(cbUs.SelectedItem.ToString()).ToArray();
                this.Chart.Labels = Transportes.getAños().ToArray();
                this.Chart.LegendX = "Años";
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
            var cbUs = this.FindControl<ComboBox>("CbUs");

            this.Chart.Values = Transportes.getPorClienteAño(dpDate.SelectedDate.Value.DateTime,
                cbUs.SelectedItem.ToString()).ToArray();
            this.Chart.Labels = Transportes.getFechas(dpDate.SelectedDate.Value.DateTime).ToArray();
            this.Chart.Draw();
        }

        void OnSelectionChanged()
        {
            var rbMes = this.FindControl<RadioButton>( "RbMes" );
            var dpDate = this.FindControl<DatePicker>("DpDate");
            var cbUs = this.FindControl<ComboBox>("CbUs");

            if (dpDate.IsEnabled)
            {
                dpDate.IsEnabled = true;
                this.Chart.Values = Transportes.getPorClienteAño(dpDate.SelectedDate.Value.DateTime,
                    cbUs.SelectedItem.ToString()).ToArray();
                this.Chart.LegendX = cbUs.SelectedItem.ToString();
                this.Chart.Labels = Transportes.getFechas(dpDate.SelectedDate.Value.DateTime).ToArray();
                
            }
            else
            {
                dpDate.IsEnabled = false;
                this.Chart.Values = Transportes.getDesdeComienzoCliente(cbUs.SelectedItem.ToString()).ToArray();
                this.Chart.Labels = Transportes.getAños().ToArray();
                this.Chart.LegendX = "Años";
            }

            this.Chart.Draw();
        }
        
        Chart Chart { get; }
        RegistroTransportes Transportes { get; }

        RegistroClientes Clientes { get; }
    }
}