using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using duangduangwang.Models.IMapper;
using duangduangwang.Models;
namespace duangduangwang.Models.Mapper
{
    public class BookMapper : IBookMapper
    {
        private DataClasses1DataContext _db = new DataClasses1DataContext();
        public IList<Book> NewBookList()
        {

            var results = (from r in _db.Book
                           orderby r.BookId descending
                           select r).Take(6);

            return results.ToList<Book>();
        }

        public IList<Book> OfferBookList()
        {

            var results = (from r in _db.Book
                           where r.Coupon==1
                           select r).Take(6);

            return results.ToList<Book>();
           
        }
    }
}