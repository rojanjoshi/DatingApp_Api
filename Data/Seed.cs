

using DatingApp.Models;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace DatingApp.Db
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.AppUsers.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password");

                context.AppUsers.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
