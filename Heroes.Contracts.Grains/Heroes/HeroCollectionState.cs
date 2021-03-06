﻿using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Contracts.Grains.Heroes
{
	public class HeroCollectionState
	{
		public Dictionary<string, HeroRoleType> HeroKeys { get; set; }
	}

	public interface IHeroCollectionGrain : IGrainWithIntegerKey
	{
		Task Set(List<Hero> heroes);
		Task<List<Hero>> GetAll(HeroRoleType? role = null);
	}
}