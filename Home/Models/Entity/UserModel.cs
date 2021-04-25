using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models.Entity
{
    public class UserModel
    {
        public UserModel()
        {
            this.tasks = new HashSet<TaskModel>();
            this.locations = new HashSet<LocationModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "Username is required field!")]
        [Display(Name = "Enter username:")]
        public string username { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "First name is required! field")]
        [Display(Name = "Enter first name:")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "The first letter is required to be uppercase. White space, numbers, and special characters are not allowed.")]
        public string firstName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "Last name is required field!")]
        [Display(Name = "Enter last name:")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "The first letter is required to be uppercase. White space, numbers, and special characters are not allowed.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Password is required field!")]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Enter password")]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}", ErrorMessage = "Password format doesn't match!\nIt must contains one uppercase and one lowercase letter, one symbol,\none number and it must be at least 8 symbols long!")]
        public string password { get; set; }
        
        public UserTypeModel type { get; set; }
        public virtual ICollection<TaskModel> tasks { get; set; }
        public virtual ICollection<LocationModel> locations { get; set; }



    }
}
