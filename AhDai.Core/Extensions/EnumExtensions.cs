using System;
using System.Linq;
using System.Reflection;

namespace AhDai.Core.Extensions;

/// <summary>
/// EnumExtensions
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets an attribute on an enum field value.
    /// </summary>
    /// <typeparam name="T">The type of the attribute to retrieve.</typeparam>
    /// <param name="enumValue">The enum value.</param>
    /// <returns>
    /// The attribute of the specified type or null.
    /// </returns>
    public static T? GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
    {
        var type = enumValue.GetType();
        var memInfo = type.GetMember(enumValue.ToString()).First();
        var attributes = memInfo.GetCustomAttributes<T>(false);
        return attributes.FirstOrDefault();
    }
}
