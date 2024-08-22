using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;

namespace PruebaMS
{
    [TestClass]
    public class UnitTest2
    {
        private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }


        //Prueba 1 Navegar y Crear
        [TestMethod]
        public void AddCliente()
        {
            driver.Navigate().GoToUrl("https://localhost:7129");
            int time = 3000;
            Thread.Sleep(time);
            //Para saber si se cargo bien la página
            IWebElement tituloElement = driver.FindElement(By.TagName("h1"));

            if (tituloElement.Text.Contains("Welcome"))
            {
                driver.FindElement(By.LinkText("Postgres")).Click();
                Thread.Sleep(time);
                driver.FindElement(By.LinkText("Crear Clientes")).Click();
                Thread.Sleep(time);
                driver.FindElement(By.Name("Cedula")).SendKeys("1726623083");
                driver.FindElement(By.Name("Apellidos")).SendKeys("Cardenas");
                driver.FindElement(By.Name("Nombres")).SendKeys("Gonzalo");
                driver.FindElement(By.Name("FechaNacimiento")).SendKeys("29-05-1998");
                driver.FindElement(By.Name("Mail")).SendKeys("daniel@info.com");
                driver.FindElement(By.Name("Telefono")).SendKeys("0999201044");
                driver.FindElement(By.Name("Direccion")).SendKeys("Av.Solanda");
                //Para encontrar el CheckBox
                IWebElement checkbox = driver.FindElement(By.Name("Estado"));
                if (!checkbox.Selected)
                {
                    checkbox.Click();
                }
                Thread.Sleep(time);
                driver.FindElement(By.CssSelector("button.btn-success.btn-sm")).Click();
            }
            else
            {
                //Tambien se puede ver el manejo de errores y que los puede provocar
                Assert.Fail("El título h1 no contiene 'Welcome'");
            }
            Thread.Sleep(time);
            // Buscar la fila que contiene los datos del cliente
            var filas = driver.FindElements(By.TagName("tr"));

            bool encontrado = false;
            foreach (var fila in filas)
            {
                var celdas = fila.FindElements(By.TagName("td"));
                if (celdas.Count > 0 &&
                    celdas[1].Text.Contains("1726623083") &&
                    celdas[2].Text.Contains("Cardenas") &&
                    celdas[3].Text.Contains("Gonzalo") &&
                    celdas[4].Text.Contains("29/05/1998 0:00:00") &&
                    celdas[5].Text.Contains("daniel@info.com") &&
                    celdas[6].Text.Contains("0999201044") &&
                    celdas[7].Text.Contains("Av.Solanda"))
                {
                    // Buscar el checkbox en la celda esperada
                    IWebElement foundCheckbox = celdas[8].FindElement(By.TagName("input"));

                    // Verificar que el elemento es un checkbox
                    if (foundCheckbox.GetAttribute("type") == "checkbox")
                    {
                        // Verificar si el checkbox está seleccionado
                        bool isChecked = foundCheckbox.Selected;
                        if (isChecked)
                        {
                            encontrado = true;
                            Console.WriteLine("El cliente se ha guardado correctamente y el checkbox está marcado.");
                        }
                        else
                        {
                            Assert.Fail("El checkbox en la fila especificada no está marcado.");
                        }
                        break;
                    }
                    else
                    {
                        Assert.Fail("El elemento en la celda especificada no es un checkbox.");
                    }
                }
            }

            if (encontrado)
            {
                Console.WriteLine("El cliente se ha guardado correctamente.");
            }
            else
            {
                Assert.Fail("El cliente no aparece en la lista.");
            }
            TearDown();

        }

        //Prueba 2 Navegar y Ver Detalles

        [TestMethod]
        public void Details()
        {
            driver.Navigate().GoToUrl("https://localhost:7129");
            int time = 3000;
            Thread.Sleep(time);
            //Para saber si se cargo bien la página
            IWebElement tituloElement = driver.FindElement(By.TagName("h1"));

            if (tituloElement.Text.Contains("Welcome"))
            {
                driver.FindElement(By.LinkText("Postgres")).Click();
                Thread.Sleep(time);
                var filas = driver.FindElements(By.TagName("tr"));

                bool encontrado = false;
                foreach (var fila in filas)
                {
                    var celdas = fila.FindElements(By.TagName("td"));
                    if (celdas.Count > 0 &&
                         celdas[0].Text.Contains("2"))
                    {
                        IWebElement botonVer = celdas[9].FindElement(By.XPath(".//a[contains(text(), 'Ver')]"));

                        if (botonVer != null)
                        {
                            botonVer.Click();
                            encontrado = true;
                            Thread.Sleep(time);
                            break; 
                        }
                        else
                        {
                            Assert.Fail("No se encontró el botón 'Ver' en la celda.");
                        }
                    }
                }

                if (!encontrado)
                {
                    Assert.Fail("No se encontró la fila con el identificador '2'.");
                }
            }
            else
            {
                Assert.Fail("El título de la página no contiene 'Welcome'.");
            }

            TearDown();

        }
        //Prueba 3 Navegar y Editar

        [TestMethod]
        public void Edit()
        {
            driver.Navigate().GoToUrl("https://localhost:7129");
            int time = 3000;
            Thread.Sleep(time);

            // Verificar que la página se ha cargado correctamente
            IWebElement tituloElement = driver.FindElement(By.TagName("h1"));

            if (tituloElement.Text.Contains("Welcome"))
            {
                driver.FindElement(By.LinkText("Postgres")).Click();
                Thread.Sleep(time);

                var filas = driver.FindElements(By.TagName("tr"));
                string Codigo = "2";
                bool encontrado = false;
                foreach (var fila in filas)
                {
                    var celdas = fila.FindElements(By.TagName("td"));
                    if (celdas.Count > 0 &&
                         celdas[0].Text.Contains(Codigo)) 
                    {
                        // Buscar y hacer clic en el botón de "Editar"
                        IWebElement botonEditar = celdas[9].FindElement(By.XPath(".//a[contains(text(), 'Editar')]"));

                        if (botonEditar != null)
                        {
                            botonEditar.Click();
                            encontrado = true;
                            Thread.Sleep(time);

                            // Editar los campos del formulario
                            driver.FindElement(By.Name("Cedula")).Clear();
                            driver.FindElement(By.Name("Cedula")).SendKeys("1726623083");
                            driver.FindElement(By.Name("Apellidos")).Clear();
                            driver.FindElement(By.Name("Apellidos")).SendKeys("NuevoApellido");
                            driver.FindElement(By.Name("Nombres")).Clear();
                            driver.FindElement(By.Name("Nombres")).SendKeys("NuevoNombre");
                            driver.FindElement(By.Name("FechaNacimiento")).Clear();
                            driver.FindElement(By.Name("FechaNacimiento")).SendKeys("30-06-1990");
                            driver.FindElement(By.Name("Mail")).Clear();
                            driver.FindElement(By.Name("Mail")).SendKeys("nuevo@info.com");
                            driver.FindElement(By.Name("Telefono")).Clear();
                            driver.FindElement(By.Name("Telefono")).SendKeys("0999200000");
                            driver.FindElement(By.Name("Direccion")).Clear();
                            driver.FindElement(By.Name("Direccion")).SendKeys("NuevaDireccion");

                            // Desmarcar el checkbox si no está marcado
                            IWebElement checkbox = driver.FindElement(By.Name("Estado"));
                            if (checkbox.Selected)
                            {
                                checkbox.Click();
                            }

                            // Guardar los cambios
                            driver.FindElement(By.CssSelector("button.btn-success.btn-sm")).Click();
                            Thread.Sleep(time);
                            break;
                        }
                        else
                        {
                            Assert.Fail("No se encontró el botón 'Editar' en la celda.");
                        }
                    }
                }

                if (!encontrado)
                {
                    Assert.Fail("No se encontró la fila con el identificador '2'.");
                }
            }
            else
            {
                Assert.Fail("El título de la página no contiene 'Welcome'.");
            }

            TearDown();
        }




        //Prueba 4 Navegar y Eliminar

        [TestMethod]
        public void Delete()
        {
            driver.Navigate().GoToUrl("https://localhost:7129");
            int time = 3000;
            Thread.Sleep(time);
            //Para saber si se cargo bien la página
            IWebElement tituloElement = driver.FindElement(By.TagName("h1"));
            string numeroCodigo = "12"; 
            if (tituloElement.Text.Contains("Welcome"))
            {
                driver.FindElement(By.LinkText("Postgres")).Click();
                Thread.Sleep(time);
                var filas = driver.FindElements(By.TagName("tr"));

                bool encontrado = false;
                foreach (var fila in filas)
                {
                    var celdas = fila.FindElements(By.TagName("td"));
                    if (celdas.Count > 0 &&
                         celdas[0].Text.Contains(numeroCodigo))
                    {
                        IWebElement botonEliminar = celdas[9].FindElement(By.XPath(".//a[contains(text(), 'Eliminar')]"));

                        if (botonEliminar != null)
                        {
                            botonEliminar.Click();
                            encontrado = true;
                            Thread.Sleep(time);
                            driver.FindElement(By.CssSelector("button.btn.btn-danger")).Click();
                            Thread.Sleep(time);
                            break;
                        }
                        else
                        {
                            Assert.Fail("No se encontró el botón 'Eliminar' en la celda.");
                        }
                    }
                }

                if (!encontrado)
                {
                    Assert.Fail("No se encontró la fila con el identificador .");
                }
            }
            else
            {
                Assert.Fail("El título de la página no contiene 'Welcome'.");
            }

           TearDown();

        }



        [TestCleanup]
        public void TearDown()
        {
            // Cierra el navegador después de la prueba
            driver.Quit();
        }
    }
}
