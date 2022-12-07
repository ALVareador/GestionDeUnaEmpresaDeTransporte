using System;
using ControlFlota.Core;

namespace fleetControlAvalonia;

class Van : Vehicle{
    const int capacity = 1500;

    public Van(string brand, string license, string modelName, float fuel, 
        DateOnly adq, DateOnly fabr, bool wifi, bool bt, bool ac, bool bed, bool tv) 
        : base(brand, license, modelName, fuel, adq, fabr, wifi, bt, ac, bed, tv){}
}