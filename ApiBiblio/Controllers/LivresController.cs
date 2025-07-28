using ApiBiblio.Database;
using ApiBiblio.Models;
using ApiBiblio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiBiblio.DTOs.LivreDTO;

namespace ApiBiblio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LivresController : ControllerBase
    {
        private readonly ILivreService _livreService;

        public LivresController(ILivreService livreService)
        {
            _livreService = livreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivreDto>>> GetAll()
        {
            try
            {
                var livres = await _livreService.GetAllAsync();
                return Ok(livres);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivreDto>> GetById(int id)
        {
            try
            {
                var livre = await _livreService.GetByIdAsync(id);
                return Ok(livre);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<LivreDto>>> GetAvailable()
        {
            try
            {
                var livres = await _livreService.GetAvailableAsync();
                return Ok(livres);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<LivreDto>> Create([FromBody] CreateLivreDto createLivreDto)
        {
            try
            {
                var livre = await _livreService.CreateAsync(createLivreDto);
                return CreatedAtAction(nameof(GetById), new { id = livre.Id }, livre);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<LivreDto>> Update(int id, [FromBody] UpdateLivreDto updateLivreDto)
        {
            try
            {
                var livre = await _livreService.UpdateAsync(id, updateLivreDto);
                return Ok(livre);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _livreService.DeleteAsync(id);
                if (result)
                    return NoContent();

                return NotFound(new { message = "Livre non trouvé" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/disponibilite")]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult> UpdateAvailability(int id, [FromBody] bool disponible)
        {
            try
            {
                var result = await _livreService.UpdateAvailabilityAsync(id, disponible);
                if (result)
                    return Ok(new { message = "Disponibilité mise à jour" });

                return NotFound(new { message = "Livre non trouvé" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}