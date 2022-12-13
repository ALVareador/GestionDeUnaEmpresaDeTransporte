using System.Xml;
using System.Xml.Linq;

namespace  GestionDeUnaEmpresaDeTransporte.Core.GestionDeClientes;

public class ArchivoXML
{
    private const string EtqRegistro = "clientes";
    private const string EtqCliente = "cliente";
    private const string EtqNIF = "nif";
    private const string EtqNombre = "nombre";
    private const string EtqTlf = "tlf";
    private const string EtqMail = "mail";
    private const string EtqDirPostal = "dirPostal";

    public ArchivoXML(RegistroClientes registro)
    {
        this.RegistroClientes = registro;
    }

    public RegistroClientes RegistroClientes { get;}

    public void toXML(string nombreArchivo)
    {
        XElement raiz = new XElement(EtqRegistro);

        foreach (var cliente in this.RegistroClientes.Clientes)
        {
            XElement elemento = new XElement(EtqCliente,
                new XElement(EtqNIF, cliente.Nif), new XElement(EtqNombre, cliente.Nombre),
                new XElement(EtqTlf, cliente.Tlf),
                new XElement(EtqMail, cliente.Mail), new XElement(EtqDirPostal, cliente.DirPostal));
            raiz.Add(elemento);
        }
        raiz.Save(nombreArchivo);
    }

    public static RegistroClientes fromXML()
    {
        XElement raiz = XElement.Load("clientes.xml");

        if (!raiz.HasElements)
        {
            throw new XmlException("El archivo no contiene datos");
        }

        RegistroClientes toret = new RegistroClientes();

        foreach (var cliente in raiz.Elements(EtqCliente))
        {
            string nif = (string)cliente.Element(EtqNIF);
            string nombre = (string)cliente.Element(EtqNombre);
            string tlf = (string)cliente.Element(EtqTlf);
            string mail = (string)cliente.Element(EtqMail);
            int dirPostal = (int)cliente.Element(EtqDirPostal);

            if (nif==null || nombre==null || tlf == null || mail == null || dirPostal == null)
            {
                throw new XmlException("Los datos del archivo son incorrectos");
            }
            
            toret.Inserta(new Cliente(nif,nombre,tlf,mail,dirPostal));
        }

        return toret;
    }

}