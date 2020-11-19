using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace model
{
    public class User : modelBase
    {

        [Required]
        [MaxLength(20), MinLength(2)]
        public string Account { get; set; }
        [Required]
        [MaxLength(16), MinLength(6)]
        public string Pwd { get; set; }

        [Required]
        [DefaultValue(1)]               //1：普通用户，0：管理员
        public int level { get; set; }
        public string Remark { get; set; }
    }

    public class Grade : modelBase
    {
        [Required]
        public string Name { get; set; }
        public string Period { get; set; }
        public string Say { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Student : modelBase
    {
        [Required]
        [MaxLength(20), MinLength(2)]
        public string Name { get; set; }
        public string Sex { get; set; }
        [Required]
        [Index("IX_FirstNameLastName", 1, IsUnique = true)]
        [MaxLength(18), MinLength(18)]
        public string CardId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [ForeignKey("Grade")]
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
    }

}
