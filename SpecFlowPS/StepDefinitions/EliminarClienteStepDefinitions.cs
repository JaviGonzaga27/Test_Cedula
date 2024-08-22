using System;
using TechTalk.SpecFlow;
using proyecto_javier.Data;
using proyecto_javier.Models;
using FluentAssertions;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class EliminarStepDefinitions
    {
        private readonly ClienteSqlDataPostgres _clienteDataAccessLayer = new ClienteSqlDataPostgres();
        private ClientePS _clienteEliminado;

        [Given(@"Existe un cliente en la BDD con Cedula ""(.*)""")]
        public void GivenExisteUnClienteEnLaBDDConCedula(string cedula)
        {
            var cliente = new ClientePS
            {
                Cedula = cedula,
                Apellidos = "Apellido Prueba",
                Nombres = "Nombre Prueba",
                FechaNacimiento = DateTime.Now,
                Mail = "prueba@mail.com",
                Telefono = "123456789",
                Direccion = "Dirección Prueba",
                Estado = true,
                Saldo = 0
            };
            _clienteDataAccessLayer.AddCliente(cliente);
        }

        [When(@"Eliminamos el cliente con Cedula ""(.*)""")]
        public void WhenEliminamosElClienteConCedula(string cedula)
        {
            _clienteDataAccessLayer.DeleteCliente(cedula);
        }

        [Then(@"El cliente no debe existir en la BDD")]
        public void ThenElClienteNoDebeExistirEnLaBDD()
        {
            // Este paso se verifica en el siguiente
        }

        [Then(@"Buscamos el cliente por su Cedula ""(.*)""")]
        public void ThenBuscamosElClientePorSuCedula(string cedula)
        {
            _clienteEliminado = _clienteDataAccessLayer.GetClienteByCedula(cedula);
        }

        [Then(@"El resultado de la búsqueda debe ser nulo")]
        public void ThenElResultadoDeLaBusquedaDebeSerNulo()
        {
            _clienteEliminado.Should().BeNull();
        }
    }
}