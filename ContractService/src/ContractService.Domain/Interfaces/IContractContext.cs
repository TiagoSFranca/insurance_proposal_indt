using InsuranceProposal.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Domain.Interfaces;

public interface IContractContext : IDbContext
{
    DbSet<Contract> Contracts { get; set; }
}
