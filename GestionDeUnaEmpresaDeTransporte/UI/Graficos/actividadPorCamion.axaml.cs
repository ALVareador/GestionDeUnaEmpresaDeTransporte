using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GestionDeUnaEmpresaDeTransporte.UI.Graficos;

namespace GestionDeUnaEmpresaDeTransporte.Graficos;

/// <summary>
/// Muestra el numero de transportes para un camion en concreto.
/// </summary>
public partial class actividadPorCamion : Window
{

    /// <summary>
    /// Crea una grafica vacia.
    /// </summary>
    public actividadPorCamion() : this(TransporteDefault)
    {

    }
    
    /// <summary>
    /// Crea una nueva grafica a partir de una lista de transportes.
    /// </summary>
    /// <param name="t">Lista de transportes.</param>
    public actividadPorCamion(Transportes t)
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
        
        this.Chart.LegendY = "Número de transportes"; 
        this.Chart.LegendX = "Camiones";
        IEnumerable<string> listaCamiones = generarListaCamiones(t);
        this.Chart.Labels = listaCamiones;
        this.Chart.Values = generarNumeroTransportes(t, listaCamiones);

        opExit.Click += (_, _) => this.Close();
        opAbout.Click += (_, _) => this.OnAbout();
        opFecha.SelectedDateChanged += (_, _) => this.FiltrarPorFecha(opFecha.SelectedDate, t, listaCamiones, opcionSalida);
        opOpcion.SelectionChanged += (_, _) => this.CambiarOpcion(opOpcion.SelectedIndex, opFecha.SelectedDate, t, listaCamiones);

        this.Chart.DataPen = new Pen( Brushes.Navy, 2 );
        this.Chart.Type = Chart.ChartType.Lines; 
    }

    /// <summary>
    /// Permite cambiar la opcion para buscar los transportes
    /// </summary>
    /// <param name="indexOpcion">Opcion seleccionada</param>
    /// <param name="SelectedDate">Fecha seleccionada</param>
    /// <param name="transportes">Lista de transportes</param>
    /// <param name="listaCamiones">Lista de camiones</param>
    private void CambiarOpcion(int indexOpcion, DateTimeOffset? SelectedDate, Transportes transportes, IEnumerable<string> listaCamiones)
    {
        if (indexOpcion == 0)
        {
            opcionSalida = true;
        }
        else
        {
            opcionSalida = false;
        }
        this.FiltrarPorFecha(SelectedDate, transportes, listaCamiones, opcionSalida);
    }

    /// <summary>
    /// Modifica la grafica para mostrar solo los transportes del mes seleccionado.
    /// </summary>
    /// <param name="mes">Mes del que queremos ver los transportes.</param>
    /// <param name="t">Lista de transportes.</param>
    /// <param name="listaCamiones">Lista de camiones.</param>
    /// <param name="opcionS">Opcion seleccionada para mostrar transportes por fecha de salida o fecha de entrega.</param>
    private void FiltrarPorFecha(DateTimeOffset? mes, Transportes t, IEnumerable<string> listaCamiones, bool opcionS)
    {
        Transportes transportesFiltrados = new Transportes();
        if (mes.Value.Month == 0)
        {
            transportesFiltrados = t;
        }
        else
        {
            foreach (var xTransporte in t.ListaTransportes)
            {
                if (opcionS == true)
                {
                    if (xTransporte.FechaSalida.Month == mes.Value.Month && xTransporte.FechaEntrega.Year == mes.Value.Year)
                    {
                        transportesFiltrados.ListaTransportes.Add(xTransporte);
                    }
                }
                else
                {
                    if (xTransporte.FechaEntrega.Month == mes.Value.Month && xTransporte.FechaEntrega.Year == mes.Value.Year)
                    {
                        transportesFiltrados.ListaTransportes.Add(xTransporte);
                    }
                }
            }
        }

        //PintarGrafica(mes.Value.Month);
        this.Chart.Values = generarNumeroTransportes(transportesFiltrados, listaCamiones);
        this.Chart.Draw();
    }

    /// <summary>
    /// Cambia el color de la grafica.
    /// </summary>
    /// <param name="mes">Mes seleccionado.</param>
    private void PintarGrafica(int mes)
    {
        this.Chart.DataPen = mes switch
        {
            00 => new Pen(Brushes.IndianRed, 2),
            01 => new Pen(Brushes.Navy, 2),
            02 => new Pen(Brushes.Cyan, 2),
            03 => new Pen(Brushes.Aquamarine, 2),
            04 => new Pen(Brushes.DarkGreen, 2),
            05 => new Pen(Brushes.GreenYellow, 2),
            06 => new Pen(Brushes.Yellow, 2),
            07 => new Pen(Brushes.Gold, 2),
            08 => new Pen(Brushes.Orange, 2),
            09 => new Pen(Brushes.OrangeRed, 2),
            10 => new Pen(Brushes.DarkRed, 2),
            11 => new Pen(Brushes.DarkViolet, 2),
            12 => new Pen(Brushes.Violet, 2),
            _ => this.Chart.DataPen
        };
    }

    /// <summary>
    /// Crea un array del numero de transportes para cada camion.
    /// </summary>
    /// <param name="t">Lista de transportes.</param>
    /// <param name="listaCamiones">Lista de camiones.</param>
    /// <returns>Array del numero de transportes.</returns>
    private IEnumerable<int> generarNumeroTransportes(Transportes t, IEnumerable<string> listaCamiones)
    {
        int[] toret = new int[listaCamiones.Count()];
        int i = 0;
        
        foreach (var matriculaCamion in listaCamiones)
        {
            foreach (var transporte in t.ListaTransportes)
            {
                if (matriculaCamion == transporte.camion.Matricula)
                {
                    toret[i] = toret[i] + 1;
                }
            }

            i++;
        }

        return toret;
    }

    /// <summary>
    /// Crea una lista de camiones unicos.
    /// </summary>
    /// <param name="t">Lista de transportes.</param>
    /// <returns></returns>
    private IEnumerable<string> generarListaCamiones(Transportes t)
    {
        List<String> idCamionesUnicos = new List<string>();

        if (t.ListaTransportes.Count != 0)
        {
            foreach (var trans in t.ListaTransportes)
            {
                if (!idCamionesUnicos.Contains(trans.camion.Matricula))
                {
                    idCamionesUnicos.Add(trans.camion.Matricula);
                }
            }
        }

        if (idCamionesUnicos != null)
        {
            return idCamionesUnicos.ToArray();
        }
        else
        {
            return new []{ "No hay camiones en la flota"};
        }
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

    private static readonly Transportes TransporteDefault = new Transportes();

    private bool opcionSalida = true;
}

