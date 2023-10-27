namespace Entities.DataTransferObjects
{
    public class CommandForUpdateDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public IEnumerable<PlayerForCreationDto> Players { get; set; }
    }
}
