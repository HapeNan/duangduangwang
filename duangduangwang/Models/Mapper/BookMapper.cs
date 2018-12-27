using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using duangduangwang.Models.IMapper;
using duangduangwang.Models;
using System.Text;

namespace duangduangwang.Models.Mapper
{
    public class BookMapper : IBookMapper
    {
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
       public List<Book> ListAllBooks()
        {
            var results = from r in _db.Book
                          
                           select r;

            return results.ToList<Book>();
        }
       public List<Book> SearchBooks(string[] query)
        {
            string orderId = query[0];  //如果为空则id为""
            string userId = query[1];
            string status = query[2];
            string createDate = query[3];
            string orderItemBookId = query[4];
            string orderItemBookName = query[5];

            var books = from book in _db.Book
                         select book;

            if (orderId != "" && IsNumeric(orderId))
            {
                books = books.Where(s => s.BookId.ToString().Contains(orderId));
            }

            //if (userId != "" && IsNumeric(userId))
            //{
            //    books = books.Where(s => s.UserId.ToString().Contains(userId));
            //}
            //if (status != "全部")
            //{
            //   books = books.Where(s => s.Status.ToString().Contains(status));
            //}
            //if (createDate != "")
            //{
            //    books = books.Where(s => s.createDate.ToString().Contains(createDate));
            //}

            return books.ToList<Book>();
        }
        public int AddBook(Book book)
        {
            _db.Book.InsertOnSubmit(book);
            _db.SubmitChanges();
            var results = from r in _db.Book
                          where r == book
                          select r;
            if (results != null)
            {
                foreach (Book r in results)
                {
                    return r.BookId;
                }
            }
            return 0;
        }
        public int DeleteBook(int Id)
        {
            var result = from r in _db.Book
                         where r.BookId == Id
                         select r;
            _db.Book.DeleteAllOnSubmit(result);
            _db.SubmitChanges();
            var results = from r in _db.Book
                          where r.BookId ==Id
                          select r;
            if (results == null)
            {
                return 1;
            }
            return 0;
        }

    }
}