using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;
using ProyDIA.UI;

namespace GestionDeUnaEmpresaDeTransporte.UI.Transportes;

public partial class MainWindowTransportes : Window
{
    public MainWindowTransportes()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        var opExit = this.FindControl<MenuItem>("OpExit");
        var opInsert = this.FindControl<MenuItem>("OpInsert");
        var btRegistrar = this.FindControl<Button>("BtRegistrar");
        var dtTransportes = this.FindControl<DataGrid>("DtTransportes");
        var opDeleteAll = this.FindControl<MenuItem>("OpDeleteAll");
        opInsert.DoubleTapped += (_, _) => this.OnExit();


        Debug.Assert(opExit != null, "opExit not found in XAML");
        Debug.Assert(opInsert != null, "opInsert not found in XAML");
        Debug.Assert(opDeleteAll != null, "opDeleteAll not found in XAML");
        Debug.Assert(btRegistrar != null, "btRegistrar not found in XAML");
        Debug.Assert(dtTransportes != null, "dtTransportes not found in XAML");


        opExit.Click += (_, _) => this.OnExit();
        opInsert.Click += (_, _) => this.OnAdd();
        opDeleteAll.Click += (_, _) => this.OnDeleteAll();
        btRegistrar.Click += (_, _) => this.OnAdd();
        dtTransportes.SelectionChanged += (_, _) => this.OnTransporteSelected();
        this.Closed += (_, _) => this.OnClose();


        this.RegistroTransportes = XmlRegistroTransportes.RecuperaXml();
        dtTransportes.Items = this.RegistroTransportes;
    }

    void InitializeComponent()
    {
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        AvaloniaXamlLoader.Load(this);
    }

    public void OnExit()
    {
        new XmlRegistroTransportes(this.RegistroTransportes).GuardaXml();
        this.Close();
    }

    async void OnAdd()
    {
        var transporteDlg = new TransporteDlg();
        await transporteDlg.ShowDialog(this);

        
        if(!transporteDlg.IsCancelled)
        {
            var t = new Transporte(transporteDlg.Matricula,
                transporteDlg.Tipo, transporteDlg.Cliente,
                transporteDlg.FechaContra, transporteDlg.Kms,
                transporteDlg.FechaSal, transporteDlg.FechaEntre);
            t.SueldoHora = transporteDlg.SueldoHora;
            t.PrecioLitro = transporteDlg.PrecioLitro;
            t.CantLtKms = transporteDlg.CantLtKms;
            t.Update();

            this.RegistroTransportes.Add(t);
        }
        

        return;
    }

    void OnTransporteSelected()
    {
        var dtTransportes = this.FindControl<DataGrid>("DtTransportes");
        

        Debug.Assert(dtTransportes != null, "dtTransportes not found in XAML");

        Transporte? transporte = (Transporte) dtTransportes.SelectedItem;
        int position = dtTransportes.SelectedIndex;
        
        var transporteDlg = new TransporteDlg(transporte);
        transporteDlg.ShowDialog(this);

        if (!transporteDlg.IsCancelled)
        {
            var t = new Transporte(transporteDlg.Matricula,
                transporteDlg.Tipo, transporteDlg.Cliente,
                transporteDlg.FechaContra, transporteDlg.Kms,
                transporteDlg.FechaSal, transporteDlg.FechaEntre);
            t.SueldoHora = transporteDlg.SueldoHora;
            t.PrecioLitro = transporteDlg.PrecioLitro;
            t.CantLtKms = transporteDlg.CantLtKms;
            t.Update();

            this.RegistroTransportes.Modificar_av(position, t);
        }

        

    }

    void OnClose()
    {
        new XmlRegistroTransportes(this.RegistroTransportes).GuardaXml();
    }

    void OnDeleteAll()
    {
        this.RegistroTransportes.Clear();
    }

    public RegistroTransportes RegistroTransportes
    {
        get;
    }
}