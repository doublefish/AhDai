using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AhDai.WebApi.Filters;

/// <summary>
/// SwaggerSchemaFilter
/// </summary>
public class SwaggerSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Apply
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumType = context.Type;
            var enumValues = Enum.GetValues(enumType);

            schema.Enum.Clear();
            foreach (var enumValue in enumValues)
            {
                if (enumValue == null) continue;
                var name = enumValue.ToString();
                if (name == null) continue;
                var value = (int)enumValue;
                var field = enumType.GetField(name);
                var attribute = field?.GetCustomAttribute<DisplayAttribute>();
                var description = attribute?.Name ?? name;
                schema.Enum.Add(new OpenApiString($"{name}={value} - {description}"));
            }
        }
    }
}
