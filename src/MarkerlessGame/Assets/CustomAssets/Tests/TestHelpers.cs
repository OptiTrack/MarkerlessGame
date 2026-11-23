using System.Reflection;
using NUnit.Framework;

public static class TestHelpers
{
    public static void SetPrivateField(object target, string fieldName, object value)
    {
        FieldInfo field = target.GetType().GetField(
            fieldName,
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        Assert.IsNotNull(
            field,
            $"Could not find private field '{fieldName}' in {target.GetType().Name}."
        );

        field.SetValue(target, value);
    }
}