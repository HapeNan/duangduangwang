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
    }
}
