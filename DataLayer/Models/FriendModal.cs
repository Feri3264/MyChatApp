using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class FriendModal
    {
        public int UserId { get; set; }
        public int FreindId { get; set; }

        //Navigation Properties
        public UserModal User { get; set; }
    }
}
