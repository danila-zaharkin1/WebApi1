using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Player
    {
        [Column("PlayerId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Player name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Position is 30 characters.")]
        public string Position { get; set; }
        [ForeignKey(nameof(Command))]
        public Guid CommandId { get; set; }
        public Command Command { get; set; }
    }
}
