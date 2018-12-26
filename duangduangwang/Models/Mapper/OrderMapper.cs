using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using duangduangwang.Models;

namespace duangduangwang.Models.Mapper
{
    public class OrderMapper
    {
       private DataClasses1DataContext db = new DataClasses1DataContext();
        public int addBookOrder(BookOrder bookOrder)
        {
            
            db.BookOrder.InsertOnSubmit(bookOrder);
            db.SubmitChanges();
            var results =from r in db.BookOrder
                         where r==bookOrder
                        select r;
            if (results != null)
            {
                foreach(BookOrder r in results)
                {
                    return r.OrderId;
                }
            }        
            return 1;
        }
        public void addOrderItem(OrderItem orderItem)
        {
           
            db.OrderItem.InsertOnSubmit(orderItem);
            db.SubmitChanges();
        }
        public void setOrderStatus(int orderId)
        {
          
            var results = from r in db.BookOrder
                          where r.OrderId == orderId
                          select r;
            if(results!=null)
            foreach(BookOrder r in results)
            {
                r.Status = 1;
            }
            db.SubmitChanges();
        }
        //返回所有订单
        public IList<BookOrder> ListAllOrders()
        {
            var results = from r in db.BookOrder
                          select r;
            return results.ToList<BookOrder>();
        }
       // 返回所有订单项
        public IList<OrderItem> ListAllOrderItems(int OrderId)
        {
            var results = from r in db.OrderItem
                          where r.OrderId == OrderId
                          select r;
            return results.ToList<OrderItem>();
        }
        //返回某个订单详情
        public IList<BookOrder> getOrderDetail(int OrderId)
        {
            var results = from r in db.BookOrder
                          where r.OrderId == OrderId
                          select r;
            return results.ToList<BookOrder>();
        }
    }
}