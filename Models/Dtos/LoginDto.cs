using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }    
    }
}
