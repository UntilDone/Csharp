using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "1984",
                Author = "George Orwell",
                YearPublished = 1949
            },

            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                YearPublished = 1960
            },

            new Book
            {
                Id = 3,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                YearPublished = 1925
            }
            [HttpGet]
            public ActionResult<List<Book>> GetBook()
        {

        }
        };  
    }
}
