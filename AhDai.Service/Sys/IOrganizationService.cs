using AhDai.Service.Models;
using AhDai.Service.Sys.Models;

namespace AhDai.Service.Sys;

/// <summary>
/// IOrganizationService
/// </summary>
public interface IOrganizationService
	: IBaseService<OrganizationInput, OrganizationOutput, OrganizationQueryInput>
	, IEnableDisableService
	, IGetByCodeService<OrganizationOutput>
	, ICodeExistService<CodeExistInput>
{

}
