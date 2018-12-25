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
        public List<Book> GetBookById(int Id)
        {

            var result = from r in _db.Book
                         where r.BookId == Id
                         select r;
            return result.ToList();
        }
        //book search
        public IList<Book> Search(string book_name)
        {
            var books = from b in _db.Book
                        select b;

            if (!string.IsNullOrEmpty(book_name))
            {
                books = books.Where(s => s.BookName.Contains(book_name));
            }
            return books.ToList<Book>();
        }

        //book detail
        public IList<Book> Details(int id)
        {
            var book = from b in _db.Book
                       where b.BookId == id
                       select b;
            return book.ToList<Book>();
        }
        //books of tag
        public IList<Book> GetBooksOfTag(string tag)
        {
            var books = from b in _db.Book
                        where b.Tag.Contains(tag)
                        select b;
            return books.ToList<Book>();
        }
        
        public HashSet<String> GetTagsOfBook(IList<Book> books)
        {
            HashSet<string> tags = new HashSet<string>();
            foreach (Book bk in books)
            {
                string[] tagOfBook = bk.Tag.Split(',');
                foreach (var t in tagOfBook)
                {
                    tags.Add(t);
                }
            }
            return tags;
        }

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