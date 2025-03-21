﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Domain.Interfaces.Book;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.WebApi.Controllers
{
    [ApiVersion("2")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        /// <summary>
        /// Carrega todos os livros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bookService.FindAll());
        }

        /// <summary>
        /// Carrega um livro por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var book = await _bookService.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        /// <summary>
        /// Registra/cadastra um novo livro
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(await _bookService.Create(book));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(await _bookService.Update(book));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _bookService.Delete(id);
            return NoContent();
        }
    }
}
