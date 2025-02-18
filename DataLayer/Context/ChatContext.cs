using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class ChatContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<FriendModel> Friends { get; set; }
        public DbSet<MessageModel> Messages { get; set; }


        #region OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ChatApp;Integrated Security=true;TrustServerCertificate=True;");

            base.OnConfiguring(optionsBuilder);
        }
        #endregion


    }
}
