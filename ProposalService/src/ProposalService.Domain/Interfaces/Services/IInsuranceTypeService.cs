namespace ProposalService.Domain.Interfaces.Services;

public interface IInsuranceTypeService
{
    Task<List<InsuranceTypeResponse>> ListAll();
}
