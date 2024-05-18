namespace AhDai.Service;

/// <summary>
/// IProcessService
/// </summary>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
public interface IProcessService<TInput, TOutput, TQueryInput>
	: IBaseService<TInput, TOutput, TQueryInput>
	where TInput : class, IProcessInput
	where TOutput : class, IProcessOutput
	where TQueryInput : class, IProcessQueryInput
{

}

