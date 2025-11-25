using EaApplicationTest.Models;
using EaFramework.Extensions;
using FluentAssertions;
using OpenQA.Selenium;

namespace EaApplicationTest.Pages
{
    public interface IProductPage
    {
        void ClickCreateButton();
        void CreateProduct(Product product);
        void PerformCLickOnSpecialValue(string name, string operation);

        string GetProductName();
        string GetProductDescription();
        int GetProductPrice();

        void DeleteProduct();
        void ClickBackToList();
        void EditProduct(Product product);
        bool IsProductDeleted(string name, int timeoutSeconds = 5, int pollMs = 200);

        //void IsProductDeleted(string productName);

    }

    public class ProductPage : IProductPage
    {
        private readonly IDriverWait _driver;

        public ProductPage(IDriverWait driver)
        {
            _driver = driver;
        }

        private IWebElement lnkCreate => _driver.FindElement(By.LinkText("Create"));
        private IWebElement txtName => _driver.FindElement(By.Id("Name"));
        private IWebElement txtDesc => _driver.FindElement(By.Id("Description"));
        private IWebElement txtPrice => _driver.FindElement(By.Id("Price"));
        private IWebElement ddlProductType => _driver.FindElement(By.Id("ProductType"));
        private IWebElement btnCreate => _driver.FindElement(By.Id("Create"));
        private IWebElement tblList => _driver.FindElement(By.CssSelector(".table"));
        private IWebElement btnDelete => _driver.FindElement(By.ClassName("btn-danger"));
        private IWebElement lnkBackToList => _driver.FindElement(By.LinkText("Back to List"));

        private IWebElement btnSaveEdit => _driver.FindElement(By.ClassName("btn-primary"));

        public void ClickCreateButton()
        {
            lnkCreate.Click();
        }

        public void CreateProduct(Product product)
        {
            txtName.SendKeys(product.Name);
            txtDesc.SendKeys(product.Description);
            txtPrice.SendKeys(product.Price.ToString());
            ddlProductType.SelectDropDownByText(product.ProductType.ToString());
            btnCreate.Click();
        }

        public void PerformCLickOnSpecialValue(string name, string operation)
        {
            tblList.PerformActionOnCell("5", "Name", name, operation);
        }

        public string GetProductName() => txtName.Text;
        public string GetProductDescription() => txtDesc.Text;
        public int GetProductPrice() => int.Parse(txtPrice.Text);
        

        public void ClickBackToList()
        {
            lnkBackToList.Click();
        }

        public void DeleteProduct()
        {
            btnDelete.Click();
        }

        public void EditProduct(Product product)
        {
            txtName.Clear();
            txtName.SendKeys(product.Name);
            txtDesc.Clear();
            txtDesc.SendKeys(product.Description);
            txtPrice.Clear();
            txtPrice.SendKeys(product.Price.ToString());
            btnSaveEdit.Click();
        }

        public bool IsProductDeleted(string name, int timeoutSeconds = 5, int pollMs = 200)
        {
            var deadline = DateTime.UtcNow.AddSeconds(timeoutSeconds);
            var target = name?.Trim() ?? string.Empty;

            while (DateTime.UtcNow < deadline)
            {
                // Re-find the table each poll to avoid stale elements
                var table = _driver.FindElement(By.CssSelector(".table"));

                // Search only inside the table (relative XPath with dot)
                var matches = table.FindElements(
                    By.XPath($".//td[normalize-space()='{target}']")
                );

                if (matches.Count == 0)
                    return true;

                Thread.Sleep(pollMs);
            }

            return false; // still found after timeout
        }



    }
}
