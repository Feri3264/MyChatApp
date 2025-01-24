using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class MessageModal
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public int MessageFrom { get; set; }
        public int MessageTo { get; set; }


        //Navigation properties
        public List<UserModal> User { get; set; }
        public List<FriendModal> Freinds { get; set; }
    }
}
