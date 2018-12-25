using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using duangduangwang.Models;

namespace duangduangwang.Models.Mapper
{
    public class OrderMapper
    {
        public int addBookOrder(BookOrder bookOrder)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
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
            DataClasses1DataContext db = new DataClasses1DataContext();
            db.OrderItem.InsertOnSubmit(orderItem);
            db.SubmitChanges();
        }
        public void setOrderStatus(int orderId)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
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
    }
}