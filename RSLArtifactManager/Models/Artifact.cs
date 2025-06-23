using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RSLArtifactManager.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSLArtifactManager.Models
{
	public class Artifact
	{
		#region Constants
		public const ushort MaxLevel = 16;
		public const ushort MaxStars = 6;
		public const ushort MaxUpgrades = 4;
		#endregion
		#region Enums
		public enum EType { Weapon, Helmet, Shield, Gauntlets, Chestplate, Boots, Ring, Amulet, Banner }
		public enum ERarity { Common, Uncommon, Rare, Epic, Legendary }
		public enum ESet {
			Life,
			Offense,
			Defense,
			Speed,
			CriticalRate,
			CriticalDamage,
			Accuracy,
			Resistance,
			Lifesteal,
			Fury,
			Daze,
			Cursed,
			Frost,
			Frenzy,
			Regeneration,
			Immunity,
			Shield,
			Relentless,
			Savage,
			Destroy,
			Stun,
			Toxic,
			Taunting,
			Retaliation,
			Avenging,
			Stalwart,
			Reflex,
			Curing,
			Cruel,
			Immortal,
			DivineOffense,
			DivineCriticalRate,
			DivineLife,
			DivineSpeed,
			None
		}
		#endregion

		#region Properties
		public long ID { get; set; }

		public ushort Level { get; set; }
		public ushort Stars { get; set; }

		public ESet Set { get; set; }
		public EType Type { get; set; }
		public ERarity Rarity { get; set; }

		public Global.Stats.EType Main { get; set; }
		public Global.Stats.EType Sub1 { get; set; }
		public Global.Stats.EType Sub2 { get; set; }
		public Global.Stats.EType Sub3 { get; set; }
		public Global.Stats.EType Sub4 { get; set; }

		public ushort UpgradesSub1 { get; set; }
		public ushort UpgradesSub2 { get; set; }
		public ushort UpgradesSub3 { get; set; }
		public ushort UpgradesSub4 { get; set; }

		public Global.Stats Stats { get; protected set; }
		#endregion
		#region Derived Properties
		public bool CanUpgrade => Level < MaxLevel;

		public string Description
		{
			get
			{
				return string.Format("{0}{1} ({2}, {3}*, level {4})",
					Set != ESet.None ? "\"" + Set + "\" " : string.Empty,
					Type,
					Rarity,
					Stars,
					Level);
			}
		}
		public string SetBonusDescription
		{
			get
			{
				switch (Set)
				{
					case ESet.Life: return "2 Artifacts per Set | HP +15%";
					case ESet.Offense: return "2 Artifacts per Set | ATK +15%";
					case ESet.Defense: return "2 Artifacts per Set | DEF +15%";
					case ESet.Speed: return "2 Artifacts per Set | SPD +12%";
					case ESet.Shield: return "4 Artifacts per Set | +30% HP Ally Shield for 3 Turns";
					case ESet.Immortal: return "2 Artifacts per Set | HP +15%. Heals by 3% every turn";
					default: return "This item provides no set bonus";
				}
			}
		}
		#endregion

		#region Constructors
		public Artifact()
		{
			ID = -1;
			Level = 1;
			Stars = 1;

			Set = ESet.None;
			Type = EType.Weapon;
			Rarity = ERarity.Common;

			Main = Global.Stats.EType.NONE;
			Sub1 = Global.Stats.EType.NONE;
			Sub2 = Global.Stats.EType.NONE;
			Sub3 = Global.Stats.EType.NONE;
			Sub4 = Global.Stats.EType.NONE;

			UpgradesSub1 = 0;
			UpgradesSub2 = 0;
			UpgradesSub3 = 0;
			UpgradesSub4 = 0;

			Stats = new Global.Stats(-1);
		}
		public Artifact(long _id, long _idStats)
		{
			ID = _id;
			Level = 1;
			Stars = 1;

			Set = ESet.None;
			Type = EType.Weapon;
			Rarity = ERarity.Common;

			Main = Global.Stats.EType.NONE;
			Sub1 = Global.Stats.EType.NONE;
			Sub2 = Global.Stats.EType.NONE;
			Sub3 = Global.Stats.EType.NONE;
			Sub4 = Global.Stats.EType.NONE;

			UpgradesSub1 = 0;
			UpgradesSub2 = 0;
			UpgradesSub3 = 0;
			UpgradesSub4 = 0;

			Stats = new Global.Stats(_idStats);
		}
		#endregion

		#region Public Methods
		public bool Upgrade(ushort Levels)
		{
			if (Level + Levels <= MaxLevel)
			{
				Level += Levels;
				return true;
			}
			else return false;
		}

		public bool SetLevel(ushort newLevel)
		{
			if (0 < newLevel && newLevel <= MaxLevel)
			{
				Level = newLevel;
				return true;
			}
			else return false;
		}
		public bool SetStars(ushort stars)
		{
			if (0 < stars && stars <= MaxStars)
			{
				Stars = stars;
				return true;
			}
			else return false;
		}
		#endregion
	}

	public class ArtifactCollection
	{
		#region Properties
		public Artifact Weapon { get; set; }
		public Artifact Helmet { get; set; }
		public Artifact Shield { get; set; }
		public Artifact Gauntlets { get; set; }
		public Artifact Chestplate { get; set; }
		public Artifact Boots { get; set; }

		public Artifact Ring { get; set; }
		public Artifact Amulet { get; set; }
		public Artifact Banner { get; set; }
		#endregion
		#region Derived Properties
		public List<Artifact> Collection { get; protected set; }
		public Global.Stats Stats
		{
			get
			{
				Global.Stats res = new Global.Stats(-1);
				foreach (Artifact equipped in Collection)
					res += equipped.Stats;

				return res;
			}
		}
		#endregion

		#region Constructors
		public ArtifactCollection()
		{ 
			Collection = new List<Artifact>() { Weapon, Helmet, Shield, Gauntlets, Chestplate, Boots, Ring, Amulet, Banner };
		} 
		#endregion
	}
}
