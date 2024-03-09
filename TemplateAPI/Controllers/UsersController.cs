using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;

namespace TemplateAPI.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        protected readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] UserModel data)
        {
            UserModel user = await _userService.CreateNewUser(data);

            if (user == null)
                return BadRequest(user);
            else
                return Ok();
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel data)
        {
            UserModel user = await _userService.UpdateUser(data);

            if (user == null)
                return NotFound(user);
            else
                return Ok();
        }

        //[Authorize]
        [HttpPut("{id},{pass}")]
        public async Task<IActionResult> UpdateUserPass(int id, string pass)
        {
            UserModel user = await _userService.UpdateUserPass(id, pass);

            if (user == null)
                return NotFound();
            else
                return Ok();
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var user = await _userService.DeleteUser(id);

            if (user != null)
                return Ok(user);
            else
                return NotFound(id);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UserModel> users = await _userService.FindAll();

            if (users == null)
                return BadRequest();
            return Ok(users);
        }
    }
}