using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationRestaurant.Models;
using WebApplicationRestaurant.Repositories;
using WebApplicationRestaurant.ViewModel;

namespace WebApplicationRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities1 objRestaurantDbEntities;
        public HomeController()
        {
            objRestaurantDbEntities = new RestaurantDBEntities1();
        }
        // GET: Home
        public ActionResult Index()
        {
            CustomerRepository objCustomerRepository = new CustomerRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCustomerRepository.GetAllCustomers(), objItemRepository.GetAllItems(), objPaymentTypeRepository.GetAllPaymentType());
            return View(objMultipleModels);
        }

        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal? UnitPrice = objRestaurantDbEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderMasterViewModel objOrderViewModel)
        {
            OrderRepository objOrderReposotory = new OrderRepository();
            objOrderReposotory.AddOrder(objOrderViewModel);

            return Json(" Your Order has been Succeefully Placed", JsonRequestBehavior.AllowGet);
        }
    }
}