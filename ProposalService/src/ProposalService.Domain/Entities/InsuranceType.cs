using System.Collections.ObjectModel;

namespace ProposalService.Domain.Entities;

public class InsuranceType : DbEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public InsuranceType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static InsuranceType Life = new((int)EInsuranceType.Life, "Vida");
    public static InsuranceType Automobile = new((int)EInsuranceType.Automobile, "Automóvel");
    public static InsuranceType Residential = new((int)EInsuranceType.Residential, "Residencial");
    public static InsuranceType Enterprise = new((int)EInsuranceType.Enterprise, "Empresarial");
    public static InsuranceType Health = new((int)EInsuranceType.Health, "Saúde");

    public static ReadOnlyCollection<InsuranceType> Seeds => new List<InsuranceType>()
    {
        Life,
        Automobile,
        Residential,
        Enterprise,
        Health,
    }.AsReadOnly();
}
