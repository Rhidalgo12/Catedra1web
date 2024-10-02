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
        Task<bool> ExistsByRut(string Rut);

         Task<CreateUserDto> AddUser(User user);
    }
}