using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControlFlota.Core;
using ControlFlota.UI;
using fleetControlAvalonia;

namespace ControlFlota.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
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
    }
}