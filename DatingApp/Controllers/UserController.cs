using DatingApp.Db;
using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<AppUser>>> GetUsers() {
            var users = await _dataContext.AppUsers.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _dataContext.AppUsers.FindAsync(id);
            return user;
        }
    }
}
