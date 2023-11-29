using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;



namespace selenium_bankxyz
{
    public class UnitTest1
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            //create instance of chrome driver and wait 5 seconds for page to load
            
            driver = new ChromeDriver();
           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            driver.Url = ("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");


        }

        [Test]
        public void Test_customer_login()
        {
            IWebElement button = driver.FindElement(By.XPath("//button[@ng-click='customer()']"));

            button.Click();
            Thread.Sleep(2000);

            IWebElement dropdown = driver.FindElement(By.Id("userSelect")); // Replace with the actual dropdown locator.
            SelectElement select = new SelectElement(dropdown);


            Thread.Sleep(2000);

            select.SelectByText("Harry Potter");
            Thread.Sleep(4000); 

            IWebElement login = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            login.Click();
            Thread.Sleep(4000);

            string user = driver.FindElement(By.XPath("//div[@class='ng-scope']/div/div/strong/span")).Text;



            Assert.That(user, Is.EqualTo("Harry Potter"));

        }
        [Test]

        public void Test_deposit()
        {

            //customer login

            IWebElement button = driver.FindElement(By.XPath("//button[@ng-click='customer()']"));
            button.Click();

            //select Happy Potter as customer from dropdown

            IWebElement dropdown = driver.FindElement(By.Id("userSelect")); // Replace with the actual dropdown locator.
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText("Harry Potter");

            //login as Hayyr potter

            IWebElement login = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            login.Click();

            //find accout balance and store it as int value as starting balance
            string balance = driver.FindElement(By.XPath("//div[@class='center']/strong[2]")).Text;
            int starting_balance = Int32.Parse(balance);


            //click deposit tab and sen $10 as deposit
            IWebElement depositBtn = driver.FindElement(By.XPath("//div[@class='center']/button[2]"));
            depositBtn.Click();
            driver.FindElement(By.XPath("//input[@type='number']")).SendKeys("10");
            
            Thread.Sleep(3000);

            //click deposit button
            IWebElement depositSubmit = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            depositSubmit.Click();


            Thread.Sleep(4000);

            //update acocount balance after deposit and test update the balance
            string new_balance1 = driver.FindElement(By.XPath("//div[@class='center']/strong[2]")).Text;
            int new_balance = Int32.Parse(new_balance1);
            Assert.That(new_balance, Is.EqualTo(starting_balance + 10));

            //check if the deposit suucessful message is displayed
            string actualMsg = driver.FindElement(By.XPath("//div[@class='ng-scope']/span")).Text;
            Assert.That(actualMsg, Is.EqualTo("Deposit Successful"));    

        }

        [Test]
                public void test_withdrawal()
        {
            //customer login

            IWebElement button = driver.FindElement(By.XPath("//button[@ng-click='customer()']"));
            button.Click();

            //select Happy Potter as customer from dropdown

            IWebElement dropdown = driver.FindElement(By.Id("userSelect")); // Replace with the actual dropdown locator.
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText("Harry Potter");

            //login as Hayyr potter

            IWebElement login = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            login.Click();

           


            //click deposit tab and send $10 as deposit
            IWebElement depositTab = driver.FindElement(By.XPath("//div[@class='center']/button[2]"));
            depositTab.Click();

            driver.FindElement(By.XPath("//input[@type='number']")).SendKeys("10");
            Thread.Sleep(3000);

            //click deposit buttont to deposit $10
            IWebElement depositButton = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            depositButton.Click();
            Thread.Sleep(4000);

            //find accout balance and store it as int value as starting balance
            string balance = driver.FindElement(By.XPath("//div[@class='center']/strong[2]")).Text;
            int deposit_balance= Int32.Parse(balance);

            //Press withdrawal tab
            IWebElement withdrawalTab = driver.FindElement(By.XPath("//div[@class='center']/button[3]"));
            withdrawalTab.Click();
            Thread.Sleep(3000);

            //enter $4 in the text box as withdrawal amount
            driver.FindElement(By.XPath("//input[@type='number']")).SendKeys("4");
            Thread.Sleep(3000);

            //click the withdrawal button
            IWebElement withdrawButton = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            withdrawButton.Click();
            Thread.Sleep(3000);

            //check of the new balance is $6
            string balance2 = driver.FindElement(By.XPath("//div[@class='center']/strong[2]")).Text;
            int withdrawal_balance = Int32.Parse(balance2);
            Assert.That(withdrawal_balance, Is.EqualTo(deposit_balance-4));

            //check for the Transaction successfull messsage
           
            string actualMsg = driver.FindElement(By.XPath("//div[@class='ng-scope']/span")).Text;
            Assert.That(actualMsg, Is.EqualTo("Transaction successful"));


        }

        [Test]

        public void test_transaction()
        {

            //customer login

            IWebElement button = driver.FindElement(By.XPath("//button[@ng-click='customer()']"));
            button.Click();

            //select Happy Potter as customer from dropdown

            IWebElement dropdown = driver.FindElement(By.Id("userSelect")); // Replace with the actual dropdown locator.
            SelectElement select = new SelectElement(dropdown);
            select.SelectByText("Harry Potter");

            //login as Hayyr potter

            IWebElement login = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            login.Click();




            //click deposit tab and send $10 as deposit
            IWebElement depositTab = driver.FindElement(By.XPath("//div[@class='center']/button[2]"));
            depositTab.Click();

            driver.FindElement(By.XPath("//input[@type='number']")).SendKeys("10");
            Thread.Sleep(3000);

            //click deposit buttont to deposit $10
            IWebElement depositButton = driver.FindElement(By.XPath("//form[@name='myForm']/button"));
            depositButton.Click();
            Thread.Sleep(4000);

            //click transaction tab
            IWebElement transactionTab = driver.FindElement(By.XPath("//div[@class='center']/button[1]"));
            transactionTab.Click(); 
            Thread.Sleep(3000);

            //assert that transaction Amount displatyed is $10
            string amount1 = driver.FindElement(By.XPath("//tr[@id='anchor0']/td[2]")).Text;
            int amount =  Int32.Parse(amount1);
            Assert.That(amount, Is.EqualTo(10));

        }

        [TearDown]
        public void tearDownBrowser()
        {
            
            driver.Close();
        }
    }
}