using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duangduangwang.Models.IMapper
{
    interface IOrderMapper
    {
        int addBookOrder(BookOrder bookOrder);
        void addOrderItem(OrderItem orderItem);
        void setOrderStatus(int orderId);
        IList<BookOrder> ListAllOrders();
        IList<OrderItem> ListAllOrderItems(int OrderId);
        IList<BookOrder> getOrderDetail(int OrderId);
        IList<BookOrder> SearchOrders(string []query);
    }
}
