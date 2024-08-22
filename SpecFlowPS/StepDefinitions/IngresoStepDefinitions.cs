using System;
using TechTalk.SpecFlow;
using proyecto_javier.Data;
using proyecto_javier.Models;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class IngresoStepDefinitions
    {
        private readonly ClienteSqlDataPostgres _clienteDataAccessLayer = new ClienteSqlDataPostgres();
        [Given(@"Seleccionar los Datos para ingresar en la BDD")]
        public void GivenSeleccionarLosDatosParaIngresarEnLaBDD(Table table)
        {
            var resultado = table.Rows.Count();
            resultado.Should().BeGreaterThanOrEqualTo(1);
        }

        [When(@"Ingresamos el registro a la BDD")]
        public void WhenIngresamosElRegistroALaBDD(Table table)
        {
            var cliente = table.CreateSet<ClientePS>().ToList();
            ClientePS clienteps = new ClientePS();
            foreach (var item in cliente)
            {
                clienteps.Cedula = item.Cedula;
                clienteps.Apellidos = item.Apellidos;
                clienteps.Nombres = item.Nombres;
                clienteps.FechaNacimiento = item.FechaNacimiento;
                clienteps.Mail = item.Mail;
                clienteps.Telefono = item.Telefono;
                clienteps.Direccion = item.Direccion;
                clienteps.Estado = item.Estado;
            }

            _clienteDataAccessLayer.AddCliente(clienteps);
        }

        [Then(@"Resultado del registro ingresado a la BDD")]
        public void ThenResultadoDelRegistroIngresadoALaBDD(Table table)
        {
            var resultado = table.Rows.Count();
            resultado.Should().BeGreaterThanOrEqualTo(1);
        }
    }
}
