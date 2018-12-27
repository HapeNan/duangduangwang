using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace duangduangwang.Models.Mapper
{
    public class UserMapper
    {
        //------------管理员--------------
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

        public IList<Customer> SearchUsers(string[] query)
        {
            string userId = query[0];  //如果为空则id为""
            string userName = query[1];
            string member = query[2];
            string phoneNumber = query[3];

            var users = from user in db.Customer
                         select user;

            if (userId != "" && IsNumeric(userId))
            {
                users = users.Where(s => s.UserId.ToString().Contains(userId));
            }
            if (userName != "")
            {
                users = users.Where(s => s.UserName.ToString().Contains(userName));
            }
            if (member != "全部")
            {
                users = users.Where(s => s.Member.ToString().Contains(member));
            }
            if (phoneNumber != "" && IsNumeric(phoneNumber))
            {
                users = users.Where(s => s.PhoneNumber.ToString().Contains(phoneNumber));
            }
            return users.ToList<Customer>();
        }
        //返回所有订单
        public IList<Customer> ListAllUsers()
        {
            var results = from r in db.Customer
                          select r;
            return results.ToList<Customer>();
        }

        //------------用  户---------------
        private DataClasses1DataContext db = new DataClasses1DataContext();
        public List<Customer> Alllist()
        {
            var results = from r in db.Customer
                          select r;
            List<Customer> templist = results.ToList();
            return templist;
        }
        public void Insert(Customer customer)
        {
            db.Customer.InsertOnSubmit(customer);
            db.SubmitChanges();
        }
    }
}