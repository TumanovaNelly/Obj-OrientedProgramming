using System.ComponentModel;
using System.Reflection;

namespace Lab1.Helpers;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();

        string? name = Enum.GetName(type, value);
        if (name is null)
            return value.ToString();

        FieldInfo? field = type.GetField(name);
        if (field is null)
            return value.ToString();
        
        DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? name;
    }
}