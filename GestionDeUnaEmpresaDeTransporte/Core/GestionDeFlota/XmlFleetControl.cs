using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using fleetControlAvalonia;

namespace ControlFlota.Core;

public class XmlFleetControl
{
    public const string ArchivoXml = "flota.xml";
        const string EtqViajes = "viajes";

        public XmlFleetControl(FleetControl rv)
        {
            this.fleetControl = rv;
        }

        public void GuardaXml()
        {
            GuardaXml(ArchivoXml);
        }

        public void GuardaXml(string fn)
        {
            var save = new XElement("vehicles");
            foreach (var i in fleetControl)
            {
                save.Add(new XElement("vehicle",
                    new XAttribute("type", i.GetType().ToString()),
                    new XAttribute("brand", i.brand),
                    new XAttribute("license", i.license),
                    new XAttribute("modelName", i.model),
                    new XAttribute("fuel", i.fuelPerKM),
                    new XAttribute("adquisition", i.adqDate.ToString()),
                    new XAttribute("fabrication", i.fabrDate.ToString()),
                    new XAttribute("wifi", i.wifi),
                    new XAttribute("bluetooth", i.bluetooth),
                    new XAttribute("AC", i.ac),
                    new XAttribute("beds", i.bed),
                    new XAttribute("TV", i.tv)
                ));
            }
            save.Save(fn);
        }

        public FleetControl fleetControl {
            get;
        }
        
		public static FleetControl RecuperaXml(string f)
		{
			var toret = new FleetControl();
            
            try {
                XDocument save = XDocument.Load(f);

                IEnumerable<XElement> xmlList = save.Element("vehicles").Elements();
                foreach (var node in xmlList)
                { 
                    switch((string) node.Attribute("type")){
                        case "fleetControlAvalonia.Van":
                            toret.Add(new Van((string) node.Attribute("brand"), (string) node.Attribute("license"), (string) node.Attribute("modelName"), (float) node.Attribute("fuel"), DateOnly.Parse((string) node.Attribute("adquisition")), DateOnly.Parse((string) node.Attribute("fabrication")), (bool) node.Attribute("wifi"), (bool) node.Attribute("bluetooth"), (bool) node.Attribute("AC"), (bool) node.Attribute("beds"), (bool) node.Attribute("TV")));
                            break;
                        case "fleetControlAvalonia.Truck":
                            toret.Add(new Truck((string) node.Attribute("brand"), (string) node.Attribute("license"), (string) node.Attribute("modelName"), (float) node.Attribute("fuel"), DateOnly.Parse((string) node.Attribute("adquisition")), DateOnly.Parse((string) node.Attribute("fabrication")), (bool) node.Attribute("wifi"), (bool) node.Attribute("bluetooth"), (bool) node.Attribute("AC"), (bool) node.Attribute("beds"), (bool) node.Attribute("TV")));
                            break;
                        case "fleetControlAvalonia.Artruck":
                            toret.Add(new Artruck((string) node.Attribute("brand"), (string) node.Attribute("license"), (string) node.Attribute("modelName"), (float) node.Attribute("fuel"), DateOnly.Parse((string) node.Attribute("adquisition")), DateOnly.Parse((string) node.Attribute("fabrication")), (bool) node.Attribute("wifi"), (bool) node.Attribute("bluetooth"), (bool) node.Attribute("AC"), (bool) node.Attribute("beds"), (bool) node.Attribute("TV")));
                            break;
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
        
		public static FleetControl RecuperaXml()
		{
			return RecuperaXml( ArchivoXml );
		}
}