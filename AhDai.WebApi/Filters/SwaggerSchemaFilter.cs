using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel;
using System.Linq;
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
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        type = Nullable.GetUnderlyingType(type) ?? type;

        if (type != null && type.IsEnum)
        {
            // 开始构建 “值 - 说明” 的文本字符串
            var values = type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field =>
                {
                    var value = Convert.ToInt64(field.GetValue(null));
                    var text = field.GetCustomAttribute<DisplayAttribute>()?.Name
                    ?? field.GetCustomAttribute<DescriptionAttribute>()?.Description
                    ?? field.Name;
                    return $"{value} = {text}";
                });

            var enumText = "\n\n枚举值：\n" + string.Join("，", values);
            //var enumText = $"<br />配置值：<br />{string.Join("<br />", values)}";
            schema.Description = (schema.Description ?? "") + enumText;
        }
    }
}
