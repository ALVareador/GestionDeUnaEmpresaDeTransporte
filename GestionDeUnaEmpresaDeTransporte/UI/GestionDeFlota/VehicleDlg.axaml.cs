using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ControlFlota.UI;

       
    public partial class VehicleDlg : Window
    {
        public VehicleDlg()
            : this(DefaultBrand, DefaultLicense, DefaultModel, DefaultFuelPerKM, DefaultAdqDate, DefaultFabrDate, 
                DefaultWifi, DefaultBluetooth, DefaultAc, DefaultBed, DefaultTv)
        {
        }
        
        public VehicleDlg(string brand = DefaultBrand, string license = DefaultLicense, string model = DefaultModel, 
            float fuelPerKM = DefaultFuelPerKM, DateOnly adqDate = default, DateOnly FabrDate = default, 
            bool wifi = DefaultWifi, bool bluetooth = DefaultBluetooth, bool ac = DefaultAc, bool bed = DefaultBed, bool tv = DefaultTv)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var btOk = this.FindControl<Button>( "BtOk" );
            var btCancel = this.FindControl<Button>( "BtCancel" );
            
            var edBrand = this.FindControl<TextBox>( "EdBrand" );
            var edLicense = this.FindControl<TextBox>( "EdLicense" );
            var edModel = this.FindControl<TextBox>( "EdModel" );
            var edFuelPerKM = this.FindControl<NumericUpDown>( "EdFuelPerKM" );
            var edAdqDate = this.FindControl<DatePicker>("EdAdqDate");
            var edAdqFabrDate = this.FindControl<DatePicker>("EdAdqFabrDate");
            var edWifi = this.FindControl<CheckBox>("EdWifi");
            var edBluetooth = this.FindControl<CheckBox>("EdBluetooth");
            var edAc = this.FindControl<CheckBox>("EdAc");
            var edBed = this.FindControl<CheckBox>("EdBed");
            var edTv = this.FindControl<CheckBox>("EdTv");
            var edVehicleType = this.FindControl<ComboBox>("EdVehicleType");

            btOk.Click += (_, _) => this.OnExit();
            btCancel.Click += (_, _) => this.OnCancelClicked();

            edBrand.Text = brand;
            edLicense.Text = license;
            edModel.Text = model;
            edFuelPerKM.Value = 10;
            edAdqDate.SelectedDate = DateTimeOffset.Now;
            edAdqFabrDate.SelectedDate = DateTimeOffset.Now;
            edWifi.IsChecked = wifi;
            edBluetooth.IsChecked = bluetooth;
            edAc.IsChecked = ac;
            edBed.IsChecked = bed;
            edTv.IsChecked = tv;
            edVehicleType.SelectedIndex = 0;
            this.IsCancelled = false;
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
        }

        void OnCancelClicked()
        {
            this.IsCancelled = true;
            this.OnExit();
        }

        void OnExit()
        {
            this.Close();
        }

        public string Brand {
            get => this.FindControl<TextBox>( "EdBrand" ).Text.Trim();
        }
        
        public string License {
            get => this.FindControl<TextBox>( "EdLicense" ).Text.Trim();
        }
        
        public string Model {
            get => this.FindControl<TextBox>( "EdModel" ).Text.Trim();
        }
        
        public float FuelPerKM {
            get => (float) this.FindControl<NumericUpDown>( "EdFuelPerKM" ).Value;
        }
        
        public DateOnly AdqDate {
            get
            {
                DateTimeOffset? offset = this.FindControl<DatePicker>( "EdAdqDate" ).SelectedDate;
                return DateOnly.FromDateTime(offset.HasValue ? offset.Value.DateTime : DateTime.MaxValue);
            }
        }
        
        public DateOnly AdqFabrDate {
            get
            {
                DateTimeOffset? offset = this.FindControl<DatePicker>( "EdAdqFabrDate" ).SelectedDate;
                return DateOnly.FromDateTime(offset.HasValue ? offset.Value.DateTime : DateTime.MaxValue);
            }
        }
        
        public bool Wifi {
            get => (bool)this.FindControl<CheckBox>( "EdWifi" ).IsChecked;
        }
        
        public bool Bluetooth {
            get => (bool)this.FindControl<CheckBox>( "EdBluetooth" ).IsChecked;
        }
        
        public bool Ac {
            get => (bool)this.FindControl<CheckBox>( "EdAc" ).IsChecked;
        }
        
        public bool Bed {
            get => (bool)this.FindControl<CheckBox>( "EdBed" ).IsChecked;
        }
        
        public bool Tv {
            get => (bool)this.FindControl<CheckBox>( "EdTv" ).IsChecked;
        }
        
        public int VehicleType {
            // 0 - Truck
            // 1 - Van
            // 2 - Artruck
            get => this.FindControl<ComboBox>( "EdVehicleType" ).SelectedIndex;
        }
        
        public bool IsCancelled {
            get;
            private set;
        }
        
        const string DefaultBrand = "Uvigo";
        const string DefaultLicense = "xxx0000";
        const string DefaultModel = "Esei";
        const float DefaultFuelPerKM = 0;
        static DateOnly DefaultAdqDate = new DateOnly(1,1,1);
        static DateOnly DefaultFabrDate = new DateOnly(1,1,1);
        const bool DefaultWifi = false;
        const bool DefaultBluetooth = false;
        const bool DefaultAc = false;
        const bool DefaultBed = false;
        const bool DefaultTv = false;
    }
