﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryProject.Models;

namespace BITSRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientInventoryAdditionsController : ControllerBase
    {
        private readonly BitsContext _context;

        public IngredientInventoryAdditionsController(BitsContext context)
        {
            _context = context;
        }

        // GET: api/IngredientInventoryAdditions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientInventoryAddition>>> GetIngredientInventoryAddition()
        {
            return await _context.IngredientInventoryAddition.ToListAsync();
        }

        // GET: api/IngredientInventoryAdditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientInventoryAddition>> GetIngredientInventoryAddition(int id)
        {
            var ingredientInventoryAddition = await _context.IngredientInventoryAddition.FindAsync(id);

            if (ingredientInventoryAddition == null)
            {
                return NotFound();
            }

            return ingredientInventoryAddition;
        }

        // PUT: api/IngredientInventoryAdditions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientInventoryAddition(int id, IngredientInventoryAddition ingredientInventoryAddition)
        {
            if (id != ingredientInventoryAddition.IngredientInventoryAdditionId)
            {
                return BadRequest();
            }

            _context.Entry(ingredientInventoryAddition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientInventoryAdditionExists(id))
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

        // POST: api/IngredientInventoryAdditions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IngredientInventoryAddition>> PostIngredientInventoryAddition(IngredientInventoryAddition ingredientInventoryAddition)
        {
            _context.IngredientInventoryAddition.Add(ingredientInventoryAddition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientInventoryAddition", new { id = ingredientInventoryAddition.IngredientInventoryAdditionId }, ingredientInventoryAddition);
        }

        // DELETE: api/IngredientInventoryAdditions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IngredientInventoryAddition>> DeleteIngredientInventoryAddition(int id)
        {
            var ingredientInventoryAddition = await _context.IngredientInventoryAddition.FindAsync(id);
            if (ingredientInventoryAddition == null)
            {
                return NotFound();
            }

            _context.IngredientInventoryAddition.Remove(ingredientInventoryAddition);
            await _context.SaveChangesAsync();

            return ingredientInventoryAddition;
        }

        private bool IngredientInventoryAdditionExists(int id)
        {
            return _context.IngredientInventoryAddition.Any(e => e.IngredientInventoryAdditionId == id);
        }
    }
}
