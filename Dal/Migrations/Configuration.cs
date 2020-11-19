using model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Dal.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<dal.DBcontent>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(dal.DBcontent context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


          /*  context.Grades.Add(new Grade()
            {
                Name = "115班",
                Period = "17届",
                Say = "这是口号",
                Students = new List<Student>() {
                 new Student(){
                     Name="张三",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345671",
                     Sex="男",
                     Phone="123456789012"
                 },
                 new Student(){
                     Name="李四",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345672",
                     Sex="男",
                     Phone="123456789012"
                 },
                 new Student(){
                     Name="王五",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345673",
                     Sex="男",
                     Phone="123456789012"
                 },
                 new Student(){
                     Name="张三",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345674",
                     Sex="男",
                     Phone="123456789012"
                 }
                }

            });
            context.Grades.Add(new Grade()
            {
                Name = "116班",
                Period = "17届",
                Say = "这是口号",
                Students = new List<Student>() {
                 new Student(){
                     Name="张三",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345675",
                     Sex="男",
                     Phone="123456789012"
                 },
                 new Student(){
                     Name="李四",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345676",
                     Sex="男",
                     Phone="123456789012"
                 },
                 new Student(){
                     Name="王五",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345677",
                     Sex="男",
                     Phone="123456789012"
                 },
                 new Student(){
                     Name="张三",
                     Address="湖南衡阳",
                     EnrollmentDate= Convert.ToDateTime("2017-9-1"),
                     CardId="123456789012345678",
                     Sex="男",
                     Phone="123456789012"
                 }
                }

            });
            context.SaveChanges();*/
        }
    }
}
