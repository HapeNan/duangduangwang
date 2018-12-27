using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using duangduangwang.Models;
using duangduangwang.Models.IMapper;
using System.Data.Linq.SqlClient;
using System.Text;

namespace duangduangwang.Models.Mapper
{
    public class OrderMapper:IOrderMapper
    {
       private DataClasses1DataContext db = new DataClasses1DataContext();

        private bool IsNumeric(string str) //接收一个string类型的参数,保存到str里
        {
            if (str == null || str.Length == 0)    //验证这个参数是否为空
                return false;                           //是，就返回False
            ASCIIEncoding ascii = new ASCIIEncoding();//new ASCIIEncoding 的实例
            byte[] bytestr = ascii.GetBytes(str);         //把string类型的参数保存到数组里

            foreach (byte c in bytestr)                   //遍历这个数组里的内容
            {
                if (c < 48 || c > 57)                          //判断是否为数字
                {
                    return false;                              //不是，就返回False
                }
            }
            return true;                                        //是，就返回True
        }

        public IList<BookOrder> SearchOrders(string []query)
        {
            string orderId = query[0];  //如果为空则id为""
            string userId = query[1];
            string status = query[2];
            string createDate = query[3];
            string orderItemBookId = query[4];
            string orderItemBookName = query[5];

            var orders = from order in db.BookOrder
                        select order;

            if (orderId!="" && IsNumeric(orderId))
            {
                orders = orders.Where(s => s.OrderId.ToString().Contains(orderId));
            }

            if (userId != "" && IsNumeric(userId))
            {
                orders = orders.Where(s => s.UserId.ToString().Contains(userId));
            }
            if (status != "全部" )
            {
                orders = orders.Where(s => s.Status.ToString().Contains(status));
            }
            if (createDate != "")
            {
                orders = orders.Where(s => s.createDate.ToString().Contains(createDate));
            }

            return orders.ToList<BookOrder>();
        }

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