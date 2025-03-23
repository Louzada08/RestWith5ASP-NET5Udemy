using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestWithASPNET.Domain.Entities;

namespace RestWithASPNET.FrameWrkDrivers.Mapping;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("person");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.HasOne(x => x.CreatedBy)
            .WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}
