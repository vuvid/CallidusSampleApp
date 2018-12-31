using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookStore.Frontend.Dto;
using BookStore.Frontend.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BookStore.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class BookStoreController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly ILogger _logger;
        private readonly string _token;
        private readonly HttpClient _httpClient;

        public BookStoreController(IConfiguration configuration, ILogger<BookStoreController> logger)
        {
            _configuration = configuration;
            _apiUrl = $"{_configuration.GetValue<string>("Endpoints:BookStoreApiUrl")}/bookstore";
            _logger = logger;
            _token = TokenManger.GetToken().Result;

            _httpClient = new HttpClient();
            _httpClient.SetBearerToken(_token);
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/authors");

            if (!response.IsSuccessStatusCode)
                return BadRequest($"Failed to get authors");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetAll()
        {

            var response = await _httpClient.GetAsync($"{_apiUrl}/books");

            if (!response.IsSuccessStatusCode)
                return BadRequest($"Failed to get books.");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }

        [HttpGet("books/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/books/{id}");

            if (!response.IsSuccessStatusCode)
                return BadRequest($"Failed to get the book using {id} as id.");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }

        [HttpPost("books")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            var stringPayload = JsonConvert.SerializeObject(book);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/books", httpContent);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Failed to create the book.");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);

        }

        [HttpPut("books")]
        public async Task<IActionResult> Update([FromBody]Book book)
        {
            var stringPayload = JsonConvert.SerializeObject(book);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/books", httpContent);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Failed to update the book.");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }

        [HttpDelete("books/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/books/{id}");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Failed to delete the book.");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }
    }
}
