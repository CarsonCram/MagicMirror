using System.ComponentModel.DataAnnotations;

namespace MagicMirror.Models
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
