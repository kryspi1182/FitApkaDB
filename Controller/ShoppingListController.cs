using FitApka.Model;
using System;
using System.Collections.Generic;
using System.Text;
using FitApka.DAL;
using Microsoft.Extensions.Configuration;

namespace FitApka.Controller
{
    class ShoppingListController
    {
        ShoppingListDAL shoppingListDAL;
        public ShoppingListController(IConfiguration _configuration)
        {
            shoppingListDAL = new ShoppingListDAL(_configuration);
        }
        public List<ShoppingList> GetList()
        {
            var shoppingLists = shoppingListDAL.GetList();
            return shoppingLists;
        }

        public void AddShoppingList(ShoppingList newShoppingList)
        {
            shoppingListDAL.AddShoppingList(newShoppingList);
        }

    }
}
