namespace ContractService.Domain.Entities;

public class Contract : DbEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

}
