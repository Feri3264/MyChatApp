using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [Keyless]
    public class FriendModal
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FreindId { get; set; }


        //Navigation Properties
        [ForeignKey("UserId")]
        public UserModal User { get; set; }
    }
}
