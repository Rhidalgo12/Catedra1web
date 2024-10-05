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

        public async Task<List<UserDto>> GetUser(string genero, string sort)
        {
            var query = _dataContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(genero))
            {
                query = query.Where(u => u.Genero == genero);
            }
            

            if (sort == "asc")
            {
                query = query.OrderBy(u => u.Nombre); 
            }
            else if (sort == "desc")
            {
                query = query.OrderByDescending(u => u.Nombre);
            }
            return await query.Select(u => new UserDto
            {
                Rut = u.Rut,
                Nombre = u.Nombre,
                Email = u.Email,
                Genero = u.Genero,
                Fecha_nacimiento = u.Fecha_nacimiento
            }).ToListAsync();
            

        }


        public async Task<User?> Put(int id, UpdateUserDto userDto)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
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