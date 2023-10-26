using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Command
    {
        [Column("CommandId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Command name is a required field.")]
        [MaxLength(40, ErrorMessage = "Maximum length for the Name is 40 characters.")]
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
