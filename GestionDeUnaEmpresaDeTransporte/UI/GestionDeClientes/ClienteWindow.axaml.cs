using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Layout;

namespace GestionClientesUI
{
    public partial class ClienteWindow : Window
    {
        private RegistroClientes registro = null;
        public ClienteWindow()
        {
            this.registro = ArchivoXML.fromXML();
            InitializeComponent();
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = registro.Clientes;
            var opSalida = this.FindControl<MenuItem>( "Salida" );
            var opRestart = this.FindControl<Button>( "LimpiaBusqueda" );
            var opBorrar = this.FindControl<Button>( "Borrar" );
            var opModificar = this.FindControl<Button>( "Modificar" );
            var opInserta = this.FindControl<MenuItem>( "insertaCliente" );
            var opBusqueda = this.FindControl<MenuItem>( "buscaCliente" );
            dtClientes.VerticalAlignment = VerticalAlignment.Stretch;
            opSalida.Click += (_, _) => this.Close();
            opRestart.Click += (_, _) => dtClientes.Items =  this.registro.Clientes;
            opModificar.Click += (_, _) => this.EditBox(dtClientes.SelectedItem);
            opBorrar.Click += (_, _) => this.Delete(dtClientes.SelectedItem);
            opInserta.Click += (_, _) => this.InsertaBox();
            opBusqueda.Click += (_, _) => this.BusquedaBox();
            


        }

        async void EditBox(object dtClientesSelectedItem)
        {
            Cliente selected = (Cliente)dtClientesSelectedItem;
            EditDialog editDialog = new EditDialog(selected.Nif,selected.Nombre,selected.Tlf,selected.Mail,selected.DirPostal);
            await editDialog.ShowDialog(this);
            this.registro.Modifica(new Cliente(editDialog.Nif,editDialog.Nombre,editDialog.Tlf,editDialog.Mail,editDialog.Postal));
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = registro.Clientes;
        }

        private void Delete(Object row)
        {
            //TODO: Añadir dialogo de confirmacion
            this.registro.Borra((Cliente)row);
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = registro.Clientes;
            
        }
        
        async void BusquedaBox()
        {
            BusquedaDialog busquedaDialog = new BusquedaDialog();
            await busquedaDialog.ShowDialog(this);
            string campoBusqueda = busquedaDialog.CampoBusqueda;
            string valorBusqueda = busquedaDialog.ValorBusqueda;
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            switch (campoBusqueda)
            {
                case "NIF":
                    dtClientes.Items =  new RegistroClientes(this.registro.busquedaPorNIF(valorBusqueda)).Clientes;
                    break;
                case "Nombre": 
                    dtClientes.Items =  new RegistroClientes(this.registro.busquedaPorNombre(valorBusqueda)).Clientes;
                    break;
                case "Tlf": 
                    dtClientes.Items =  new RegistroClientes(this.registro.busquedaPorTLF(valorBusqueda)).Clientes;
                    break;
                case "Mail": 
                    dtClientes.Items =  new RegistroClientes(this.registro.busquedaPorMail(valorBusqueda)).Clientes;
                    break;
                case "Postal": 
                    dtClientes.Items =  new RegistroClientes(this.registro.busquedaPorCodPostal(Int32.Parse(valorBusqueda))).Clientes;
                    break;
            }
        }


        public RegistroClientes RegistroClientes { get; }

        async void InsertaBox()
        {
            InsertDialog insertDialog = new InsertDialog();
            await insertDialog.ShowDialog(this);
            this.registro.Inserta(new Cliente(insertDialog.Nif,insertDialog.Nombre,insertDialog.Tlf,insertDialog.Mail,insertDialog.Postal));
            var dtClientes = this.FindControl<DataGrid>( "DtClients");
            dtClientes.Items = registro.Clientes;
        }
        
    }
}