﻿using Heroes.Contracts.Grains.Heroes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Heroes.Api.Controllers
{
	[Route("api/[controller]")]
	public class HeroesController : Controller
	{
		private readonly IHeroClient _client;

		public HeroesController(IHeroClient client)
		{
			_client = client;
		}
		// GET api/values
		[HttpGet]
		public async Task<List<Hero>> Get()
		{
			var result = await _client.GetAll().ConfigureAwait(false);
			return result;
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public Task<Hero> Get(string id)
		{
			return _client.Get(id);
		}

	}
}