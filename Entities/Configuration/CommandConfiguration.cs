using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.HasData
            (
            new Command
            {
                Id = new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7"),
                Name = "Mordovia",
                Country = "Russia",
                City = "Saransk"
            },
            new Command
            {
                Id = new Guid("d960d5e1-a23e-4052-8d41-2cd31c6a9bda"),
                Name = "Amkal",
                Country = "Russia",
                City = "Moscow"
            }
            );
        }
    }
}
