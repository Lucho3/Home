using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models.Entity
{
    public class StatusModel
    {
        public StatusModel()
        {
            this.tasks = new HashSet<TaskModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string status { get; set; }

        public virtual ICollection<TaskModel> tasks { get; set; }

    }
}
