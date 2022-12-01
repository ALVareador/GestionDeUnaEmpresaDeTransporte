using System;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
    public comodidadPorCamion(Flota f)
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
        /*var opEnero = this.FindControl<Button>("BtEnero");
        var opFebrero = this.FindControl<Button>("BtFebrero");
        var opMarzo = this.FindControl<Button>("BtMarzo");
        var opAbril = this.FindControl<Button>("BtAbril");
        var opMayo = this.FindControl<Button>("BtMayo");
        var opJunio = this.FindControl<Button>("BtJunio");
        var opJulio = this.FindControl<Button>("BtJulio");
        var opAgosto = this.FindControl<Button>("BtAgosto");
        var opSetiembre = this.FindControl<Button>("BtSetiembre");
        var opOctubre = this.FindControl<Button>("BtOctubre");
        var opNoviembre = this.FindControl<Button>("BtNoviembre");
        var opDiciembre = this.FindControl<Button>("BtDiciembre");
        var opOlder = this.FindControl<Button>("BtOlder");
        var opAdquisicion = this.FindControl<Button>("BtAdquisicion");
        var opFabricacion = this.FindControl<Button>("BtFabricacion");*/
            
        this.Chart.LegendY = "Nº de camiones";
        this.Chart.LegendX = "Comodidades"; 
        this.Chart.Values = valoresComodidades(f); 
        this.Chart.Labels = new []{ "Wifi", "Bluetooth", "Aire acondicionado", "Litera de descanso", "TV", "Total" };
        //this.Chart.IsVisible = false;
        //this.Chart.Height = 50;
        this.Chart.Type = Chart.ChartType.Bars; 
        this.Chart.DataPen = new Pen( Brushes.IndianRed, 20 );
        
        opExit.Click += (_, _) => this.Close(); 
        opAbout.Click += (_, _) => this.OnAbout();
        opFecha.SelectedDateChanged += (_, _) => this.FiltrarPorFecha(opFecha.SelectedDate, f, opcionAdquision);
        opOpcion.SelectionChanged += (_, _) => this.CambiarOpcion(opOpcion.SelectedIndex, opFecha.SelectedDate, f);
        /*opAdquisicion.Click += (_, _) => this.opcionAdquision = true; 
        opFabricacion.Click += (_, _) => this.opcionAdquision = false; 
        opEnero.Click += (_, _) => this.FiltrarPorFecha(1, f, opcionAdquision);
        opFebrero.Click += (_, _) => this.FiltrarPorFecha(2, f, opcionAdquision); 
        opMarzo.Click += (_, _) => this.FiltrarPorFecha(3, f, opcionAdquision); 
        opAbril.Click += (_, _) => this.FiltrarPorFecha(4, f, opcionAdquision); 
        opMayo.Click += (_, _) => this.FiltrarPorFecha(5, f, opcionAdquision); 
        opJunio.Click += (_, _) => this.FiltrarPorFecha(6, f, opcionAdquision); 
        opJulio.Click += (_, _) => this.FiltrarPorFecha(7, f, opcionAdquision); 
        opAgosto.Click += (_, _) => this.FiltrarPorFecha(8, f, opcionAdquision); 
        opSetiembre.Click += (_, _) => this.FiltrarPorFecha(9, f, opcionAdquision); 
        opOctubre.Click += (_, _) => this.FiltrarPorFecha(10, f, opcionAdquision);*/

    }

    private void CambiarOpcion(int opOpcionSelectedIndex, DateTimeOffset? opFechaSelectedDate, Flota flota)
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
    private void FiltrarPorFecha(DateTimeOffset? mes, Flota f, bool opcionA) 
    { 
        Flota flotaFiltrada = new Flota();
        
        if (mes.Value.Month == 0)
        {
            flotaFiltrada = f;
        }
        else
        { 
            foreach (var xCamion in f.Camiones)
            {
                if (opcionA == true)
                {
                    if (xCamion.FechaAdquisicion.Month == mes.Value.Month && xCamion.FechaAdquisicion.Year == mes.Value.Year)
                    {
                        flotaFiltrada.Camiones.Add(xCamion);
                    }
                }
                else
                {
                    if (xCamion.FechaFabricación.Month == mes.Value.Month && xCamion.FechaFabricación.Year == mes.Value.Year)
                    {
                        flotaFiltrada.Camiones.Add(xCamion);
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
    private IEnumerable<int> valoresComodidades(Flota flota) 
    {
        
        int[] comodidades = { 0, 0, 0, 0, 0, 0}; 
        if (flota.Camiones != null)
        {
            foreach (var camion in flota.Camiones)
            {
                if (camion.Wifi)
                {
                    comodidades[0] = comodidades[0] + 1;
                }

                if (camion.Bluetooth)
                {
                    comodidades[1] = comodidades[1] + 1;
                }

                if (camion.AireAcondicionado)
                {
                    comodidades[2] = comodidades[2] + 1;
                }

                if (camion.LiteraDeDescando)
                {
                    comodidades[3] = comodidades[3] + 1;
                }

                if (camion.TV)
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
        this.Chart.DataPen = mes switch
        {
            00 => new Pen(Brushes.IndianRed, 20),
            01 => new Pen(Brushes.Navy, 20),
            02 => new Pen(Brushes.Cyan, 20),
            03 => new Pen(Brushes.Aquamarine, 20),
            04 => new Pen(Brushes.DarkGreen, 20),
            05 => new Pen(Brushes.GreenYellow, 20),
            06 => new Pen(Brushes.Yellow, 20),
            07 => new Pen(Brushes.Gold, 20),
            08 => new Pen(Brushes.Orange, 20),
            09 => new Pen(Brushes.OrangeRed, 20),
            10 => new Pen(Brushes.DarkRed, 20),
            11 => new Pen(Brushes.DarkViolet, 20),
            12 => new Pen(Brushes.Violet, 20),
            _ => this.Chart.DataPen
        };
    }
    
    /// <summary>
    /// Muestra la ventana sobre la aplicacion.
    /// </summary>
    void OnAbout() 
    { 
        new AboutWindow().ShowDialog( this );
    } 
    
    void InitializeComponent() 
    { 
        AvaloniaXamlLoader.Load(this);
    }

    Chart Chart { get; }
    
    private static Flota FlotaDefault = new Flota();
    
    private bool opcionAdquision = true;
}
    