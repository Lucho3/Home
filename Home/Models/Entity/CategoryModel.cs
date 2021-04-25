using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models.Entity
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            this.tasks = new HashSet<TaskModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "Tyepe is required field!")]
        public string type { get; set; }

        public virtual ICollection<TaskModel> tasks { get; set; }
    }
}
