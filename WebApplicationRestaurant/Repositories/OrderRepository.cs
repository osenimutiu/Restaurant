using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationRestaurant.Models;
using WebApplicationRestaurant.ViewModel;

namespace WebApplicationRestaurant.Repositories
{
    public class OrderRepository
    {
        private RestaurantDBEntities1 objRestaurantDbEntities;

        public OrderRepository()
        {
            objRestaurantDbEntities = new RestaurantDBEntities1();
        }

        public bool AddOrder(OrderMasterViewModel objOrderViewModel)
        {
            OrderMaster objOrder = new OrderMaster();
            objOrder.CustomerId = objOrderViewModel.CustomerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = string.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objRestaurantDbEntities.OrderMasters.Add(objOrder);
            objRestaurantDbEntities.SaveChanges();
            int OrderId = objOrder.OrderId;

            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Discount = item.Discount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = (-1) * item.Quantity;
                objRestaurantDbEntities.OrderDetails.Add(objOrderDetail);
                objRestaurantDbEntities.SaveChanges();


                Transaction objTransaction = new Transaction();
                objTransaction.ItemId = item.ItemId;
                objTransaction.Quantity = item.Quantity;
                objTransaction.TransactionDate = DateTime.Now;
                objTransaction.TypeId = 2;
                objRestaurantDbEntities.Transactions.Add(objTransaction);
                objRestaurantDbEntities.SaveChanges();
            }
            return true;
        }
    }
}