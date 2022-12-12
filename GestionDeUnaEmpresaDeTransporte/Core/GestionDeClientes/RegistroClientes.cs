using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionClientesUI;

public class RegistroClientes : IEnumerable<Cliente>
{
    private List<Cliente> clientes;

    public RegistroClientes()
    {
        this.clientes = new List<Cliente>();
    }

    public RegistroClientes(IEnumerable<Cliente> clientes) : this()
    {
        this.clientes.AddRange(clientes);
    }

    public IList<Cliente> Clientes => new List<Cliente>(this.clientes);

    public void Inserta(Cliente nuevo)
    {
        this.clientes.Add(nuevo);
    }

    public void Borra(Cliente borrar)
    {
        this.clientes.Remove(borrar);
    }

    public void Modifica(Cliente modificar)
    {
        int mod = this.clientes.FindLastIndex(x => x.Nif == modificar.Nif);
        this.clientes[mod] = modificar;
    }

    public int Total()
    {
        return this.clientes.Count;
    }
    
    //TODO: Hacer los findBy usando LINQ
    public IEnumerable<Cliente> busquedaPorNIF(String nif)
    {
        return this.clientes.Where(x => x.Nif == nif);
    }

    public IEnumerable<Cliente> busquedaPorNombre(String nombre)
    {
        return this.clientes.Where(x => x.Nombre == nombre);
    }

    public IEnumerable<Cliente> busquedaPorTLF(String tlf)
    {
        return this.clientes.Where(x => x.Tlf == tlf);
    }

    public IEnumerable<Cliente> busquedaPorMail(String mail)
    {
        return this.clientes.Where(x => x.Mail == mail);
    }

    public IEnumerable<Cliente> busquedaPorCodPostal(int codPostal)
    {
        return this.clientes.Where(x => x.DirPostal == codPostal);
    }

    public IEnumerator<Cliente> GetEnumerator()
    {
        foreach(var Cliente in clientes) {
            yield return Cliente;
        }

    }

    public override string ToString()
    {
        StringBuilder toret = new StringBuilder();
        toret.AppendLine("Registro de clientes:");
        foreach (var cliente in clientes)
        {
            toret.AppendLine(cliente.ToString());
        }

        return toret.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}