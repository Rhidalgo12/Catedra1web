using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Data;
using api.src.Dtos;
using api.src.Interfaces;
using api.src.Models;
using Microsoft.EntityFrameworkCore;

namespace api.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> ExistsByRut(string Rut)
        {
            return await _dataContext.Users.AnyAsync(x => x.Rut == Rut);
        }

        public async Task<CreateUserDto> AddUser(User user)
        {
            await _dataContext.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return new CreateUserDto
            {
                Rut = user.Rut,
                Nombre = user.Nombre,
                Email = user.Email,
                Genero = user.Genero,
                Fecha_nacimiento = user.Fecha_nacimiento
            };
        }


    }
}