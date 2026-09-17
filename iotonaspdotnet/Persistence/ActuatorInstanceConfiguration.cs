using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ActuatorInstanceConfiguration : IEntityTypeConfiguration<ActuatorInstance>
{
    public void Configure(EntityTypeBuilder<ActuatorInstance> builder)
    {
        builder.ToTable("actuatorInstances");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.TopicName, CommandTopic =>
{
    CommandTopic.Property(x => x.Value).HasColumnName("CommandTopic_value");
});
        builder.Property(x => x.ActuatorType).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per ActuatorInstance (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
    }
}
