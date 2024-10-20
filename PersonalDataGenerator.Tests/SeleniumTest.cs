using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using OpenQA.Selenium.Support.UI;

namespace PersonalDataGenerator.Tests
{
    public class SeleniumTest // Pair programmed with Kristian Mortensen
    {
        [Fact]
        public void Test_Google_Search()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("http://localhost:5268/");
                Thread.Sleep(1000);

                driver.FindElement(By.Id("chkPartialOptions")).Click(); //Klikker "Partial generation radio button"
                driver.FindElement(By.Id("frmGenerate")).Submit(); // Genererer CRP data
                Thread.Sleep(1000);

                new SelectElement(driver.FindElement(By.Id("cmbPartialOptions"))).SelectByIndex(1); // Vælger "Name and Gender" i dropdown  
                driver.FindElement(By.Id("frmGenerate")).Submit();
                Thread.Sleep(1000);

                new SelectElement(driver.FindElement(By.Id("cmbPartialOptions"))).SelectByIndex(2); // Vælger "Name, Gender and Birthday" i dropdown  
                driver.FindElement(By.Id("frmGenerate")).Submit();
                Thread.Sleep(1000);

                new SelectElement(driver.FindElement(By.Id("cmbPartialOptions"))).SelectByIndex(3); // Vælger "CPR, Name and Gender" i dropdown  
                driver.FindElement(By.Id("frmGenerate")).Submit();
                Thread.Sleep(1000);

                new SelectElement(driver.FindElement(By.Id("cmbPartialOptions"))).SelectByIndex(4); // Vælger "CPR, Name, Gender and Birthday" i dropdown  
                driver.FindElement(By.Id("frmGenerate")).Submit();
                Thread.Sleep(1000);

                new SelectElement(driver.FindElement(By.Id("cmbPartialOptions"))).SelectByIndex(5); // Vælger "Address" i dropdown  
                driver.FindElement(By.Id("frmGenerate")).Submit();
                Thread.Sleep(1000);

                new SelectElement(driver.FindElement(By.Id("cmbPartialOptions"))).SelectByIndex(6); // Vælger "Phonenumber" i dropdown  
                driver.FindElement(By.Id("frmGenerate")).Submit();
                Thread.Sleep(1000);

                driver.FindElement(By.Id("chkPerson")).Click(); //Klikker "Person" radio button
                driver.FindElement(By.Id("frmGenerate")).Submit(); // Genererer 1 hel person
                Thread.Sleep(1000);

                driver.FindElement(By.Id("txtNumberPersons")).Clear(); // Sletter antal personer
                driver.FindElement(By.Id("txtNumberPersons")).SendKeys("50"); // Indtaster 50 personer
                driver.FindElement(By.Id("frmGenerate")).Submit(); // Genererer 50 personer
                Thread.Sleep(1000);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // Smooth scroll down by 5000 pixels
                js.ExecuteScript("window.scrollBy({ top: 5000, behavior: 'smooth' });");

                Thread.Sleep(5000);


            }
        }
    }
}