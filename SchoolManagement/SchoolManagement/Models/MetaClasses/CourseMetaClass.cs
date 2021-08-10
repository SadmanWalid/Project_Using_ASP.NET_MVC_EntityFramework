using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class CourseMetaClass
    {
       
        [Display(Name = "Title")]
        [StringLength(50, MinimumLength = 3)]
        [Required(AllowEmptyStrings =false)]
        public string title { get; set; }
        [Display(Name = "Credits")]
        [Range(1,8)]
        public Nullable<int> credits { get; set; }

    }

    [MetadataType(typeof(CourseMetaClass))]
    public partial class Course
    {


    }
    public class StudentMetaClass
    {

        [Display(Name = "First Name")]
        [StringLength(50,MinimumLength =3)]
        [Required(AllowEmptyStrings =false)]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3)]
        [Required(AllowEmptyStrings =false)]
        public string lastName { get; set; }
        [Display(Name = "Enrollment Date")]
        public Nullable<System.DateTime> enrollmentDate { get; set; }
        [Display(Name = "Middle Name")]
        [StringLength(50, MinimumLength = 3)]
        [Required(AllowEmptyStrings =false)]
        public string middleName { get; set; }
    }

    [MetadataType(typeof(StudentMetaClass))]
    public partial class Student
    {

    }
    public class TeacherMetaClass
    {
        [Required(AllowEmptyStrings =false)]
        [Display(Name ="First Name")]
        [StringLength(50,MinimumLength =3)]
        public string firstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3)]
        public string lastName { get; set; }

    }

    [MetadataType(typeof(TeacherMetaClass))]
    public partial class Teacher
    {

    }


}