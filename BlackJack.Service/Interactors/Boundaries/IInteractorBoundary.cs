using System.Threading.Tasks;

namespace Interactors.Boundaries;

public interface IInteractorBoundary<TRequestModel, TResponseModel>
{
	Task HandleRequestAsync(TRequestModel requestModel, TResponseModel responseModel);
}