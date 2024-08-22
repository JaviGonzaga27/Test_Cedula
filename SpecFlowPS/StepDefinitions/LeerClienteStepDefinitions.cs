using System;
using TechTalk.SpecFlow;
using proyecto_javier.Data;
using proyecto_javier.Models;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class LeerStepDefinitions
    {
        private readonly ClienteSqlDataPostgres _clienteDataAccessLayer = new ClienteSqlDataPostgres();
        private ClientePS _clienteLeido;

        [Given(@"Tenemos un cliente en la BDD con los siguientes datos")]
        public void GivenTenemosUnClienteEnLaBDDConLosSiguientesDatos(Table table)
        {
            var cliente = table.CreateInstance<ClientePS>();
            _clienteDataAccessLayer.AddCliente(cliente);
        }

        [When(@"Buscamos el cliente por su Cedula ""(.*)""")]
        public void WhenBuscamosElClientePorSuCedula(string cedula)
        {
            _clienteLeido = _clienteDataAccessLayer.GetClienteByCedula(cedula);
        }

        [Then(@"El resultado debe ser")]
        public void ThenElResultadoDebeSer(Table table)
        {
            var clienteEsperado = table.CreateInstance<ClientePS>();
            _clienteLeido.Should().BeEquivalentTo(clienteEsperado);
        }
    }
}