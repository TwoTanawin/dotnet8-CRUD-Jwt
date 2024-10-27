using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI.Controllers
{
    public class Book{
        public int Id {get;set;}
        public string? Title {get;set;}
        public string? Author {get;set;}
    }

    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book {Id=1, Title="1", Author="1"},
            new Book {Id=2, Title="2", Author="2"},
            new Book {Id=3, Title="3", Author="3"},
        };

        [HttpGet("[action]")]
        public IActionResult List(){
            return Ok(books);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Get(int id){
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null){
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] Book newBook){
            return Ok("HttpPost Doing");
        }

        [HttpPost("[action]")]
        public IActionResult Create2([FromBody] Book newBook){
            newBook.Id = books.Max(b => b.Id) + 1;
            books.Add(newBook);

            return CreatedAtAction(nameof(Get), new{id = newBook.Id}, newBook);
        }

        [HttpPut("[action]/{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook){
            var existingBook= books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null){
                return NotFound();
            }

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;

            return Ok(existingBook);
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id){
            var book= books.FirstOrDefault(b => b.Id == id);
            if (book == null){
                return NotFound();
            }

            books.Remove(book);

            return NoContent();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return Ok(new { message = "index" });
        }

        [HttpGet]
        [Route("Info/{name}")]
        public IActionResult Info(string name)
        {
            return Ok(new { message = "info", name = name });
        }

        [HttpGet]
        [Route("MultiParams/{name}/{age}")]
        public IActionResult MultiParams(string name, int age){
            return Ok(new {message = "MultiParams", name=name, age=age});
        }

        [HttpGet("GetValue")]
        public IActionResult GetValue(){
            return Ok("Hello World");
        }

        [HttpGet("Query1")]
        public IActionResult Query1(){
            var value = HttpContext.Request.Query["value"].ToString();
            return Ok(value);
        }

        [HttpGet("Query11")]
        public IActionResult Query11(){
            var value = HttpContext.Request.Query["value"].ToString();
            var age = HttpContext.Request.Query["age"].ToString();
            return Ok(new {value=value, age=age});
        }

        [HttpGet("Query2")]
        public IActionResult Query2([FromQuery] string value){
            return Ok(value);
        }

        [HttpGet("Query22")]
        public IActionResult Query22([FromQuery] string value, [FromQuery] int age){
            return Ok(new {value = value, age = age});
        }
    }
}