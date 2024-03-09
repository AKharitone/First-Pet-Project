using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;

namespace TemplateAPI.Controllers
{
    [Route("healthcards")]
    public class HealthCardController : Controller
    {
        private readonly IHealthCardService _healthCardService;

        public HealthCardController(IHealthCardService _healthCardService)
        {
            this._healthCardService = _healthCardService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            List<HealthCardModel> healthCards = await _healthCardService.FindAll();

            if (healthCards == null)
                return BadRequest(healthCards);
            else
                return Ok(healthCards);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHealthCard(int id)
        {
            var healthCard = await _healthCardService.DeleteHealthCard(id);

            if (healthCard == null)
                return NotFound(healthCard);
            else
                return Ok(healthCard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewHealthCard([FromBody]HealthCardModel healthCardM)
        {
            var healthCard = await _healthCardService.CreateNewHealthCard(healthCardM);

            if (healthCard == null)
                return BadRequest(healthCard);
            else
                return Ok(healthCard);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateHealthCard([FromBody]HealthCardModel healthcardM)
        {
            var healthCard = await _healthCardService.UpdateHealthCard(healthcardM);

            if (healthCard == null)
                return BadRequest(healthCard);
            else
                return Ok(healthCard);
        }
    }
}
