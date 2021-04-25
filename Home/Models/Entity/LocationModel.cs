﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models.Entity
{
    public class LocationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "Name of the location is required field!")]
        public string name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "Address is required field!")]
        public string address { get; set; }

        public UserModel user { get; set; }
    }
}
