using System;
using ControlFlota.Core;

namespace fleetControlAvalonia;

class Artruck : Vehicle{
    const int capacity = 40000;

    public Artruck(string brand, string license, string modelName, float fuel, 
        DateTime adq, DateTime fabr, bool wifi, bool bt, bool ac, bool bed, bool tv) 
        : base(brand, license, modelName, fuel, adq, fabr, wifi, bt, ac, bed, tv){}
}