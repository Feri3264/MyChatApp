using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class MessageModal
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string MessageText { get; set; }

        [Required]
        public int MessageFrom { get; set; }

        [Required]
        public int MessageTo { get; set; }


        //Navigation properties
        [ForeignKey("MessageFrom")]
        public List<UserModal> User { get; set; }
        [ForeignKey("MessageTo")]
        public List<FriendModal> Freinds { get; set; }
    }
}
