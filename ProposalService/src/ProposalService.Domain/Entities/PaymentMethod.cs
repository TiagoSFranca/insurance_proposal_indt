using System.Collections.ObjectModel;

namespace ProposalService.Domain.Entities;

public class PaymentMethod : DbEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public PaymentMethod(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static PaymentMethod Ticket = new((int)EPaymentMethod.Ticket, "Boleto");
    public static PaymentMethod Card = new((int)EPaymentMethod.Card, "Cartão");
    public static PaymentMethod DirectDebit = new((int)EPaymentMethod.DirectDebit, "Débito automático");
    public static PaymentMethod Installments = new((int)EPaymentMethod.Installments, "Parcelado");

    public static ReadOnlyCollection<PaymentMethod> Seeds => new List<PaymentMethod>()
    {
        Ticket,
        Card,
        DirectDebit,
        Installments
    }.AsReadOnly();
}
