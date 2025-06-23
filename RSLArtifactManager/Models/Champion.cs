using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace RSLArtifactManager.Models
{
	public class Champion
	{
		#region Constants
		public const ushort MaxLevel = 60;
		public const ushort MaxStars = 6;
		#endregion
		#region Enums
		public enum EFaction { BannerLords, HighElves, TheSacredOrder, Barbarians, OgrynTribes, Lizardmen, Skinwalkers, Orcs, Demonspawn, UndeadHordes, DarkElves, KnightRevenant, Dwarves }
		public enum EAffinity { Magic, Spirit, Force, Void }
		public enum EType { Attack, Defense, Support, HP }
		public enum ERarity { Common, Uncommon, Rare, Epic, Legendary }
		#endregion

		#region Properties
		public long ID { get; set; }

		public string Name { get; set; }
		public EFaction Faction { get; set; }
		public EAffinity Affinity { get; set; }
		public EType Type { get; set; }
		public ERarity Rarity { get; set; }

		public ushort Level { get; set; }
		public ushort Stars { get; set; }
		public ushort AscendedStars { get; set; }

		public Global.Stats Base { get; protected set; }
		public Global.Stats Masteries { get; protected set; }
		public ArtifactCollection Artifacts { get; protected set; }
		#endregion
		#region Derived Properties
		public string Fullname
		{
			get
			{
				return string.Format("{0} | {1}", Name, Faction);
			}
		}
		public string Description
		{
			get
			{
				return string.Format("{0} {1} {2} Champion, {3}* level {4}", Rarity, Affinity, Type, Stars, Level);
			}
		}
		public Global.Stats SetBonus
		{ 
			get
			{
				int HPpSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.Life || x.Set == Artifact.ESet.Immortal || x.Set == Artifact.ESet.DivineLife).Count() / (double)2, MidpointRounding.ToZero);
				int ATKpSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.Offense || x.Set == Artifact.ESet.Cruel|| x.Set == Artifact.ESet.DivineOffense).Count() / (double)2, MidpointRounding.ToZero);
				int DEFpSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.Defense).Count() / (double)2, MidpointRounding.ToZero);
				int SPDpSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.Speed || x.Set == Artifact.ESet.DivineSpeed).Count() / (double)2, MidpointRounding.ToZero);
				int CRATESets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.CriticalRate || x.Set == Artifact.ESet.DivineCriticalRate).Count() / (double)2, MidpointRounding.ToZero);
				int CDMGSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.CriticalDamage).Count() / (double)2, MidpointRounding.ToZero);
				int RESISTSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.Resistance).Count() / (double)2, MidpointRounding.ToZero);
				int ACCSets = (int)Math.Round(Artifacts.Collection.Where(x => x.Set == Artifact.ESet.Accuracy).Count() / (double)2, MidpointRounding.ToZero);

				return new Global.Stats(-1)
				{
					HPp = 15 * HPpSets,
					ATKp = 15 * ATKpSets,
					DEFp = 15 * DEFpSets,
					SPDp = 12 * SPDpSets,
					CRATE = 12 * CRATESets,
					CDMG = 20 * CDMGSets,
					RESIST = 40 * RESISTSets,
					ACC = 40 * ACCSets
				};
			}
		}
		public Global.Stats TotalStats
		{
			get
			{
				Global.Stats ExternalStats = Artifacts.Stats + SetBonus + Global.ArenaTier + Global.Clan;
				switch (Affinity)
				{
					case EAffinity.Magic: ExternalStats += Global.GreatHall.Magic; break;
					case EAffinity.Spirit: ExternalStats += Global.GreatHall.Spirit; break;
					case EAffinity.Force: ExternalStats += Global.GreatHall.Force; break;
					case EAffinity.Void: ExternalStats += Global.GreatHall.Void; break;
					default: throw new Exception("Champion has no affinity");
				}

				Global.Stats Consolidated = new Global.Stats(-1);
				Consolidated.HP = Base.HP + Masteries.HP + ExternalStats.HP;
				Consolidated.HP += Base.HP * ExternalStats.HPp / 100;

				Consolidated.ATK = Base.ATK + Masteries.ATK + ExternalStats.ATK;
				Consolidated.ATK += Base.ATK * ExternalStats.ATKp / 100;

				Consolidated.DEF = Base.DEF + Masteries.DEF + ExternalStats.DEF;
				Consolidated.DEF += Base.DEF * ExternalStats.DEFp / 100;

				Consolidated.SPD = Base.SPD + Masteries.SPD + ExternalStats.SPD;
				Consolidated.SPD += Base.SPD * ExternalStats.SPDp / 100;

				Consolidated.CRATE = Base.CRATE + Masteries.CRATE + ExternalStats.CRATE;
				Consolidated.CDMG = Base.CDMG + Masteries.CDMG + ExternalStats.CDMG;
				Consolidated.RESIST = Base.RESIST + Masteries.RESIST + ExternalStats.RESIST;
				Consolidated.ACC = Base.ACC + Masteries.ACC + ExternalStats.ACC;

				return Consolidated;
			}
		}
		#endregion

		#region Constructors
		public Champion(long _id)
		{
			ID = _id;
			Base = new Global.Stats(-1);
			Masteries = new Global.Stats(-1);
			Artifacts = new ArtifactCollection();
		}
		#endregion

		#region Public Methods
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
		public bool SetAscendedStars(ushort ascendedStars)
		{
			if (0 < ascendedStars && ascendedStars <= Stars)
			{
				AscendedStars = ascendedStars;
				return true;
			}
			else return false;
		}
		#endregion
	}
}