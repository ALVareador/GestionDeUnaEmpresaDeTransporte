using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ControlFlota.Core;

public class FleetControl: ObservableCollection<Vehicle> {
    /*public List<Vehicle> vehicles
    {
        get;
        set;
    }*//*

    public void Add(string type, string brand, string license, string modelName, float fuel, 
                    DateOnly adq, DateOnly fabr, bool wifi, bool bt, bool ac, bool bed, bool tv){
        switch(type){
            case "Van":
                this.vehicles.Add(new Van(brand, license, modelName, fuel, adq, fabr, wifi, bt, ac, bed, tv));
                break;
            case "Truck":
                this.vehicles.Add(new Truck(brand, license, modelName, fuel, adq, fabr, wifi, bt, ac, bed, tv));
                break;
            case "Artruck":
                this.vehicles.Add(new Artruck(brand, license, modelName, fuel, adq, fabr, wifi, bt, ac, bed, tv));
                break;
        }
    }

    public void Delete(int i){
        this.vehicles.RemoveAt(i);
    }

    public Vehicle ReturnVehicle(int i){
        Vehicle toret = this.vehicles[i];
        return toret;
    }*/

    
    public FleetControl()
            :this( new List<Vehicle>() )
    {
    }

    public FleetControl(IEnumerable<Vehicle> vehicles)
            :base(vehicles)
    {
    }

    public void AddRange(IEnumerable<Vehicle> vs)
    {
        foreach (Vehicle v in vs)
        { 
            this.Add( v );
        }
    }
        
    public Vehicle[] ToArray()
    {
        var toret = new Vehicle[ this.Count ];
	        
        this.CopyTo( toret, 0 );
        return toret;
    }

    public override string ToString()
    {
        var toret = new StringBuilder();
			
        foreach(Vehicle v in this) {
            toret.AppendLine( v.ToString() );
        }

        return toret.ToString();
    }

}