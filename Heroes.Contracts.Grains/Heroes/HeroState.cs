﻿using Orleans;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Heroes.Contracts.Grains.Heroes
{
	public class HeroState
	{
		public Hero Hero { get; set; }
	}

	public interface IHeroGrain : IGrainWithStringKey
	{
		Task Set(Hero hero);
		Task<Hero> Get();
	}

	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class Hero
	{
		protected string DebuggerDisplay => $"Key: '{Key}', Name: '{Name}', Role: {Role}";
		public string Key { get; set; }
		public string Name { get; set; }
		public HeroRoleType Role { get; set; }
		public HashSet<string> Abilities { get; set; }

		public override string ToString() => DebuggerDisplay;
	}

	public enum HeroRoleType
	{
		Assassin = 1,
		Fighter = 2,
		Mage = 3,
		Support = 4,
		Tank = 5,
		Marksman = 6
	}
}