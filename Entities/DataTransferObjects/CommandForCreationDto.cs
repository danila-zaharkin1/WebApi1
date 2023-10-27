namespace Entities.DataTransferObjects
{
    public class CommandForCreationDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public IEnumerable<PlayerForCreationDto> Players { get; set; }

    }
}
