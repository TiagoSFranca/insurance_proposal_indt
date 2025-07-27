namespace ProposalService.Persistence.Configurations;

public class InsuranceTypeConfiguration : IEntityTypeConfiguration<InsuranceType>
{
    public void Configure(EntityTypeBuilder<InsuranceType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);
    }
}
