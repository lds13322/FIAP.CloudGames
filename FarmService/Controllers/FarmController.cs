using FarmService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase
    {
        // "Banco de dados" em memória para o MVP
        private static readonly List<Farm> _farms = new List<Farm>();

        [HttpPost]
        public IActionResult CreateFarm([FromBody] Farm farm)
        {
            _farms.Add(farm);
            return CreatedAtAction(nameof(GetFarms), new { id = farm.Id }, new { Message = "Propriedade e Talhões cadastrados com sucesso!", Data = farm });
        }

        [HttpGet]
        public IActionResult GetFarms()
        {
            return Ok(_farms);
        }
    }
}