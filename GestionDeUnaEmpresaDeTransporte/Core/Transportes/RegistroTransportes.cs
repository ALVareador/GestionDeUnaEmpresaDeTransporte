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
            this.Eliminar(position + 1);
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
        




    }
}
