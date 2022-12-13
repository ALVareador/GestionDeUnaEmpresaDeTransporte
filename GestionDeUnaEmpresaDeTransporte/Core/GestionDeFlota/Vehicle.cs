using System;

namespace ControlFlota.Core;

public class Vehicle{

    public string brand{
        get; set;
    }
    public string license{
        get; set;
    }
    public string model{
        get; set;
    }
    public float fuelPerKM{
        get; set;
    }
    public DateOnly adqDate{
        get; set;
    }
    public DateOnly fabrDate{
        get; set;
    }
    public bool wifi{
        get; set;
    }
    public bool bluetooth{
        get; set;
    }
    public bool ac{
        get; set;
    }
    public bool bed{
        get; set;
    }
    public bool tv{
        get; set;
    }

    public Vehicle(string brand, string license, string modelName, float fuel, DateOnly adq, DateOnly fabr, bool wifi, bool bt, bool ac, bool bed, bool tv){
        this.brand = brand;
        this.license = license;
        model = modelName;
        fuelPerKM = fuel;
        adqDate = adq;
        fabrDate = fabr;
        this.wifi = wifi;
        this.bluetooth = bt;
        this.ac = ac;
        this.bed = bed;
        this.tv = tv;
    }
    
    
}