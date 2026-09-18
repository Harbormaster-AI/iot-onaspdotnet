using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ActuatorInstanceConfiguration : IEntityTypeConfiguration<ActuatorInstance>
{
    public void Configure(EntityTypeBuilder<ActuatorInstance> builder)
    {
        builder.ToTable("ActuatorInstances");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.TopicName, CommandTopic =>
{
    CommandTopic.Property(x => x.Value).HasColumnName("CommandTopic_value");
});
        builder.Property(x => x.ActuatorType).HasConversion<string>();

// Exactly one ActuatorType per ActuatorInstance (1:1)
        builder.Property(x => x.Device).IsRequired();
//        builder.HasIndex(x => x.Device.Id).IsUnique();
    }
}
