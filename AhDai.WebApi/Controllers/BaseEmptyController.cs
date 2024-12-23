using Microsoft.AspNetCore.Mvc;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// BaseEmptyController
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseEmptyController() : ControllerBase
{
}

/// <summary>
/// BaseEmptyController
/// </summary>
/// <typeparam name="Service"></typeparam>
/// <param name="service"></param>
[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseEmptyController<Service>(Service service) : BaseEmptyController where Service : class
{
    /// <summary>
    /// _service
    /// </summary>
    protected readonly Service _service = service;
}
