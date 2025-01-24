using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class ChatContext : DbContext
    {
        public DbSet<UserModal> Users { get; set; }
        public DbSet<FriendModal> Friends { get; set; }
        public DbSet<MessageModal> Messages { get; set; }


        #region OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ChatApp;Integrated Security=true;Trusted_Connection=yes;");

            base.OnConfiguring(optionsBuilder);
        }
        #endregion
     
    }
}
