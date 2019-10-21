using System;
using System.ComponentModel.DataAnnotations;

namespace MagicMirror.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}