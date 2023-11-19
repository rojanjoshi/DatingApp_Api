using DatingApp.Db;
using DatingApp.Db.Interface;
using DatingApp.Models;
using DatingApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext dataContext,ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto user)
        {
           /* if (user.UserName == null || user.PasswordHash == null)
            {
                return BadRequest("Fill the fields");
            }*/
            if (await IsUserUnique(user.UserName)) return BadRequest("Username already taken.");
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            AppUser createUser = new AppUser()
            {
                UserName = user.UserName,
                PasswordHash = passwordHash,
            };

            _dataContext.Add(createUser);
            await _dataContext.SaveChangesAsync();
            var token = _tokenService.GenerateToken(createUser);
            UserDto userDto = new UserDto() { 
                userName = user.UserName,  
                token = token
            };
            return Ok(userDto);
        
        }

        private async Task<bool> IsUserUnique(string userName) {
            return await _dataContext.AppUsers.AnyAsync(x=>x.UserName.ToLower()==userName.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto user) {
          /*  if (user.userName == null || user.password == null)
            {
                return BadRequest("Fill the fields.");
            }*/
            var userfromdb =  await _dataContext.AppUsers
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == user.userName);
            if (userfromdb == null)
            { 
                return Unauthorized("Invalid Username");
            }
            bool isPassword = BCrypt.Net.BCrypt.Verify(user.password, userfromdb.PasswordHash);
            if (!isPassword)
            {
                return Unauthorized("Invalid Password");
            }
            var token = _tokenService.GenerateToken(userfromdb);
            UserDto userDto = new UserDto() { 
                userName = userfromdb.UserName,
                token = token,
                PhotoUrl = userfromdb.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
            return Ok(userDto);
        }
        
        
    }
}
