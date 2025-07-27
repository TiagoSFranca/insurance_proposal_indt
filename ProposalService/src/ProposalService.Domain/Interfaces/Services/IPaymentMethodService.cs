namespace ProposalService.Domain.Interfaces.Services;

public interface IPaymentMethodService
{
    Task<List<PaymentMethodResponse>> ListAll();
}
