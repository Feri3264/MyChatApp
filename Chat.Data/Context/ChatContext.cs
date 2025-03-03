using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Context
{
    public class ChatContext : DbContext
    {

        public ChatContext(DbContextOptions<ChatContext> options) :
        base(options) {}                

        #region Entities
        public DbSet<UserModel> Users { get; set; }
        public DbSet<FriendModel> Friends { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        #endregion

    }
}
