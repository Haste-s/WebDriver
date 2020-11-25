using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverLab
{
    class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void SetupTests()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.muzdv.ru/");
        }

        [Test]
        [Obsolete]
        public void CorrectDisplayingPriceSumOfTwoItems()
        {
            IWebElement SearchLink = driver.FindElement(By.Id("title-search-input_fixed"));
            SearchLink.SendKeys("C-120 NA 4/4 Классическая гитара. Flight");
            SearchLink.SendKeys(Keys.Enter);

            IWebElement GuitarLink = driver.FindElement(By.LinkText("C-120 NA 4/4 Классическая гитара. Flight"));
            GuitarLink.Click();

            IWebElement AddButtonToSoppingCart = driver.FindElement(By.Id("bx_117848907_18441_basket_actions"));
            AddButtonToSoppingCart.Click();

            SearchLink = driver.FindElement(By.Id("title-search-input_fixed"));
            SearchLink.SendKeys("62C Гитара акустическая, Ижевский завод Т.И.М");
            SearchLink.SendKeys(Keys.Enter);

            GuitarLink = driver.FindElement(By.LinkText("62C Гитара акустическая, Ижевский завод Т.И.М"));
            GuitarLink.Click();

            AddButtonToSoppingCart = driver.FindElement(By.Id("bx_117848907_12542_basket_actions"));
            AddButtonToSoppingCart.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement SoppingCatrLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("wrap_button")));
            SoppingCatrLink.Click();

            IWebElement ItemsPrise = driver.FindElement(By.Id("basket-item-table"));

            string sumString= driver.FindElement(By.ClassName("basket-coupon-block-total-price-current")).Text;
            string PriceFirstProductString = ItemsPrise.FindElement(By.XPath("tbody/tr[1]/td[4]/div/div/span")).Text;
            string PriceSecondProductString = ItemsPrise.FindElement(By.XPath("tbody/tr[2]/td[4]/div/div/span")).Text;

            Assert.AreEqual(GetPrice(sumString), GetPrice(PriceFirstProductString) + GetPrice(PriceSecondProductString));           
        }

        [TearDown]
        public void TearDownTests()
        {
        }
        int GetPrice(string number)
        {
             return int.Parse(new string(number.Where(t => char.IsDigit(t)).ToArray()));
        }

    }
}
