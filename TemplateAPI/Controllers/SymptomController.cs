using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;

namespace TemplateAPI.Controllers
{
    [Route("Symptoms")]
    public class SymptomController : Controller
    {
        private readonly ISymptomService _symptomService;

        public SymptomController(ISymptomService _symptomService)
        {
            this._symptomService = _symptomService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            List<SymptomModel> symptoms = await _symptomService.FindAll();

            if (symptoms == null)
                return BadRequest(symptoms);
            else
                return Ok(symptoms);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindSymptomById(int id)
        {
            SymptomModel symptom = await _symptomService.FindSymptomById(id);

            if (symptom == null)
                return BadRequest(symptom);
            else
                return Ok(symptom);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSymptom(int id)
        {
            SymptomModel symptom = await _symptomService.DeleteSymptom(id);

            if (symptom == null)
                return NotFound(symptom);
            else
                return Ok(symptom);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSymptom([FromBody] SymptomModel symptomM)
        {
            SymptomModel symptom = await _symptomService.CreateNewSymptom(symptomM);

            if (symptom == null)
                return BadRequest(symptom);
            else
                return Ok(symptom);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateSymptom([FromBody] SymptomModel symptomM)
        {
            SymptomModel symptom = await _symptomService.UpdateSymptom(symptomM);

            if (symptom == null)
                return BadRequest(symptom);
            else
                return Ok(symptom);
        }
    }
}
