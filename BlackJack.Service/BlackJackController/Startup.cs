using BlackJackController.Data;
using Entities;
using Entities.Interfaces;
using Entities.Providers;
using Interactors.Boundaries;
using Interactors.Providers;
using Interactors.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Interactors;

namespace BlackJackController
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>()
				.AddSingleton<IGameRepository, InMemoryGameRepo>()
				.AddSingleton<IGameIdentifierProvider, GuidBasedGameIdentifierProvider>()
				.AddSingleton<IDealerProvider, DealerProvider>()
				.AddSingleton<IRandomProvider, RandomProvider>()
				.AddSingleton<IHandIdentifierProvider, GuidBasedHandIdentifierProvider>()
				.AddSingleton<ICardProvider, CardProvider>()
				.AddSingleton<Deck, Deck>()
				.AddScoped<IPlayerIdentifierProvider, GuidBasedPlayerIdentifierProvider>()
				.AddSingleton<IAvitarIdentifierProvider, GuidBasedAvitarIdentifierProvider>()

				.AddScoped<IInteractorBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>, JoinGameInteractor>()
				.AddScoped<IInteractorBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>, BeginGameInteractor>()
				.AddScoped<IInteractorBoundary<HitInteractor.RequestModel, HitInteractor.ResponseModel>, HitInteractor>()
				.AddScoped<IInteractorBoundary<HoldInteractor.RequestModel, HoldInteractor.ResponseModel>, HoldInteractor>()
				.AddScoped<IInteractorBoundary<SplitInteractor.RequestModel, SplitInteractor.ResponseModel>, SplitInteractor>()

				//services.AddSingleton<IGameRepository, InMemoryGameRepo>();

				//services.AddSingleton(typeof(IOutputBoundary<>), typeof(IOutputBoundary<>).Assembly);
				//services.AddSingleton(typeof(IInputBoundary<,>), typeof(IInputBoundary<,>).Assembly);
				//.AddSingleton(typeof(IInteractorBoundary<,>), typeof(IInteractorBoundary<,>).Assembly);
				;
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors(x => x
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(origin => true) // allow any origin
				.AllowCredentials());
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
