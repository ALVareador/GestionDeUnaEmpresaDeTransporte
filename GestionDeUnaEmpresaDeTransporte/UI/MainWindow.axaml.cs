using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControlFlota.Core;
using fleetControlAvalonia;
using GestionDeUnaEmpresaDeTransporte.Core.Busquedas;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;
using GestionDeUnaEmpresaDeTransporte.Core.GestionDeClientes;
using GestionDeUnaEmpresaDeTransporte.Graficos;
using GestionDeUnaEmpresaDeTransporte.UI.Busquedas;
using GestionDeUnaEmpresaDeTransporte.UI.GestionDeClientes;
using GestionDeUnaEmpresaDeTransporte.UI.GestionDeFlota;
using GestionDeUnaEmpresaDeTransporte.UI.Graficos;
using ProyDIA.UI;
using ProyectoIndividualDia;

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
            
            //Cliente
            var btInsertCliente = this.FindControl<Button>( "addButtonClient" );
            var btModCliente = this.FindControl<Button>( "modButtonClient" );
            var btDelCliente = this.FindControl<Button>( "delButtonClient" );
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            

            this.RegistroClientes = new RegistroClientes();
            this.RegistroClientes = ArchivoXML.fromXML();
            dtClientes.Items = this.RegistroClientes;
            
            btInsertCliente.Click += (_, _) => this.OnInsertCliente();
            btModCliente.Click += (_, _) => this.OnModCliente(dtClientes.SelectedItem);
            btDelCliente.Click += (_, _) => this.OnDeleteCliente(dtClientes.SelectedItem);
            
            
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
            
            //Busquedas
            var btIniciarBusqueda = this.FindControl<Button>( "iniciarBusqueda" );
            var btBorrarBusqueda = this.FindControl<Button>( "limpiaBusqueda" );
            btIniciarBusqueda.Click += (_, _) => this.BusquedaBox();
            btBorrarBusqueda.Click += (_, _) => this.RestartBusqueda();
        }

        private void RestartBusqueda()
        {
            var dtVehiculos = this.FindControl<DataGrid>( "vehicleGrid");
            var dtTransportes = this.FindControl<DataGrid>( "DtTransportes");
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtTransportes.Items = this.RegistroTransportes.busquedaReservasCamionFlota();
            dtClientes.Items = this.RegistroClientes.Clientes;
            dtVehiculos.Items = this.FleetControl.ToArray();

        }

        public void BusquedaBox()
        {
            var tipoBusqueda = this.CampoBusqueda;
            switch (tipoBusqueda)
            {
                case "Cliente": busquedaCliente();
                    break;
                case "Flota": ; 
                    break;
                case "Transporte": ; 
                    break;
                case "Transportes pendientes": busquedaTransPendientes(); 
                    break;
                case "Disponibilidad": busquedaDisponibilidad(); 
                    break;
                case "Historico cliente": busquedaHistoricoCliente(); 
                    break;
                case "Reservas cliente": busquedaReservasCliente(); 
                    break;
                case "Reservas camion": busquedaReservasCamion(); 
                    break;
                case "Ocupacion": busquedaOcupacion(); 
                    break;
            }
        }

        async void busquedaOcupacion()
        {
            BusquedaDialogOcupacion busquedaDialog = new BusquedaDialogOcupacion();
            await busquedaDialog.ShowDialog(this);
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            string campoBusqueda = busquedaDialog.CampoBusqueda;
            var dtVehiculos = this.FindControl<DataGrid>( "vehicleGrid");
            
            if (campoBusqueda.Equals("Fecha"))
            {
                dtVehiculos.Items = this.FleetControl.busquedaOcupacionFecha(this.RegistroTransportes, campoBusqueda);
            }
            else
            {
                dtVehiculos.Items = this.FleetControl.busquedaOcupacionAnho(this.RegistroTransportes, campoBusqueda);
            }
        }

        async void busquedaReservasCamion()
        {
            BusquedaDialogUnico busquedaDialog = new BusquedaDialogUnico("Matricula camión");
            await busquedaDialog.ShowDialog(this);
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtTransportes = this.FindControl<DataGrid>( "DtTransportes");
            
            if (valorBusqueda.Length > 1)
            {
                dtTransportes.Items = this.RegistroTransportes.busquedaReservasCamionConcreto(valorBusqueda);
            }
            else
            {
                dtTransportes.Items = this.RegistroTransportes.busquedaReservasCamionFlota();
            }
        }

        async void busquedaReservasCliente()
        {
            BusquedaDialogUnico busquedaDialog = new BusquedaDialogUnico("DNI cliente");
            await busquedaDialog.ShowDialog(this);
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtTransportes = this.FindControl<DataGrid>( "DtTransportes");
            
            dtTransportes.Items = this.RegistroTransportes.busquedaReservasCliente(valorBusqueda);
        }

        async void busquedaHistoricoCliente()
        {
            BusquedaDialogUnico busquedaDialog = new BusquedaDialogUnico("DNI cliente");
            await busquedaDialog.ShowDialog(this);
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtTransportes = this.FindControl<DataGrid>( "DtTransportes");
            
            dtTransportes.Items = this.RegistroTransportes.busquedaHistoricoCliente(valorBusqueda);
            
        }

        async void busquedaDisponibilidad()
        {
            BusquedaDialogUnico busquedaDialog = new BusquedaDialogUnico("Marca");
            await busquedaDialog.ShowDialog(this);
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtVehiculos = this.FindControl<DataGrid>( "vehicleGrid");

            if (valorBusqueda.Length > 1)
            {
                dtVehiculos.Items = this.FleetControl.busquedaDisponiblesConcreto(this.RegistroTransportes,valorBusqueda);
            }
            else
            {
                dtVehiculos.Items = this.FleetControl.busquedaDisponiblesFlota(this.RegistroTransportes);
            }
        }

        async void busquedaTransPendientes()
        {
            BusquedaDialogUnico busquedaDialog = new BusquedaDialogUnico("Matricula");
            await busquedaDialog.ShowDialog(this);
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtTransportes = this.FindControl<DataGrid>( "DtTransportes");

            if (valorBusqueda.Length > 1)
            {
                dtTransportes.Items = this.RegistroTransportes.busquedaTransportesPendientesConcreto(valorBusqueda);
            }
            else
            {
                dtTransportes.Items = this.RegistroTransportes.busquedaTransportesPendientesFlota();
            }
        }

       

        async void busquedaCliente()
        {
            BusquedaDialog busquedaDialog = new BusquedaDialog();
            await busquedaDialog.ShowDialog(this);
            string campoBusqueda = busquedaDialog.CampoBusqueda;
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            switch (campoBusqueda)
            {
                case "NIF":
                    dtClientes.Items =  new RegistroClientes(this.RegistroClientes.busquedaPorNIF(valorBusqueda)).Clientes;
                    break;
                case "Nombre": 
                    dtClientes.Items =  new RegistroClientes(this.RegistroClientes.busquedaPorNombre(valorBusqueda)).Clientes;
                    break;
                case "Tlf": 
                    dtClientes.Items =  new RegistroClientes(this.RegistroClientes.busquedaPorTLF(valorBusqueda)).Clientes;
                    break;
                case "Mail": 
                    dtClientes.Items =  new RegistroClientes(this.RegistroClientes.busquedaPorMail(valorBusqueda)).Clientes;
                    break;
                case "Postal": 
                    dtClientes.Items =  new RegistroClientes(this.RegistroClientes.busquedaPorCodPostal(Int32.Parse(valorBusqueda))).Clientes;
                    break;
            }
        }

        private void OnDeleteCliente(object dtClientesSelectedItem)
        {
            this.RegistroClientes.Borra((Cliente)dtClientesSelectedItem);
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = RegistroClientes.Clientes;
        }

        async void OnModCliente(object dtClientesSelectedItem)
        {
            Cliente selected = (Cliente)dtClientesSelectedItem;
            EditDialog editDialog = new EditDialog(selected.Nif,selected.Nombre,selected.Tlf,selected.Mail,selected.DirPostal);
            await editDialog.ShowDialog(this);
            this.RegistroClientes.Modifica(new Cliente(editDialog.Nif,editDialog.Nombre,editDialog.Tlf,editDialog.Mail,editDialog.Postal));
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = this.RegistroClientes.Clientes;
        }

        async void OnInsertCliente()
        {
            InsertDialog insertDialog = new InsertDialog();
            await insertDialog.ShowDialog(this);
            this.RegistroClientes.Inserta(new Cliente(insertDialog.Nif,insertDialog.Nombre,insertDialog.Tlf,insertDialog.Mail,insertDialog.Postal));
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = this.RegistroClientes.Clientes;
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

        public RegistroClientes RegistroClientes
        {
            get;
        }
        
        public string CampoBusqueda
        {
            get
            {
                var selectedIndex = this.FindControl<ComboBox>("tipoBusqueda").SelectedIndex;
                switch (selectedIndex)
                {
                    case 0: return "Cliente";
                    case 1: return "Flota";
                    case 2: return "Transporte";
                    case 3: return "Transportes pendientes";
                    case 4: return "Disponibilidad";
                    case 5: return "Historico cliente";
                    case 6: return "Reservas cliente";
                    case 7: return "Reservas camion";
                    case 8: return "Ocupacion";
                }

                return null;
            }
        }
    }
}