using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GestionDeUnaEmpresaDeTransporte.Core.GestionDeClientes;
using GestionDeUnaEmpresaDeTransporte.Core.Transportes;

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
    
    public IEnumerable<Vehicle> busquedaDisponiblesFlota(RegistroTransportes transportes)
    {
        IList<Vehicle> toret = new List<Vehicle>();
        IEnumerable<Transporte> disponibles = transportes.Where(x => x.FechaSal > DateTime.Now || x.FechaEntre < DateTime.Now);
        foreach (var actual in disponibles)
        {

            if (vehiculoPorMatricula(actual.Matricula) != null)
            {
                toret.Add(vehiculoPorMatricula(actual.Matricula));
            }
            
        }

        return toret;
    }
    
    public IEnumerable<Vehicle> busquedaDisponiblesConcreto(RegistroTransportes transportes,string marca)
    {
        IEnumerable<Vehicle> toret = busquedaDisponiblesFlota(transportes);

        return toret.Where(x=> x.brand == marca);
    }
    
    public IEnumerable<Vehicle> busquedaOcupacionAnho(RegistroTransportes transportes,string tiempo)
    {
        IEnumerable<Transporte> transportesOcupados = transportes.Where(x => x.FechaEntre.Year.ToString().Equals(tiempo));
        IList<Vehicle> toret = new List<Vehicle>();
        
        foreach (var actual in transportesOcupados)
        {
            if (vehiculoPorMatricula(actual.Matricula) != null)
            {
                toret.Add(vehiculoPorMatricula(actual.Matricula));
            }
        }

        return toret;
    }
    
    public IEnumerable<Vehicle> busquedaOcupacionFecha(RegistroTransportes transportes,string tiempo)
    {
        IEnumerable<Transporte> transportesOcupados = transportes.Where(x => x.FechaEntre.Date.ToString().Equals(tiempo));
        IList<Vehicle> toret = new List<Vehicle>();
        
        foreach (var actual in transportesOcupados)
        {
            if (vehiculoPorMatricula(actual.Matricula) != null)
            {
                toret.Add(vehiculoPorMatricula(actual.Matricula));
            }
        }

        return toret;
    }

    private Vehicle vehiculoPorMatricula(string matricula)
    {
        foreach (var vehiculo in Items)
        {
            if (vehiculo.license.Equals(matricula))
            {
                return vehiculo;
            }
        }

        return null;
    }
    
    public IEnumerable<Vehicle> busquedaPorMatricula(string matricula)
    {
        return this.Items.Where(x=> x.license == matricula);
    }
    
    public IEnumerable<Vehicle> busquedaPorMarca(string marca)
    {
        return this.Items.Where(x=> x.brand == marca);
    }
    
    public IEnumerable<Vehicle> busquedaPorModelo(string modelo)
    {
        return this.Items.Where(x=> x.model == modelo);
    }
    
    public IEnumerable<Vehicle> busquedaPorConsumo(float consumoKM)
    {
        return this.Items.Where(x=> x.fuelPerKM == consumoKM);
    }
    
    

}