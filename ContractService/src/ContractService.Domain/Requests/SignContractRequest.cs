namespace ContractService.Domain.Requests;

public class SignContractRequest
{
    public Guid IdProposal { get; set; }
    public DateTime SignAt { get; set; }
}
