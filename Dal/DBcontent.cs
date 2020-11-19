using model;
using System.Data.Entity;

namespace dal
{
    public class DBcontent : DbContext
    {
        public DBcontent() : base("name=conn")
        {


        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }


    }
}
