using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationRestaurant.Models;

namespace WebApplicationRestaurant.Repositories
{
    public class CustomerRepository
    {
        private RestaurantDBEntities1 objRestaurantDbEntities;

        public CustomerRepository()
        {
            objRestaurantDbEntities = new RestaurantDBEntities1();
        }

        public IEnumerable<SelectListItem> GetAllCustomers()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDbEntities.Customers
                                  select new SelectListItem()
                                  {
                                      Text = obj.CustomerName,
                                      Value = obj.CustomerId.ToString(),
                                      Selected = true
                                  }).ToList();
            return objSelectListItems;
        }
    }
}