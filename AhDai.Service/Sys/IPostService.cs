using AhDai.Service.Models;
using AhDai.Service.Sys.Models;

namespace AhDai.Service.Sys;

/// <summary>
/// IPostService
/// </summary>
public interface IPostService
	: IBaseService<PostInput, PostOutput, PostQueryInput>
	, IEnableDisableService
	, IGetByCodeService<PostOutput>
	, ICodeExistService<CodeExistInput>
{

}
