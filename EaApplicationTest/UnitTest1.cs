using AutoFixture.Xunit2;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using FluentAssertions;

namespace EaApplicationTest
{
    public class UnitTest1
    {
        private readonly IHomePage _homePage;
        private readonly IProductPage _productPage;

        public UnitTest1(IHomePage homePage, IProductPage productPage)
        {
            _homePage = homePage;
            _productPage = productPage;
        }


        
        [Theory]
        [AutoData]

        public void CreateAndSendProduct(Product product)
        {
            // Click Create link
            _homePage.ClickProduct();

            // Create Product
            _productPage.ClickCreateButton();
            _productPage.CreateProduct(product);

            _productPage.PerformCLickOnSpecialValue(product.Name, "Details");

            _productPage.GetProductName().Should().Be(product.Name.Trim());
                       
        }

        [Theory]
        [InlineAutoData("Del-Product-001")]

        public void CreateAndDeleteProduct(string productName, Product product)
        {
            //Arrange 
            product.Name = productName;
            // Click Create link
            _homePage.ClickProduct();

            // Create Product
            _productPage.ClickCreateButton();
            _productPage.CreateProduct(product);

            _productPage.PerformCLickOnSpecialValue(product.Name, "Details");

            _productPage.GetProductName().Should().Be(product.Name.Trim());

            //Delete Product
            _productPage.ClickBackToList();
            _productPage.PerformCLickOnSpecialValue(product.Name, "Delete");
            _productPage.DeleteProduct();
            _productPage.IsProductDeleted(productName).Should().BeTrue();
                  
            
        }

        [Theory]
        [InlineAutoData("New_product")]
        public void CreateAndEditProduct(string productName, Product product)
        {
            product.Name = productName;

            // Click Create link
            _homePage.ClickProduct();
            // Create Product
            _productPage.ClickCreateButton();
            _productPage.CreateProduct(product);
            _productPage.PerformCLickOnSpecialValue(product.Name, "Details");
            //Assert
            _productPage.GetProductName().Should().Be(product.Name.Trim());

            //Edit Product
            _productPage.ClickBackToList();
            _productPage.PerformCLickOnSpecialValue(product.Name, "Edit");
            string editedName = product.Name + "_edited";
            product.Name = editedName;
            string editedDescription = product.Description + "_edited";
            product.Description = editedDescription;
            string editedPrice = (product.Price + 100).ToString();
            _productPage.EditProduct(product);
            _productPage.PerformCLickOnSpecialValue(editedName, "Details");

            //Assert
            _productPage.GetProductName().Should().Be(editedName.Trim());
            _productPage.GetProductDescription().Should().Be(editedDescription.Trim());
            _productPage.GetProductPrice().Should().Be(product.Price);

            //Delete Product
            _productPage.ClickBackToList();
            _productPage.PerformCLickOnSpecialValue(product.Name, "Delete");
            _productPage.DeleteProduct();
            _productPage.IsProductDeleted(editedName).Should().BeTrue("product should be removed from the list after deletion");

        }

        


    }
}