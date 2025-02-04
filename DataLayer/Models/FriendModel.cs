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
    public class FriendModel
    {
        [Key]
        public int FriendsRelationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FreindId { get; set; }


        //Navigation Properties
        public virtual UserModel User { get; set; }
        public virtual List<MessageModel> Messages { get; set; }
    }
}
