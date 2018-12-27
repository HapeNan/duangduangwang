using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duangduangwang.Models.IMapper
{
    interface IBookMapper
    {
        IList<Book> NewBookList();
        IList<Book> OfferBookList();
        IList<Book> Search(string book_name);
        IList<Book> Details(int id);
        IList<Book> GetBooksOfTag(string tag);
        List<Book> GetBookById(int Id);
        List<Book> ListAllBooks();
        List<Book> SearchBooks(string []query);
        int AddBook(Book book);
    }
}
