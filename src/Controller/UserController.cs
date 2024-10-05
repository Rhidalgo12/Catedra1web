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
            if (createUserDto.Fecha_nacimiento >= DateOnly.FromDateTime(DateTime.Today))
            {
                return TypedResults.BadRequest("La fecha de nacimiento debe ser menor a la fecha actual.");
            }
            bool exists = await _userRepository.ExistsByRut(createUserDto.Rut);

            if (exists)
            {
                return TypedResults.Conflict("Rut already exists");
            }
            else
            {
                var userAdded = await _userRepository.AddUser(createUserDto);
                var uri = $"/user/{userAdded.Rut}";
                return TypedResults.Created(uri, userAdded);
            }
            }

         [HttpGet]
            public async Task<IResult> GetUser(string? genero, string? sort)
            {
                if (sort != null && sort != "asc" && sort != "desc")
                {
                    return TypedResults.BadRequest("Valor de sort inválido");
                }

                if (genero != null && 
                    genero != "Masculino" && 
                    genero != "Femenino" && 
                    genero != "Otro" && 
                    genero != "Prefiero no decirlo")
                {
                    return TypedResults.BadRequest("Valor de genero inválido");
                }
                var users = await _userRepository.GetUser(genero, sort);

                if (users == null)
                {
                    return TypedResults.BadRequest("No se encontraron Usuarios");
                }

                return TypedResults.Ok(users);
            }
            
            [HttpPut ("{id}")]
            public async Task <IActionResult> Put(int id,UpdateUserDto updateDto)
            {
                 if (updateDto.Fecha_nacimiento >= DateOnly.FromDateTime(DateTime.Today))
                {
                return BadRequest("La fecha de nacimiento debe ser menor a la fecha actual.");
                }
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userModel = await _userRepository.Put(id, updateDto);
                if(userModel == null)
                {
                    return NotFound("User Not Found");
                }
                return Ok(userModel);
            }

             [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete( int id)
            {
                var user = await _userRepository.Delete(id);
                if(user == null)
                {
                    return NotFound("User Not Found");
                }
                return Ok("User Deleted");
            }
         
    }
}