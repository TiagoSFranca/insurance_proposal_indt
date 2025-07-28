namespace ContractService.Persistence;

public class ContractContext : DbContext, IContractContext
{
    public ContractContext(DbContextOptions<ContractContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Contract> Contracts { get; set; }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : DbEntity => base.Set<TEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContractContext).Assembly);
    }
}
