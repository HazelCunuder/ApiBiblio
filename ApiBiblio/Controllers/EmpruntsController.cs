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
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiBiblio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpruntsController : ControllerBase
    {
        private readonly IEmpruntService _empruntService;
        private readonly IEmployeService _employeService;

        public EmpruntsController(IEmpruntService empruntService, IEmployeService employeService)
        {
            _empruntService = empruntService;
            _employeService = employeService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<IEnumerable<EmpruntDTO>>> GetAll()
        {
            try
            {
                var emprunts = await _empruntService.GetAllAsync();
                return Ok(emprunts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpruntDTO>> GetById(int id)
        {
            try
            {
                var emprunt = await _empruntService.GetByIdAsync(id);
                return Ok(emprunt);
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

        [HttpGet("en-cours")]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<IEnumerable<EmpruntDTO>>> GetEmpruntsEnCours()
        {
            try
            {
                var emprunts = await _empruntService.GetEmpruntsEnCoursAsync();
                return Ok(emprunts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("membre/{membreId}")]
        public async Task<ActionResult<IEnumerable<EmpruntDTO>>> GetByMembre(int membreId)
        {
            try
            {
                var emprunts = await _empruntService.GetEmpruntsByMembreAsync(membreId);
                return Ok(emprunts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult<EmpruntDTO>> Create([FromBody] CreateEmpruntDto createEmpruntDto)
        {
            try
            {
                // Récupérer l'ID de l'employé connecté
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var employeId = await _employeService.GetEmployeIdAsync(email);

                var emprunt = await _empruntService.CreateAsync(createEmpruntDto, employeId);
                return CreatedAtAction(nameof(GetById), new { id = emprunt.Id }, emprunt);
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

        [HttpPatch("{id}/retour")]
        [Authorize(Roles = "Administrateur,Bibliothécaire")]
        public async Task<ActionResult> RetournerLivres(int id)
        {
            try
            {
                // Récupérer l'ID de l'employé connecté
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var employeId = await _employeService.GetEmployeIdAsync(email);

                var result = await _empruntService.RetournerLivresAsync(id, employeId);
                if (result)
                    return Ok(new { message = "Livres retournés avec succès" });

                return NotFound(new { message = "Emprunt non trouvé" });
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
