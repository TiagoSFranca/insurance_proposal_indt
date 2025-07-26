namespace ProposalService.Persistence.Configurations;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.IdStatus)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
