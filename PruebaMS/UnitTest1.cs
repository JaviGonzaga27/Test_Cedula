using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MsTestPS
{
    [TestClass]
    public class UnitTest1
    {
        //Se asocia en una variable el id de la barra de buscador de google mediante la inspeccion
        By googleSearchBar = By.Id("APjFqb");
        //Si no se encuentra o el elemento no posee el id, se busca o implementa por el nombre
        By googleSearchButton = By.Name("btnK");

       // By googleResultText = By.XPath("");
        
        private readonly IWebDriver driver;
        int time = 2000;
        public UnitTest1()
        {
            //driver.Quit();
           // driver.Dispose();
        }

       // [TestMethod]
      //  public void TestMethod1()
      //  {
      //      
       //     IWebDriver driver = new ChromeDriver();
       //     driver.Manage().Window.Maximize();
       //     driver.Navigate().GoToUrl("https://www.google.com");
       //     Thread.Sleep(time);
       //     driver.FindElement(googleSearchBar).SendKeys("Visual Studio Code");
       //     Thread.Sleep(time);
       //     driver.FindElement(googleSearchButton).Click();

          //  var result = driver.FindElement(googleResultText);
          //  Assert.IsNotNull(result);
           // driver.Quit();
            //Assert.IsTrue(result.Text.Equals("Titulo Esperado");
        //}

        [TestMethod]
        public void Create_Get_ReturnValue()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7129/Postgres/Create");
            driver.FindElement(By.Name("Cedula")).SendKeys("Gonzalo");
            driver.FindElement(By.Name("Apellidos")).SendKeys("Cardenas");
            driver.FindElement(By.Name("Nombres")).SendKeys("Gonzalo");
            driver.FindElement(By.Name("FechaNacimiento")).SendKeys("29-05-1998");
            driver.FindElement(By.Name("Mail")).SendKeys("daniel@info.com");
            driver.FindElement(By.Name("Telefono")).SendKeys("0999201044");
            driver.FindElement(By.Name("Direccion")).SendKeys("Av.Solanda");
            driver.FindElement(By.TagName("btn btn-success btn-sm"));
        }

    }
}