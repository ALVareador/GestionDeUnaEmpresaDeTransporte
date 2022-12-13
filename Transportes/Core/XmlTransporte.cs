using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProyDIA.Core
{
    /// <summary>
    /// Crea y recupera XML para un objeto <see cref="Transporte"/> dado
    /// </summary>
    public class XmlTransporte
    {
        /// <summary>
        /// Etiqueta XML principal para un transporte
        /// </summary>
        public const string EtqTransporte = "transporte";
        const string EtqCliente = "cliente";
        const string EtqMatricula = "matricula";
        const string EtqTipo = "tipo";
        const string EtqKms = "kilometros";
        const string EtqFechaContr = "fecha_contrato";
        const string EtqFechaSal = "fecha_salida";
        const string EtqFechaEntre = "fecha_entrega";
        const string EtqSueldo = "sueldo_hora";
        const string EtqPrecioLitro = "precio_litro";
        const string EtqCantLtKm = "litro_por_km";
        const string EtqPrecioFinal = "precio_final";


        /// <summary>
        /// Crea un XMLTransporte para un <see cref="Transporte"/> dado por parámetro
        /// </summary>
        /// <param name="t">El <see cref="Transporte"/> del que crear el XML</param>
        public XmlTransporte(Transporte t)
        {
            this.Transporte = t;
        }


        /// <summary>
        /// Crea un objeto XML con la información del transporte
        /// </summary>
        /// <returns>Un <see cref="XElement"/> con la información del <see cref="Transporte"/></returns>
        public XElement ToXml()
        {
            return new XElement(EtqTransporte,
                new XAttribute(EtqCliente, this.Transporte.Cliente),
                new XAttribute(EtqMatricula, this.Transporte.Matricula),
                new XAttribute(EtqTipo, this.Transporte.Tipo),
                new XAttribute(EtqKms, this.Transporte.Kms),
                new XAttribute(EtqFechaContr, this.Transporte.FechaContra),
                new XAttribute(EtqFechaSal, this.Transporte.FechaSal),
                new XAttribute(EtqFechaEntre, this.Transporte.FechaEntre),
                 new XAttribute(EtqSueldo, this.Transporte.SueldoHora),
                  new XAttribute(EtqPrecioLitro, this.Transporte.PrecioLitro),
                   new XAttribute(EtqCantLtKm, this.Transporte.CantLtKms),
                new XAttribute(EtqPrecioFinal, this.Transporte.PrecioFinal));
        }


        /// <summary>
        /// El <see cref="Transporte"/> sobre el que actuar
        /// </summary>
        public Transporte Transporte { get; }

        /// <summary>
        /// Recupera un <see cref="Transporte"/> desde un <see cref="XElement"/>
        /// </summary>
        /// <param name="node">Un objeto <see cref="XElement"> del que extraer la info</param>
        /// <returns>Un <see cref="Transporte"/> con los datos obtenidos</returns>
        /// <seealso cref="ToXml"/>
        public static Transporte FromXml(XElement node)
        {
            int id = Convert.ToInt32(
                (string?)node.Element(EtqTransporte) ?? "0",
                CultureInfo.InvariantCulture);
            string cliente = (string?)node.Attribute(EtqCliente) ?? "CLI";
            string matricula = (string?)node.Attribute(EtqMatricula) ?? "MAT";
            string tipo = (string?)node.Attribute(EtqTipo) ?? "TIPO";
            double kms = Convert.ToDouble(
                (string?)node.Element(EtqKms) ?? "0",
                CultureInfo.InvariantCulture);
            DateTime fechaCont = (DateTime?)node.Attribute(EtqFechaContr) ?? DateTime.Now;
            DateTime fechaSal = (DateTime?)node.Attribute(EtqFechaSal) ?? DateTime.Now;
            DateTime fechaEntre = (DateTime?)node.Attribute(EtqFechaEntre) ?? DateTime.Now;
            double sueldo = Convert.ToDouble(
                (string?)node.Element(EtqSueldo) ?? "0",
                CultureInfo.InvariantCulture);
            double preciolitro = Convert.ToDouble(
                (string?)node.Element(EtqPrecioLitro) ?? "0",
                CultureInfo.InvariantCulture);
            double cantltkm = Convert.ToDouble(
                (string?)node.Element(EtqCantLtKm) ?? "0",
                CultureInfo.InvariantCulture);
            double preciofinal = Convert.ToDouble(
                (string?)node.Element(EtqPrecioFinal) ?? "0",
                CultureInfo.InvariantCulture);

            var toret =  new Transporte(matricula, tipo, cliente, fechaCont, kms, fechaSal, fechaEntre);
            toret.SueldoHora = sueldo;
            toret.PrecioLitro = preciolitro;
            toret.CantLtKms = cantltkm;
            toret.Update();
            return toret;
        }
    }
}
