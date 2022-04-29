using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dio.Web.Models;
using Dio.Web.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly DIOContext _context;

        public ProdutoController(DIOContext context)
        {
            this._context = context;
        }
        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.Include("Categoria").ToListAsync();
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            var produto =   _context.Produtos.Include("Categoria").FirstOrDefaultAsync(c => c.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return await produto;
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id, produto});
        }

        private bool ProdutoExiste(int id)
        {
            return _context.Produtos.Any(c => c.Id == id);
        }

        private Produto RetornaId(int id)
        {
            return _context.Produtos.Find(id);
        }
    }
}
