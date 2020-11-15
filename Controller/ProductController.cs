using System;
using System.Collections.Generic;
using System.Text;
using FitApka.DAL;
using FitApka.Model;
using Microsoft.Extensions.Configuration;

namespace FitApka.Controller
{
    class ProductController
    {
        ProductDAL productDAL;

        public ProductController(IConfiguration _configuration)
        {
            productDAL = new ProductDAL(_configuration);
        }

        public List<Product> GetList()
        {
            List<Product> products = productDAL.GetList();
            return products;
        }

        public List<Product> GetMealProducts(int IDMeal)
        {
            List<Product> products = productDAL.GetMealProducts(IDMeal);
            return products;
        }

        public List<Product> GetShoppingProducts(int IDShoppingList)
        {
            List<Product> products = productDAL.GetShoppingProducts(IDShoppingList);
            return products;
        }

        public void AddProduct(Product newProduct)
        {
            productDAL.AddProduct(newProduct);
        }

        public void EditProduct(int IDProduct, Product modifiedProduct)
        {
            productDAL.EditProduct(IDProduct, modifiedProduct);
        }
    }
}
