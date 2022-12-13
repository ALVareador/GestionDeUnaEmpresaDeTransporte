using System;

namespace GestionDeUnaEmpresaDeTransporte.Core.Transportes
{
    public class Transporte {
        public Transporte(string matricula, string tipo, string cliente, 
            DateTime fechaContra, double kms, DateTime fechaSal, 
            DateTime fechaEntre)
        {
            this.Matricula = matricula;
            this.Tipo = tipo;
            this.Cliente = cliente;
            this.FechaContra = fechaContra;
            this.Kms = kms;
            this.FechaSal = fechaSal;
            this.FechaEntre = fechaEntre;
            this.Id = String.Concat(matricula, fechaContra.ToString("yyyyMMdd"));
        }

        public void Update()
        {
            this.ImporteDia = this.precioDia(this.SueldoHora);
            this.ImporteKm = this.precioKm(this.PrecioLitro, this.CantLtKms);
            this.Gas = this.gas(this.Kms, this.CantLtKms);
            this.IvaAplicado = ivaAplicado();
            this.PrecioFinal = this.IvaAplicado + this.precioTotal();
        }

        private const double IvaPorcentaje = (double)21/100;
        public string Id {
            get;
            private set;
        }
        public string Tipo {
            get; set; 
        }
        public string Cliente {
            get; set; 
        }
        public DateTime FechaContra {
            get; set; 
        }
        public double Kms {
            get; set; 
        }
        public DateTime FechaSal {
            get; set; 
        }
        public DateTime FechaEntre {
            get; set; 
        } 
        public double ImporteDia
        {
            get; set;
        }
        public double ImporteKm {
            get; set; 
        }

        public double Gas
        {
            get;
            set;
        }
        public double IvaAplicado {
            get; 
            set; 
        }

        public double PrecioFinal
        {
            get; private set;
        }

        public string Matricula
        {
            get; set;
        }

        public double SueldoHora
        {
            get; set;
        }

        public double PrecioLitro
        {
            get; set; 
        }

        public double CantLtKms
        {
            get; set; 
        }

        public double precioDia(double sueldoHora)
        {
            return sueldoHora * 8;
        }

        public int cantDias()
        {
            TimeSpan difFechas = this.FechaEntre - this.FechaSal;
            return difFechas.Days;
        }

        public double precioKm(double precioLitro, double cantLtKms)
        {
            double pCombKm = precioLitro * cantLtKms;
            return pCombKm * 3;
        }

        public double gas(double kms, double cantLtKms)
        {
            return  kms * cantLtKms;
        }

        public double precioTotal()
        {
            double toret;
            int numDias = this.cantDias();
            int suplencia=1;
            if (numDias > 1)
            {
                suplencia = 2;
            }
            
            toret = (numDias * this.ImporteDia * suplencia) + (this.Kms*this.ImporteKm) + this.Gas;
            return toret;
        }

        public double ivaAplicado()
        {
            return precioTotal() * IvaPorcentaje;
        }

        public void setImporteKm(double precioLitro, double cantLtKms)
        {
            this.ImporteKm = this.precioKm(precioLitro, cantLtKms);
        }

        public void setGas(double kms, double cantLtKms)
        {
            this.Gas = this.gas(kms, cantLtKms);
        }

       
    }
}