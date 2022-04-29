using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dio.Web.Models;
using Dio.Web.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController: ControllerBase
    {

        private readonly DIOContext _context;

        public CategoriaController(DIOContext context)
        {
            this._context = context;
        }
        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            var categoria = _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return await categoria;
        }

                // POST api/<CategoriaController>
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.Id, categoria});
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = RetornaId(id);

            if (categoria == null)
                return NotFound();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExiste(int id)
        {
            return _context.Categorias.Any(c => c.Id == id);
        }

        private Categoria RetornaId(int id)
        {
            return _context.Categorias.Find(id);
        }


    }
}
