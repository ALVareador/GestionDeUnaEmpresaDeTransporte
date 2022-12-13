using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControlFlota.Core;
using fleetControlAvalonia;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;
using GestionDeUnaEmpresaDeTransporte.Graficos;
using GestionDeUnaEmpresaDeTransporte.UI.GestionDeFlota;
using GestionDeUnaEmpresaDeTransporte.UI.Graficos;
using ProyDIA.UI;

namespace GestionDeUnaEmpresaDeTransporte.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            //Flota
            var btInsert = this.FindControl<Button>( "addButton" );
            var btMod = this.FindControl<Button>( "modButton" );
            var btDel = this.FindControl<Button>( "delButton" );
            var dtTrips = this.FindControl<DataGrid>( "vehicleGrid" );
            
            btInsert.Click += (_, _) => this.OnInsert();

            btMod.Click += (_, _) => this.OnMod();
            btDel.Click += (_, _) => this.OnDel();
            this.Closed += (_, _) => this.OnClose();
            
            dtTrips.SelectionChanged += (_, _) => this.OnTripSelected();

            this.FleetControl = XmlFleetControl.RecuperaXml();
            dtTrips.Items = this.FleetControl;
            
            //Transportes
            var btRegistrar = this.FindControl<Button>("BtRegistrar");
            var dtTransportes = this.FindControl<DataGrid>("DtTransportes");
            var opDeleteAll = this.FindControl<MenuItem>("OpDeleteAll");
            
            Debug.Assert(opDeleteAll != null, "opDeleteAll not found in XAML");
            Debug.Assert(btRegistrar != null, "btRegistrar not found in XAML");
            Debug.Assert(dtTransportes != null, "dtTransportes not found in XAML");

            
            opDeleteAll.Click += (_, _) => this.OnDeleteAll();
            btRegistrar.Click += (_, _) => this.OnAdd();
            dtTransportes.SelectionChanged += (_, _) => this.OnTransporteSelected();
            this.Closed += (_, _) => this.OnClose();
            
            this.RegistroTransportes = XmlRegistroTransportes.RecuperaXml();
            dtTransportes.Items = this.RegistroTransportes;
            
            //Grafica

            var opActividadPorCamion = this.FindControl<MenuItem>("OpActividadPorCamion");
            var opGraficaComodidadPorCamion = this.FindControl<MenuItem>("OpGraficaComodidadPorCamion");

            opActividadPorCamion.Click += (_, _) => abrirGraficaActividadPorCamion(RegistroTransportes);
            opGraficaComodidadPorCamion.Click += (_, _) => abrirGraficaComodidadPorCamion(FleetControl);
        }

        async private void abrirGraficaActividadPorCamion(RegistroTransportes registroTransportes)
        {
            var graficaActividadComodidadPorCamion = new actividadPorCamion(registroTransportes);
            await graficaActividadComodidadPorCamion.ShowDialog( this );
        }

        async private void abrirGraficaComodidadPorCamion(FleetControl FleetControl)
        {
            var graficaComodidadPorCamion = new comodidadPorCamion(FleetControl);
            await graficaComodidadPorCamion.ShowDialog( this );
        }

        private void OnDel()
        {
            if (vehicleSel != null)
            {
                var vehicle = vehicleSel;
                if (FleetControl.Contains(vehicle))
                {
                    FleetControl.Remove(vehicle);
                }
            }

            return;
        }

        async private void OnMod()
        {
            if (vehicleSel != null)
            {
                var vehicle = vehicleSel;
                var vehicleMod = new VehicleDlg(vehicle.brand, vehicle.license, vehicle.model, vehicle.fuelPerKM,
                    vehicle.adqDate, vehicle.fabrDate, vehicle.wifi, vehicle.bluetooth, vehicle.ac, vehicle.bed,
                    vehicle.tv);
                await vehicleMod.ShowDialog(this);

                if (!vehicleMod.IsCancelled)
                {
                    Vehicle vehicleModificated;
                    int index = FleetControl.IndexOf(vehicle);
                    FleetControl.RemoveAt(index);
                    switch (vehicleMod.VehicleType)
                    {
                        case 0:
                            vehicleModificated = new Truck(vehicleMod.Brand, vehicleMod.License, vehicleMod.Model,
                                vehicleMod.FuelPerKM, vehicleMod.AdqDate, vehicleMod.AdqFabrDate, vehicleMod.Wifi,
                                vehicleMod.Bluetooth, vehicleMod.Ac, vehicleMod.Bed, vehicleMod.Tv);
                            FleetControl.Insert(index, vehicleModificated);
                            break;
                        case 1:
                            vehicleModificated = new Van(vehicleMod.Brand, vehicleMod.License, vehicleMod.Model,
                                vehicleMod.FuelPerKM, vehicleMod.AdqDate, vehicleMod.AdqFabrDate, vehicleMod.Wifi,
                                vehicleMod.Bluetooth, vehicleMod.Ac, vehicleMod.Bed, vehicleMod.Tv);
                            FleetControl.Insert(index, vehicleModificated);
                            break;
                        case 2:
                            vehicleModificated = new Artruck(vehicleMod.Brand, vehicleMod.License, vehicleMod.Model,
                                vehicleMod.FuelPerKM, vehicleMod.AdqDate, vehicleMod.AdqFabrDate, vehicleMod.Wifi,
                                vehicleMod.Bluetooth, vehicleMod.Ac, vehicleMod.Bed, vehicleMod.Tv);
                            FleetControl.Insert(index, vehicleModificated);
                            break;
                    }
                }
            }

            return;
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AvaloniaXamlLoader.Load(this);
        }
        
        void OnTripSelected()
        {
            var dtTrips = this.FindControl<DataGrid>( "vehicleGrid" );
            
            vehicleSel = (Vehicle) dtTrips.SelectedItem;
            vehicleSelIndex = dtTrips.SelectedIndex;
            
            return;
        }
        
        void OnClose()
        {
            new XmlFleetControl( this.FleetControl ).GuardaXml();
            new XmlRegistroTransportes(this.RegistroTransportes).GuardaXml();
        }
        

        async void OnInsert()
        {
            var vehicleInsert = new VehicleDlg();
            await vehicleInsert.ShowDialog( this );

            if ( !vehicleInsert.IsCancelled ) {
                switch (vehicleInsert.VehicleType)
                {
                    case 0:
                        this.FleetControl.Add(new Truck(vehicleInsert.Brand, vehicleInsert.License, vehicleInsert.Model, 
                            vehicleInsert.FuelPerKM, vehicleInsert.AdqDate, vehicleInsert.AdqFabrDate, vehicleInsert.Wifi, 
                            vehicleInsert.Bluetooth, vehicleInsert.Ac, vehicleInsert.Bed, vehicleInsert.Tv));
                        break;
                    case 1:
                        this.FleetControl.Add(new Van(vehicleInsert.Brand, vehicleInsert.License, vehicleInsert.Model, 
                            vehicleInsert.FuelPerKM, vehicleInsert.AdqDate, vehicleInsert.AdqFabrDate, vehicleInsert.Wifi, 
                            vehicleInsert.Bluetooth, vehicleInsert.Ac, vehicleInsert.Bed, vehicleInsert.Tv));
                        break;
                    case 2:
                        this.FleetControl.Add(new Artruck(vehicleInsert.Brand, vehicleInsert.License, vehicleInsert.Model, 
                            vehicleInsert.FuelPerKM, vehicleInsert.AdqDate, vehicleInsert.AdqFabrDate, vehicleInsert.Wifi, 
                            vehicleInsert.Bluetooth, vehicleInsert.Ac, vehicleInsert.Bed, vehicleInsert.Tv));
                        break;
                }
            }

            return;
        }

        public int vehicleSelIndex
        {
            get;
            set;
        }

        public Vehicle vehicleSel
        {
            get;
            set;
        }

        public FleetControl FleetControl {
            get;
        }
        
        //Flota
        
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

        void OnDeleteAll()
        {
            this.RegistroTransportes.Clear();
        }

        public RegistroTransportes RegistroTransportes
        {
            get;
        }
    }
}