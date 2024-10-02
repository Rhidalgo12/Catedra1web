using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Dtos;
using api.src.Models;

namespace api.src.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByRut(string rut);

         Task<CreateUserDto> AddUser(User user);

         Task<List<User>> GetUser(string genero);

         Task<User?> Put(int id, UpdateUserDto userDto);
    }
}