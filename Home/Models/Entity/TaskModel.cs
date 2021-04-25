using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models.Entity
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [Display(Name = "Enter description:")]
        public string description { get; set; }

        [Required]
        public UserModel user { get; set; }

        [Required(ErrorMessage = "Location is required field!")]
        public LocationModel location { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime deadline { get; set; }

        [Column(TypeName = "decimal")]
        [Required(ErrorMessage = "Budget is required field!")]
        public decimal budget { get; set; }

        [Required(ErrorMessage = "Category is required field!")]
        public CategoryModel category { get; set; }

        [Required(ErrorMessage = "Status is required field!")]
        public StatusModel status { get; set; }
    }
}
