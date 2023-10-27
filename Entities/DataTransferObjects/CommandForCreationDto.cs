using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CommandForCreationDto
    {
        [Required(ErrorMessage = "Command name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Company country is a required field.")]
        [MaxLength(15, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Company city is a required field.")]
        [MaxLength(15, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string City { get; set; }
        public IEnumerable<PlayerForCreationDto> Players { get; set; }

    }
}
