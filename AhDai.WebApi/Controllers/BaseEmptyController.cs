using AhDai.Service;
using Microsoft.AspNetCore.Mvc;

namespace AhDai.WebApi.Controllers;

/// <summary>
/// BaseEmptyController
/// </summary>
/// <typeparam name="Service"></typeparam>
/// <param name="service"></param>
[Route("api/v1/[controller]")]
public abstract class BaseEmptyController<Service>(Service service) : ControllerBase
	where Service : IBaseService
{
	/// <summary>
	/// _service
	/// </summary>
	protected readonly Service _service = service;

}
