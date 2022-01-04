using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJackController.Data;
using Entities;
using Entities.Interfaces;
using Entities.Providers;
using Interactors.Boundaries;
using Interactors.Providers;
using Interactors.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static Interactors.BeginGameInteractor;
using static Interactors.JoinGameInteractor;

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

			services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();
			services.AddScoped<IGameRepository, InMemoryGameRepository>();
			services.AddScoped<IGameIdentifierProvider, GuidBasedGameIdentifierProvider>();
			services.AddScoped<IDealerProvider, DealerProvider>();
			services.AddScoped<IRandomProvider, RandomProvider>();
			services.AddScoped<IHandIdentifierProvider, GuidBasedHandIdentifierProvider>();
			services.AddScoped<ICardProvider, CardProvider>();
			services.AddScoped<Deck, Deck>();
			services.AddScoped<IPlayerIdentifierProvider, GuidBasedPlayerIdentifierProvider>();
			services.AddScoped<IAvitarIdentifierProvider, GuidBasedAvitarIdentifierProvider>();
			services.AddSingleton<IGameRepository, InMemoryGameRepo>();

			//services.AddSingleton(typeof(IOutputBoundary<>), typeof(IOutputBoundary<>).Assembly);
			//services.AddSingleton(typeof(IInputBoundary<,>), typeof(IInputBoundary<,>).Assembly);
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
