﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Entities
{
    [Table("ClassRoom")]
    public class ClassRoom
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
