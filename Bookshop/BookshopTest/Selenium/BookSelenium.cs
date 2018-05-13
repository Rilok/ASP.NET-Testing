using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace BookshopTest.Selenium
{
    [TestClass]
    public class BookSelenium
    {
        private string _loginAdmin, _passwordAdmin;
        private string _loginUser;
        private IWebDriver _driver;
        private static string url = "https://localhost:44307";

        [TestInitialize]
        public void initialize()
        {
            _driver = new InternetExplorerDriver();
            _driver.Navigate().GoToUrl(url);
            _loginAdmin = "admin@o2.pl";
            _loginUser = "janek@o2.pl";
            _passwordAdmin = "Real123!";
        }

        [TestMethod]
        public void CheckLoginCorrectly()
        {
            AutoLogin(_loginUser, _passwordAdmin);
            var button = _driver.FindElement(By.Id("logout"));
            StringAssert.Contains("Wyloguj", button.Text);
        }

        [TestMethod]
        public void GoToBookTest()
        {
            AutoLogin(_loginUser, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Books']")).Click();
            string title = _driver.Title;

            Assert.AreEqual("Książki - Bookshop", title);
        }

        [TestMethod]
        public void GoToPublisherTest()
        {
            AutoLogin(_loginUser, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Publishers']")).Click();
            string title = _driver.Title;
            
            Assert.AreEqual("Wydawcy - Bookshop", title);
        }

        [TestMethod]
        public void GoToStoreTest()
        {
            AutoLogin(_loginUser, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Stores']")).Click();
            string title = _driver.Title;

            Assert.AreEqual("Magazyn - Bookshop", title);
        }

        [TestMethod]
        public void AddBookTest()
        {
            AutoLogin(_loginAdmin, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Books']")).Click();
            try
            {
                var elements = _driver.FindElements(By.TagName("tr")).Count;
                var expected = elements + 1;

                _driver.FindElement(By.CssSelector("[href*='Create']")).Click();
                _driver.FindElement(By.Id("name")).SendKeys("Higurashi");
                _driver.FindElement(By.Id("genre")).SendKeys("Horror");
                _driver.FindElement(By.Id("year")).SendKeys("2015");
                _driver.FindElement(By.ClassName("btn-primary")).Click();

                elements = _driver.FindElements(By.TagName("tr")).Count;
                Assert.AreEqual(expected, elements);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                AutoLogout();
            }
        }

        [TestMethod]
        public void DetailsOfExistingBookTest()
        {
            var name = "Higurashi";
            AutoLogin(_loginAdmin, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Books']")).Click();
            try
            {
                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                             "[normalize-space(text())='" + name + "']]//" +
                                             "a[@id='details_book']")).Click();


                StringAssert.Contains(_driver.Url, "/Books/Details/");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                AutoLogout();
            }
        }

        [TestMethod]
        public void DeleteExistingBookTest()
        {
            var name = "Higurashi";
            AutoLogin(_loginAdmin, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Books']")).Click();
            try
            {
                var elements = _driver.FindElements(By.TagName("tr")).Count;
                var expected = elements - 1;

                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                             "[normalize-space(text())='" + name + "']]//" +
                                             "a[@id='remove_book']"))
                    .Click();
                _driver.FindElement(By.Id("delete")).Click();

                elements = _driver.FindElements(By.TagName("tr")).Count;
                Assert.AreEqual(expected, elements);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                AutoLogout();
            }
        }

        [TestMethod]
        public void AddStoreTest()
        {
            var bookName = "Ania z Zielonego Wzgórza";
            var publisherName = "Insignis";

            AutoLogin(_loginAdmin, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Stores']")).Click();
            try
            {
                var elements = _driver.FindElements(By.TagName("tr")).Count;
                var expected = elements + 1;

                _driver.FindElement(By.CssSelector("[href*='Create']")).Click();
                var bookDropDownList = new SelectElement(_driver.FindElement(By.Id("BookID")));
                bookDropDownList.SelectByText(bookName);
                var publisherDropDownList = new SelectElement(_driver.FindElement(By.Id("PublisherID")));
                publisherDropDownList.SelectByText(publisherName);
                _driver.FindElement(By.Id("amount")).SendKeys("2137");
                _driver.FindElement(By.Id("price")).SendKeys("19.99");
                _driver.FindElement(By.ClassName("btn-default")).Click();

                elements = _driver.FindElements(By.TagName("tr")).Count;

                Assert.AreEqual(expected, elements);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                AutoLogout();
            }
        }
        [TestMethod]
        public void DetailsOfExistingStoreTest()
        {
            var name = "Ania z Zielonego Wzgórza";
            AutoLogin(_loginAdmin, _passwordAdmin);
            _driver.FindElement(By.CssSelector("[href*='Stores']")).Click();
            try
            {
                _driver.FindElement(By.XPath("//table/tbody/tr[td" +
                                             "[normalize-space(text())='" + name + "']]//" +
                                             "a[@id='details_store']")).Click();


                StringAssert.Contains(_driver.Url, "/Stores/Details/");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                AutoLogout();
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
            _driver = null;
        }

        private void AutoLogin(string email, string password)
        {
            _driver.FindElement(By.CssSelector("[href*='Account/Login']")).Click();
            _driver.FindElement(By.Id("Email")).SendKeys(email);
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.XPath("//button[@type='submit'][text()='Zaloguj się']")).Click();
        }

        private void AutoLogout()
        {
            _driver.FindElement(By.Id("logout")).Click();
        }
    }
}
