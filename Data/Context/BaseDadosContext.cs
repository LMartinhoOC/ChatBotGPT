using System.Data.Entity;

namespace ChatGPTChatBot.Data.Context
{
    public class BaseDadosContext : DbContext
    {
       public BaseDadosContext(DbContextOptions<BaseDadosContext> options) : base(options) 
       { 
       }

       public DbSet<Message> mensagens {  get; set; }
            
       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {

       }
    }
}
