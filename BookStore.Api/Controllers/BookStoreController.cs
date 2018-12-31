using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookDto = BookStore.Api.Dto.Book;
using BookStore.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class BookStoreController : ControllerBase
    {
        private IBookStoreService _service;
        private ILogger _logger;        

        public BookStoreController(IBookStoreService service, ILogger<BookStoreController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("authors")]
        public IActionResult GetAllAuthors()
        {
            return new JsonResult(_service.GetAuthors());
        }

        [HttpGet("books")]
        public IActionResult GetAll()
        {
            return new JsonResult(_service.GetBooks());
        }

        [HttpGet("books/{id}")]
        public IActionResult GetById(int id)
        {            
            return new JsonResult(_service.GetBookById(id));
        }

        [HttpPost("books")]
        public IActionResult Create([FromBody] BookDto bookDto)
        {
            return new JsonResult(_service.InsertBook(bookDto));
        }

        [HttpPut("books")]
        public IActionResult Update([FromBody] BookDto bookDto)
        {
            return new JsonResult(_service.UpdateBook(bookDto));
        }

        [HttpDelete("books/{id}")]
        public IActionResult Delete(int id)
        {
            return new JsonResult(_service.DeleteBook(id));
        }


    }
}