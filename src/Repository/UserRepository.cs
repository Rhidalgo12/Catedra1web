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

        public async Task<bool> ExistsByRut(string rut)
        {
            return await _dataContext.Users.AnyAsync(x => x.Rut == rut);
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

        public async Task<List<User>> GetUser(string genero)
        {
            if (string.IsNullOrEmpty(genero))
            {
                return await _dataContext.Users.ToListAsync();
            }

            return await _dataContext.Users.Where(p => p.Genero == genero).ToListAsync();
        }

        public async Task<User?> Put(int id, UpdateUserDto userDto)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            user.Rut = userDto.Rut;
            user.Nombre = userDto.Nombre;
            user.Email = userDto.Email;
            user.Genero = userDto.Genero;
            user.Fecha_nacimiento = userDto.Fecha_nacimiento;

            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Delete(int id)
        {
            var userModel = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(userModel == null)
            {
                return null;
            }

            _dataContext.Users.Remove(userModel);
            await _dataContext.SaveChangesAsync();
            return userModel;
        }



    }
}