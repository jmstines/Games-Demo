using System.Threading.Tasks;

namespace Interactors.Boundaries;

public interface IInputBoundary<TRequestModel, TResponseModel>
{
	Task HandleRequestAsync(TRequestModel requestModel, IOutputBoundary<TResponseModel> outputBoundary);
}