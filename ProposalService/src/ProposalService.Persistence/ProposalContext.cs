namespace ProposalService.Persistence;

public class ProposalContext : DbContext, IProposalContext
{
    public ProposalContext(DbContextOptions<ProposalContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<ProposalStatus> ProposalStatuses { get; set; }
    public DbSet<InsuranceType> InsuranceTypes { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : DbEntity => base.Set<TEntity>();
}
