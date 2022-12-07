using System;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControlFlota.Core;
using GestionDeUnaEmpresaDeTransporte.UI.Graficos;

namespace GestionDeUnaEmpresaDeTransporte.Graficos;

/// <summary>
/// Muestra el número de camiones que cumple con una comodidad.
/// </summary>
public partial class comodidadPorCamion : Window
{

    /// <summary>
    /// Crea una grafica vacia.
    /// </summary>
    public comodidadPorCamion() : this(FlotaDefault)
    {
            
    }
    
    /// <summary>
    /// Crea una grafica a partir de una lista de la flota.
    /// </summary>
    /// <param name="f">Lista de la flota</param>
    public comodidadPorCamion(FleetControl f)
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.Chart = this.FindControl<Chart>( "ChGrf" );
        var opExit = this.FindControl<MenuItem>( "OpExit" );
        var opAbout = this.FindControl<MenuItem>( "OpAbout" );
        var opFecha = this.FindControl<DatePicker>("DpFecha");
        opFecha.SelectedDate = new DateTimeOffset(DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy")));
        var opOpcion = this.FindControl<ComboBox>("CbOpcion");
        opOpcion.SelectedIndex = 0;

        this.Chart.LegendY = "Nº de camiones";
        this.Chart.LegendX = "Comodidades"; 
        this.Chart.Values = valoresComodidades(f); 
        this.Chart.Labels = new []{ "Wifi", "Bluetooth", "Aire acondicionado", "Litera de descanso", "TV", "Total" };
        this.Chart.Type = Chart.ChartType.Bars; 
        this.Chart.DataPen = new Pen( Brushes.IndianRed, 20 );
        
        opExit.Click += (_, _) => this.Close(); 
        opFecha.SelectedDateChanged += (_, _) => this.FiltrarPorFecha(opFecha.SelectedDate, f, opcionAdquision);
        opOpcion.SelectionChanged += (_, _) => this.CambiarOpcion(opOpcion.SelectedIndex, opFecha.SelectedDate, f);
    }

    private void CambiarOpcion(int opOpcionSelectedIndex, DateTimeOffset? opFechaSelectedDate, FleetControl flota)
    {
        if (opOpcionSelectedIndex == 0)
        {
            opcionAdquision = true;
        }
        else
        {
            opcionAdquision = false;
        }
        this.FiltrarPorFecha(opFechaSelectedDate, flota, opcionAdquision);
    }

    /// <summary>
    /// Modifica la grafica para mostrar solo los camiones del mes seleccionado.
    /// </summary>
    /// <param name="mes">Mes del que queremos ver los camiones.</param>
    /// <param name="f">Lista de la flota.</param>
    /// <param name="opcionA">Opcion seleccionada para mostrar los camiones por fecha de adquisicion o por fecha de fabricacion.</param>
    private void FiltrarPorFecha(DateTimeOffset? mes, FleetControl f, bool opcionA) 
    { 
        FleetControl flotaFiltrada = new FleetControl();
        
        if (mes.Value.Month == 0)
        {
            flotaFiltrada = f;
        }
        else
        { 
            foreach (var vehicle in f)
            {
                if (opcionA == true)
                {
                    if (vehicle.adqDate.Month == mes.Value.Month && vehicle.adqDate.Year == mes.Value.Year)
                    {
                        flotaFiltrada.Add(vehicle);
                    }
                }
                else
                {
                    if (vehicle.fabrDate.Month == mes.Value.Month && vehicle.fabrDate.Year == mes.Value.Year)
                    {
                        flotaFiltrada.Add(vehicle);
                    }
                }
            }
        }
            
        //PintarGrafica(mes);
        this.Chart.Values = valoresComodidades(flotaFiltrada);
        this.Chart.Draw();
    }

    /// <summary>
    /// Crea un array con el numero de comodidades que la flota de camiones posee.
    /// </summary>
    /// <param name="flota">Lista de la flota</param>
    /// <returns>Array con el numero de comodidades.</returns>
    private IEnumerable<int> valoresComodidades(FleetControl flota) 
    {
        
        int[] comodidades = { 0, 0, 0, 0, 0, 0}; 
        if (flota != null)
        {
            foreach (var vehicle in flota)
            {
                if (vehicle.wifi)
                {
                    comodidades[0] = comodidades[0] + 1;
                }

                if (vehicle.bluetooth)
                {
                    comodidades[1] = comodidades[1] + 1;
                }

                if (vehicle.ac)
                {
                    comodidades[2] = comodidades[2] + 1;
                }

                if (vehicle.bed)
                {
                    comodidades[3] = comodidades[3] + 1;
                }

                if (vehicle.tv)
                {
                    comodidades[4] = comodidades[4] + 1;
                }

                comodidades[5] = comodidades[5] + 1;
            }
        }

        return comodidades;
    }

    /// <summary>
    /// Cambia el color de la grafica.
    /// </summary>
    /// <param name="mes">Mes seleccionado.</param>
    private void PintarGrafica(short mes)
    {
        switch (mes)
        {
            case 00:
                this.Chart.DataPen = new Pen(Brushes.IndianRed, 20);
                break;
            case 01:
                this.Chart.DataPen = new Pen(Brushes.Navy, 20);
                break;
            case 02:
                this.Chart.DataPen = new Pen(Brushes.Cyan, 20);
                break;
            case 03:
                this.Chart.DataPen = new Pen(Brushes.Aquamarine, 20);
                break;
            case 04:
                this.Chart.DataPen = new Pen(Brushes.DarkGreen, 20);
                break;
            case 05:
                this.Chart.DataPen = new Pen(Brushes.GreenYellow, 20);
                break;
            case 06:
                this.Chart.DataPen = new Pen(Brushes.Yellow, 20);
                break;
            case 07:
                this.Chart.DataPen = new Pen(Brushes.Gold, 20);
                break;
            case 08:
                this.Chart.DataPen = new Pen(Brushes.Orange, 20);
                break;
            case 09:
                this.Chart.DataPen = new Pen(Brushes.OrangeRed, 20);
                break;
            case 10:
                this.Chart.DataPen = new Pen(Brushes.DarkRed, 20);
                break;
            case 11:
                this.Chart.DataPen = new Pen(Brushes.DarkViolet, 20);
                break;
            case 12:
                this.Chart.DataPen = new Pen(Brushes.Violet, 20);
                break;
            default:
                this.Chart.DataPen = this.Chart.DataPen;
                break;
        }
    }

    void InitializeComponent() 
    { 
        AvaloniaXamlLoader.Load(this);
    }

    Chart Chart { get; }
    
    private static FleetControl FlotaDefault = new FleetControl();
    
    private bool opcionAdquision = true;
}
    