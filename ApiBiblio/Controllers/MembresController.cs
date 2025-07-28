using ApiBiblio.Database;
using ApiBiblio.DTOs;
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
using static ApiBiblio.DTOs.MembreDTO;

namespace ApiBiblio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MembresController : ControllerBase
    {
        private readonly IMembreService _membreService;

        public MembresController(IMembreService membreService)
        {
            _membreService = membreService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<IEnumerable<MembreDto>>> GetAll()
        {
            try
            {
                var membres = await _membreService.GetAllAsync();
                return Ok(membres);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MembreDto>> GetById(int id)
        {
            try
            {
                var membre = await _membreService.GetByIdAsync(id);
                return Ok(membre);
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

        [HttpGet("{id}/historique")]
        public async Task<ActionResult<IEnumerable<EmpruntDTO>>> GetHistorique(int id)
        {
            try
            {
                var historique = await _membreService.GetHistoriqueAsync(id);
                return Ok(historique);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<MembreDto>> Create([FromBody] CreateMembreDto createMembreDto)
        {
            try
            {
                var membre = await _membreService.CreateAsync(createMembreDto);
                return CreatedAtAction(nameof(GetById), new { id = membre.Id }, membre);
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
        public async Task<ActionResult<MembreDto>> Update(int id, [FromBody] UpdateMembreDto updateMembreDto)
        {
            try
            {
                var membre = await _membreService.UpdateAsync(id, updateMembreDto);
                return Ok(membre);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _membreService.DeleteAsync(id);
                if (result)
                    return NoContent();

                return NotFound(new { message = "Membre non trouvé" });
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
    }
}
