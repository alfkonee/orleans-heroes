﻿using Heroes.Api.GraphQLCore;
using Heroes.Api.Infrastructure;
using Heroes.Api.Sample;
using Heroes.Clients;
using Heroes.Contracts.Grains.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Heroes.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHeroService, HeroService>();

			services.ConfigureClusterClient();
			services.AddHeroesClients();
			services.AddHeroesAppGraphQL();
			services.AddMvc();
			services.AddCors(o => o.AddPolicy("TempCorsPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials();
			}));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			IWarmUpClient warmUpClient,
			ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			warmUpClient.Initialize();

			app.UseCors("TempCorsPolicy");
			app.SetGraphQLMiddleWare();
			//app.UseWebSockets();
			//app.UseGraphQLEndPoint<HeroesAppSchema>("/graphql");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseGraphiQl();
			}

			app.UseMvc();
		}
	}
}