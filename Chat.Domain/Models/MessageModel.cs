﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Domain.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string MessageText { get; set; }
        
        [Required]
        public int Sender { get; set; }

        [Required]
        public int Receiver { get; set; }

        [Required]
        public DateTime MessageDate { get; set; }

        [Required]
        public int FriendsRelationId { get; set; }


        //Navigation Properties
        [ForeignKey("FriendsRelationId")]
        public virtual FriendModel Friends { get; set; }
    }
}
