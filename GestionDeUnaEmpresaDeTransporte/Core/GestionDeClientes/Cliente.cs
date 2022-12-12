using System;

namespace GestionClientesUI;

public class Cliente
{
    public Cliente(string nif, string nombre, string tlf, string mail, int dir_postal)
    {
        this.Nif = nif;
        this.Nombre = nombre;
        this.Tlf = tlf;
        this.Mail = mail;
        this.DirPostal = dir_postal;
    }

    public string Nif { get; }
    public string Nombre { get; }
    public string Tlf { get; }
    public string Mail { get; }
    public int DirPostal { get; }
    public override string ToString()
    {
        return String.Format("{0}: NIF: {1}, TLF: {2}, MAIL: {3}, COD_POSTAL: {4}", Nombre, Nif, Tlf, Mail, DirPostal);
    }
}