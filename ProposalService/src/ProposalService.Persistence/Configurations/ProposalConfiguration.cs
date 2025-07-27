namespace ProposalService.Persistence.Configurations;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Client)
            .WithMany()
            .HasForeignKey(e => e.IdClient)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.IdStatus)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.InsuranceType)
            .WithMany()
            .HasForeignKey(e => e.IdInsuranceType)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.PaymentMethod)
            .WithMany()
            .HasForeignKey(e => e.IdPaymentMethod)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
