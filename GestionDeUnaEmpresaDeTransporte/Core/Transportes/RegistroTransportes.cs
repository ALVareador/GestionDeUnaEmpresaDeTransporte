using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ControlFlota.Core;

namespace GestionDeUnaEmpresaDeTransporte.Core.Transportes {
    public class RegistroTransportes: ObservableCollection<Transporte> {
        
        public RegistroTransportes() 
            :this(new List<Transporte>()) {
        }

        public RegistroTransportes(IEnumerable<Transporte> transportes)
            :base(transportes) {
        }

        public void AddRange(IEnumerable<Transporte> ts) {
            foreach (Transporte t in ts) {
                this.Add(t);
            }
        }

        public Transporte[] ToArray() {
            var toret = new Transporte[this.Count];
            this.CopyTo(toret, 0);
            return toret;
        }

        public void Eliminar (int posicion) {
            this.RemoveAt(posicion);
        }

        public void ELiminarTodo()
        {
            this.Clear();
        }

        public void Modificar(int posicion, string matricula, string tipo, string cliente, 
            DateTime fechaContra, double kms, DateTime fechaSal, 
            DateTime fechaEntre, double sueldo, double preciolitro,
            double cantltkm)
        {
            this.ElementAt(posicion).Matricula = matricula;
            this.ElementAt(posicion).Tipo = tipo;
            this.ElementAt(posicion).Cliente = cliente;
            this.ElementAt(posicion).FechaContra = fechaContra;
            this.ElementAt(posicion).Kms = kms;
            this.ElementAt(posicion).FechaSal = fechaSal;
            this.ElementAt(posicion).FechaEntre = fechaEntre;
            this.ElementAt(posicion).SueldoHora = sueldo;
            this.ElementAt(posicion).PrecioLitro = preciolitro;
            this.ElementAt(posicion).CantLtKms = cantltkm;

            this.ElementAt(posicion).Update();
        }

        public void Modificar_av(int position, Transporte nuevotr)
        {
            this.InsertItem(position, nuevotr);
            //this.Eliminar(position + 1);
        }
        
        public IEnumerable<Transporte> busquedaTransportesPendientesFlota()
        {
            return this.Items.Where(x => x.FechaSal >= DateTime.Now && x.FechaSal < DateTime.Now.AddDays(5) );
        }
        
        public IEnumerable<Transporte> busquedaTransportesPendientesConcreto(string matricula)
        {
            return this.Items.Where(x => x.Matricula == matricula && x.FechaSal >= DateTime.Now && x.FechaSal < DateTime.Now.AddDays(5) );
        }
        
        public IEnumerable<Transporte> busquedaHistoricoCliente(string dni)
        {
            return this.Items.Where(x => x.Cliente == dni );
        }
        
        public IEnumerable<Transporte> busquedaReservasCliente(string dni)
        {
            return this.Items.Where(x => x.Cliente == dni && x.FechaSal > DateTime.Now );
        }
        
        public IEnumerable<Transporte> busquedaReservasCamionConcreto(string matricula)
        {
            return this.Items.Where(x => x.Matricula == matricula );
        }
        
        public IEnumerable<Transporte> busquedaReservasCamionFlota()
        {
            return this.Items;
        }
        
        public IEnumerable<Transporte> busquedaPorTipoTransporte(string tipo)
        {
            return this.Items.Where(x => x.Tipo == tipo );
        }
        
        public IEnumerable<Transporte> busquedaPorDNICliente(string dni)
        {
            return this.Items.Where(x => x.Cliente == dni );
        }
        
        public IEnumerable<Transporte> busquedaPorFechaContrato(DateTime fechaContrato)
        {
            return this.Items.Where(x => x.FechaContra == fechaContrato );
        }
        public IEnumerable<Transporte> busquedaPorFechaSalida(DateTime fechaSalida)
        {
            return this.Items.Where(x => x.FechaSal == fechaSalida );
        }
        
        public IEnumerable<Transporte> busquedaPorFechaEntrega(DateTime fechaEntrega)
        {
            return this.Items.Where(x => x.FechaEntre == fechaEntrega );
        }
        
        public IEnumerable<Transporte> busquedaPorImporteDia(double importeDia)
        {
            return this.Items.Where(x => x.ImporteDia == importeDia );
        }
        
        public IEnumerable<Transporte> busquedaPorImporteKM(double importeKm)
        {
            return this.Items.Where(x => x.ImporteKm == importeKm );
        }
        
        public IEnumerable<Transporte> busquedaPorIVA(double iva)
        {
            return this.Items.Where(x => x.IvaAplicado == iva );
        }
        

             public int findMes(DateTime d)
    {
        DateTime date = d.Date;
        List<Transporte> tr = new List<Transporte>(this.Items);
        
        List<Transporte> l = tr.FindAll((t) => (t.FechaSal.Month == date.Month && t.FechaSal.Year == date.Year));
        return l.Count;
    }
    
    public int findAño(DateTime d)
    {
        DateTime date = d.Date;
        List<Transporte> tr = new List<Transporte>(this.Items);
        
        List<Transporte> l = tr.FindAll((t) => (t.FechaSal.Year == date.Year));
        return l.Count;
    }

    public List<int> getParaAño(DateTime d)
    {
        List<int> l = new List<int>();
        for (int i = 0; i < 12; i++)
        {
            l.Add(findMes(d.AddMonths(i)));
        }

        return l;
    }
    
    public List<int> getDesdeComienzo()
    {
        List<int> l = new List<int>();
        DateTime date = getFechaAntigua();
        date = date.Date;
        int a = getFechaNueva().Year - date.Year;
        for (int i = 0; i <= a; i++)
        {
            l.Add(findAño(date.AddYears(i)));
            Console.WriteLine(date.AddYears(i));
        }

        return l;
    }
    
    public int findPorClienteMes(DateTime d, String nif)
    {
        DateTime date = d.Date;
        List<Transporte> tr = new List<Transporte>(this.Items);
        List<Transporte> l = tr.FindAll((t) => (t.Cliente.Equals(nif) &&
                                                  (t.FechaSal.Month == date.Month && t.FechaSal.Year == date.Year)));
        return l.Count;
    }
    
    public int findPorClienteAño(DateTime d, String nif)
    {
        DateTime date = d.Date;
        List<Transporte> tr = new List<Transporte>(this.Items);
        List<Transporte> l = tr.FindAll((t) => (t.Cliente.Equals(nif) &&
                                                         t.FechaSal.Year == date.Year));
        return l.Count;
    }

    public List<int> getPorClienteAño(DateTime d, String nif)
    {
        List<int> l = new List<int>();
        for (int i = 0; i <= 12; i++)
        {
            l.Add(findPorClienteMes(d.AddMonths(i), nif));
        }

        return l;
    }

    
    public List<int> getDesdeComienzoCliente(String nif)
    {
        List<int> l = new List<int>();
        DateTime date = getFechaAntigua(nif);
        date = date.Date;
        int a = getFechaNueva(nif).Year - date.Year;
        for (int i = 0; i <= a; i++)
        {
            l.Add(findPorClienteAño(date.AddYears(i), nif));
            Console.WriteLine(date.AddYears(i));
        }

        return l;
    }
    
    public DateTime getFechaAntigua()
    {
        DateTime fechaAnt;
        List<Transporte> tr = new List<Transporte>(this.Items);
        if (this.Count > 0){
            tr.Sort((x, y) => DateTime.Compare(x.FechaSal, y.FechaSal));
            fechaAnt = this[0].FechaSal;

        }
        else
        {
            fechaAnt = DateTime.Today;
        }
        return fechaAnt.Date;
    }
    
    public DateTime getFechaAntigua(String nif)
    {
        DateTime fechaAnt;
        List<Transporte> tr = new List<Transporte>(this.Items);
        tr.Sort((x, y) => DateTime.Compare(x.FechaSal, y.FechaSal));
        
        List<Transporte> t = tr.FindAll((x) => (x.Cliente.Equals(nif)));
        if (t.Count > 0){
            fechaAnt = t[0].FechaSal;
        }
        else
        {
            fechaAnt = DateTime.Today;
        }
        return fechaAnt.Date;
    }
    
    public DateTime getFechaNueva()
    {
        DateTime fechaN;
        List<Transporte> tr = new List<Transporte>(this.Items);
        if (this.Count > 0){
            tr.Sort((x, y) => DateTime.Compare(x.FechaSal, y.FechaSal));
            fechaN = tr[tr.Count-1].FechaSal;
        }
        else
        {
            fechaN = DateTime.Today;
        }
        return fechaN.Date;
    }
    
    public DateTime getFechaNueva(String nif)
    {
        DateTime fechaN;
 
        List<Transporte> tr = new List<Transporte>(this.Items);
        
        tr.Sort((x, y) => DateTime.Compare(x.FechaSal, y.FechaSal));
        List<Transporte> t = tr.FindAll((x) => (x.Cliente.Equals(nif)));
        if (t.Count > 0)
        {
            fechaN = t[t.Count - 1].FechaSal;
        }
        else
        {
            fechaN = DateTime.Today;
        }
        return fechaN.Date;
    }
    
    public List<string> getFechas(DateTime t)
    {
        DateTime date = t.Date;
        List<string> l = new List<string>();
        for (int i = 0; i < 12; i++)
        {
            l.Add(date.AddMonths(i).ToString("dd/MM"));
        }

        return l;
    }
    
    public List<string> getAños()
    {
        DateTime date = this.getFechaAntigua();
        List<string> l = new List<string>();
        int a = getFechaNueva().Year - date.Year;
        for (int i = 0; i <= a; i++)
        {
            l.Add(date.AddYears(i).Year.ToString());
        }

        return l;
    }
        
    


    }
}
