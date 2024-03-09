using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;

namespace TemplateAPI.Controllers
{
    [Route("Illnesses")]
    public class IllnessController : Controller
    {
        private readonly IIllnessService _illnessService;

        public IllnessController(IIllnessService _illnessService)
        {
            this._illnessService = _illnessService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            List<IllnessModel> illnesses = await _illnessService.FindAll();

            if (illnesses == null)
                return BadRequest(illnesses);
            else
                return Ok(illnesses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindIllnessById(int id)
        {
            IllnessModel illness = await _illnessService.FindIllnessById(id);

            if (illness == null)
                return BadRequest(illness);
            else
                return Ok(illness);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIllness(int id)
        {
            IllnessModel illness = await _illnessService.DeleteIllness(id);

            if (illness == null)
                return NotFound(illness);
            else
                return Ok(illness);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewIllness([FromBody] IllnessModel illnessM)
        {
            IllnessModel illness = await _illnessService.CreateNewIllness(illnessM);

            if (illness == null)
                return BadRequest(illness);
            else
                return Ok(illness);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateIllness([FromBody] IllnessModel illnessM)
        {
            IllnessModel illness = await _illnessService.UpdateIllness(illnessM);

            if (illness == null)
                return BadRequest(illness);
            else
                return Ok(illness);
        }
    }
}
