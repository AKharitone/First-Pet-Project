using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;

namespace TemplateAPI.Controllers
{
    [Route("doctors")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            List<DoctorModel> doctors = await _doctorService.FindAll();

            if (doctors == null)
                return BadRequest();
            else
                return Ok(doctors);
        }

        //[Authorize]
        [HttpGet("{surname}")]
        public async Task<IActionResult> FindDoctorBySurname(string surname)
        {
            var doctor = await _doctorService.FindDoctorBySurname(surname);

            if (doctor == null)
                return NotFound(doctor);
            else
                return Ok(doctor);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _doctorService.DeleteDoctor(id);

            if (doctor == null)
                return NotFound(doctor);
            else
                return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDoctor([FromBody]DoctorModel data)
        {
            var doctor = await _doctorService.CreateNewDoctor(data);

            if (doctor == null)
                return BadRequest(doctor);
            else
                return Ok(doctor);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorModel data)
        {
            var doctor = await _doctorService.UpdateDoctor(data);

            if (doctor == null)
                return BadRequest(doctor);
            else
                return Ok(doctor);
        }

        [HttpPut("assign_pacient")]
        public async Task<IActionResult> AssignPacient([FromBody]DoctorModel docM, UserModel userM)
        {
            var doctor = await _doctorService.AssignPacient(docM, userM);

            if (doctor == null)
                return NotFound(doctor);
            else
                return Ok(doctor);
        }

        [HttpPut("unassign_pacient")]
        public async Task<IActionResult> UnassignPacient([FromBody] DoctorModel docM, UserModel userM)
        {
            var doctor = await _doctorService.UnassignPacient(docM, userM);

            if (doctor == null)
                return NotFound(doctor);
            else
                return Ok(doctor);
        }
    }
}
