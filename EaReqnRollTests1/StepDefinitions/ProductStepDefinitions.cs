using System;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using FluentAssertions;
using Reqnroll.Assist;

namespace EaReqnRollTests1.StepDefinitions
{
    [Binding]
    public sealed class ProductStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IHomePage _homePage;
        private readonly IProductPage _productPage;

        public ProductStepDefinitions(ScenarioContext scenarioContext, IHomePage homePage, IProductPage productPage)
        {
            _scenarioContext = scenarioContext;
            _homePage = homePage;
            _productPage = productPage;
            
        }   

        [Given("I click on the Product menu")]
        public void GivenIClickOnTheProductMenu()
        {
            _homePage.ClickProduct();
        }

        [Given("I click {string} link")]
        public void GivenIClickLink(string create)
        {
            _productPage.ClickCreateButton();
        }

        [Given("I create product with following details")]
        public void GivenICreateProductWithFollowingDetails(DataTable dataTable)
        {
            var product = dataTable.CreateInstance<Product>();
            _productPage.CreateProduct(product);
            _scenarioContext.Set<Product>(product);
        }

        [When("I click the Details link of the newly created product")]
        public void WhenIClickTheDetailsLinkOfTheNewlyCreatedProduct()
        {
            var product = _scenarioContext.Get<Product>();
            _productPage.PerformCLickOnSpecialValue(product.Name, "Details");
        }

        [Then("I see all the product details as entered")]
        public void ThenISeeAllTheProductDetailsAsEntered()
        {
            var product = _scenarioContext.Get<Product>();
            _productPage.GetProductName().Should().BeEquivalentTo(product.Name.Trim());
        }
    }
}
