using System.ComponentModel.DataAnnotations;


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
