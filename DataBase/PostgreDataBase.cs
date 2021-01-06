using Microsoft.EntityFrameworkCore;

namespace Server.DataBase
{
    public class PostgreDataBase : DbContext
    {
        public PostgreDataBase (DbContextOptions<PostgreDataBase> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}