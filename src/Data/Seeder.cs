using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Models;

namespace api.src.Data
{
    public class Seeder
    {
        public static async Task Seed(DataContext context)
        {
            if (context.Users.Any())
                return;

            var generos = new string[] { "Masculino", "Femenino", "Otro", "Prefiero no decirlo" };
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Rut = $"{random.Next(10000000, 99999999)}-{random.Next(0, 9)}", 
                    Nombre = $"User {i}",
                    Email = $"user{i}@example.com", 
                    Genero = generos[random.Next(generos.Length)], 
                    Fecha_nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-random.Next(18, 60)))
                };

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
