namespace ProposalService.Persistence.Configurations;

public class ProposalStatusConfiguration : IEntityTypeConfiguration<ProposalStatus>
{
    public void Configure(EntityTypeBuilder<ProposalStatus> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);
    }
}
