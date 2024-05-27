using AhDai.Service.Models;
using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// DicitCodeExistInput
/// </summary>
public class DicitCodeExistInput : CodeExistInput
{
    /// <summary>
    /// 父级Id
    /// </summary>
    [Required]
    public long ParentId { get; set; }
}
