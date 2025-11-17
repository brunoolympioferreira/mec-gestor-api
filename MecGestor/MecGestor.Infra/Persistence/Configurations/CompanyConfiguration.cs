using MecGestor.Domain.Entities;
using MecGestor.Domain.Enums;
using MecGestor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MecGestor.Infra.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(c => c.Document, document =>
        {
            document.Property(d => d.Value)
                .HasColumnName("Document")
                .IsRequired()
                .HasMaxLength(14);

            document.Property(d => d.Type)
                .HasColumnName("DocumentType")
                .IsRequired()
                .HasMaxLength(10)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<Document.DocumentTypeEnum>(v));

            document.Ignore(d => d.FormattedValue);
        });

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(254);
        });

        builder.Property(c => c.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Active)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.Plan)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<PlanEnum>(v));

        builder.OwnsOne(c => c.Document, document =>
        {
            document.HasIndex(d => d.Value)
                .IsUnique()
                .HasDatabaseName("IX_Companies_Document");
        });
    }
}
