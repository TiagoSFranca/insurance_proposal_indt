namespace ProposalService.UnitTest._Helpers;

public static class BuilderHelpers
{
    public static void SetProperty(object instance, string propertyName, object newValue)
    {
        ArgumentNullException.ThrowIfNull(instance);

        var type = instance.GetType();

        var prop = type.GetProperty(propertyName);

        prop.SetValue(instance, newValue, null);
    }
}
