namespace Interactors.Boundaries
{
	public interface IInteractorBoundary<TRequestModel, TResponseModel>
	{
		void HandleRequestAsync(TRequestModel requestModel, out TResponseModel responseModel);
	}
}
