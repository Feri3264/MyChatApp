using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DTOs
{
    public class LoginDTO
    {

        [Display(Name ="Email Or Username")]
        public string EmailOrUsername { get; set; }

        public string Password { get; set; }

    }
}
