using Home.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models.ViewModels
{
    public class TaskCreateViewModel
    {
        public TaskCreateViewModel()
        {
            task = new TaskModel();
            locations = new HashSet<LocationModel>();
            categories = new HashSet<CategoryModel>();
        }
        public TaskModel task { get; set; }

        public IEnumerable<LocationModel> locations { get; set; }

        public IEnumerable<CategoryModel> categories { get; set; }
    }
}
