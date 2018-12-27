using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace duangduangwang.Models.Mapper
{
    public class UserMapper
    {
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