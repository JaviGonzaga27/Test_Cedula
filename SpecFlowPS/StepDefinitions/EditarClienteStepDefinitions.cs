using System;
using TechTalk.SpecFlow;
using proyecto_javier.Data;
using proyecto_javier.Models;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class EditarStepDefinitions
    {
        private readonly ClienteSqlDataPostgres _clienteDataAccessLayer = new ClienteSqlDataPostgres();
        private ClientePS _clienteEditado;

        [Given(@"Existe un cliente en la BDD con los siguientes datos")]
        public void GivenExisteUnClienteEnLaBDDConLosSiguientesDatos(Table table)
        {
            var cliente = table.CreateInstance<ClientePS>();
            _clienteDataAccessLayer.AddCliente(cliente);
        }

        [When(@"Editamos los datos del cliente con Cedula ""(.*)""")]
        public void WhenEditamosLosDatosDelClienteConCedula(string cedula, Table table)
        {
            var clienteActualizado = table.CreateInstance<ClientePS>();
            _clienteDataAccessLayer.UpdateClienteByCedula(clienteActualizado);
        }

        [Then(@"El resultado de la búsqueda del cliente editado debe ser")]
        public void ThenElResultadoDeLaBusquedaDelClienteEditadoDebeSer(Table table)
        {
            var clienteEsperado = table.CreateInstance<ClientePS>();
            _clienteEditado = _clienteDataAccessLayer.GetClienteByCedula(clienteEsperado.Cedula);
            _clienteEditado.Should().BeEquivalentTo(clienteEsperado, options =>
                options.Excluding(c => c.Codigo)); // Excluimos Codigo de la comparación
        }
    }
}
