using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ProyDIA.Core
{
    public class XmlRegistroTransportes
    {
        public const string ArchivoXml = "transportes.xml";
        const string EtqTransportes = "transportes";

        public XmlRegistroTransportes(RegistroTransportes rt)
        {
            this.RegistroTransportes = rt;
        }

        /// <summary>
        /// Guarda la lista de transportes como xml
        /// </summary>
        public void GuardaXml()
        {
            this.GuardaXml(ArchivoXml);
        } 

        /// <summary>
        /// Guarda la lista de transportes como xml
        /// </summary>
        /// <param name="filename">El nombre del archivo</param>
        public void GuardaXml(string filename)
        {
            var root = new XElement(EtqTransportes);

            foreach(Transporte transporte in this.RegistroTransportes)
            {
                root.Add(new XmlTransporte(transporte).ToXml());
            }

            root.Save(filename);
        }

        public RegistroTransportes RegistroTransportes { get; }


        /// <summary>
        /// Recupera los transportes desde un archivo XML
        /// </summary>
        /// <param name="filename">El nombre del archivo</param>
        /// <returns>Un <see cref="RegistroTransportes"/> con los <see cref="Transporte"/>'s</returns>
        public static RegistroTransportes RecuperaXml(string filename)
        {
            var toret = new RegistroTransportes();

            try
            {
                var doc = XDocument.Load(filename);
                var transportes = doc.Element(EtqTransportes)?.Elements(XmlTransporte.EtqTransporte);

                if(transportes != null)
                {
                    foreach(XElement node in transportes)
                    {
                        toret.Add(XmlTransporte.FromXml(node));
                    }
                }
            }
            catch(XmlException)
            {
                toret.Clear();
            }
            catch(IOException)
            {
                toret.Clear();
            }

            return toret;
        }


        /// <summary>
        /// Crea un registro de transportes con la lista de transportes recuperada del archivo por defecto
        /// </summary>
        /// <returns>Un <see cref="RegistroTransportes"/></returns>
        public static RegistroTransportes RecuperaXml()
        {
            return RecuperaXml(ArchivoXml);
        }
    }
}
