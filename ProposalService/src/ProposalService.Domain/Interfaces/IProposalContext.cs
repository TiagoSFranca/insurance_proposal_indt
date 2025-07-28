using InsuranceProposal.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace ProposalService.Domain.Interfaces;

public interface IProposalContext : IDbContext
{
    DbSet<Proposal> Proposals { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<ProposalStatus> ProposalStatuses { get; set; }
    DbSet<InsuranceType> InsuranceTypes { get; set; }
    DbSet<PaymentMethod> PaymentMethods { get; set; }
}
