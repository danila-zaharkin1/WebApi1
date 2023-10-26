using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasData
            (
            new Player
            {
                Id = new Guid("a68fcbca-7f58-45e6-a1f9-aea8d263764e"),
                Name = "Nikita Shirmankin",
                Age = 20,
                Position = "Center forward",
                CommandId = new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7")
            },
            new Player
            {
                Id = new Guid("c53068cb-27ab-4f3f-832e-b93f5f04e263"),
                Name = "Denis Ivanov",
                Age = 19,
                Position = "Halfback",
                CommandId = new Guid("d960d5e1-a23e-4052-8d41-2cd31c6a9bda")
            },
            new Player
            {
                Id = new Guid("92674f7a-31f0-4a51-9331-74e82ab980c1"),
                Name = "Andrey Zubanov",
                Age = 25,
                Position = "Goalkeeper",
                CommandId = new Guid("1ac59c05-7f72-45f5-bb5a-d2006998d3e7")
            });
        }
    }
}
