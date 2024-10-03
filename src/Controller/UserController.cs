using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Dtos;
using api.src.Interfaces;
using api.src.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.src.Controller
{

    [Route("api/user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("")]
             public async Task<IResult> CreateUser(User createUserDto)
            {
            bool exists = await _userRepository.ExistsByRut(createUserDto.Rut);

            if (exists)
            {
                return TypedResults.Conflict("User already exists");
            }
            else
            {
                var userAdded = await _userRepository.AddUser(createUserDto);
                return TypedResults.Ok(userAdded);
            }
            }

         [HttpGet]
            public async Task<IResult> GetUser(string? genero)
            {
                var users = await _userRepository.GetUser(genero);

            if (users == null)
            {
                return TypedResults.BadRequest("No se encontraron Usuarios");
            }

            return TypedResults.Ok(users);
            }

            [HttpPut ("{id}")]
            public async Task <IActionResult> Put(int id,UpdateUserDto updateDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userModel = await _userRepository.Put(id, updateDto);
                if(userModel == null)
                {
                    return NotFound();
                }
                return Ok(userModel);
            }

             [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete( int id)
            {
                var user = await _userRepository.Delete(id);
                if(user == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
         
    }
}