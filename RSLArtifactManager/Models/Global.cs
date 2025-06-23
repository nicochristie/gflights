using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace RSLArtifactManager.Models
{
	public static class Global
	{
		public static class GreatHall
		{
			public static Stats Magic = new Stats(-1)
			{
				HPp = 6,
				ATKp = 4,
				DEFp = 4,
				CDMG = 6,
				RESIST = 20,
				ACC = 40
			};
			public static Stats Spirit = new Stats(-1)
			{
				HPp = 4,
				ATKp = 4,
				DEFp = 4,
				CDMG = 6,
				RESIST = 20,
				ACC = 30
			};
			public static Stats Force = new Stats(-1)
			{
				HPp = 4,
				ATKp = 3,
				DEFp = 3,
				CDMG = 4,
				RESIST = 15,
				ACC = 15
			};
			public static Stats Void = new Stats(-1)
			{
				HPp = 4,
				ATKp = 3,
				DEFp = 3,
				CDMG = 4,
				RESIST = 15,
				ACC = 30
			};
		}
		public static Stats ArenaTier = new Stats(-1)
		{
			HPp = 18,
			ATKp = 18,
			DEFp = 18
		};
		public static Stats Clan = new Stats(-1)
		{
			HP = 0,
			HPp = 0,
			ATK = 0,
			ATKp = 0,
			DEF = 0,
			DEFp = 0
		};

		public class Stats
		{
			#region Enums
			public enum EType 
			{ 
				HP, HPe, HPp, HPpe, 
				ATK, ATKe, ATKp, ATKpe, 
				DEF, DEFe, DEFp, DEFpe, 
				SPD, SPDe, SPDp, 
				CRATE, CRATEe,
				CDMG, CDMGe, 
				RESIST, RESISTe,
				ACC, ACCe,
				NONE 
			}
			#endregion

			#region Properties
			public long ID { get; set; }

			public int HP { get; set; }
			public int HPe { get; set; }
			public int HPp { get; set; }
			public int HPpe { get; set; }

			public int ATK { get; set; }
			public int ATKe { get; set; }
			public int ATKp { get; set; }
			public int ATKpe { get; set; }

			public int DEF { get; set; }
			public int DEFe { get; set; }
			public int DEFp { get; set; }
			public int DEFpe { get; set; }

			public int SPD { get; set; }
			public int SPDe { get; set; }
			public int SPDp { get; set; }

			public int CRATE { get; set; }
			public int CRATEe { get; set; }

			public int CDMG { get; set; }
			public int CDMGe { get; set; }

			public int RESIST { get; set; }
			public int RESISTe { get; set; }

			public int ACC { get; set; }
			public int ACCe { get; set; }
			#endregion

			#region Constructors
			public Stats(long _id)
			{
				ID = _id;

				HP = 0;
				HPe = 0;
				HPp = 0;
				HPpe = 0;

				ATK = 0;
				ATKe = 0;
				ATKp = 0;
				ATKpe = 0;

				DEF = 0;
				DEFe = 0;
				DEFp = 0;
				DEFpe = 0;

				SPD = 0;
				SPDe = 0;
				SPDp = 0;

				CRATE = 0;
				CRATEe = 0;

				CDMG = 0;
				CDMGe = 0;

				RESIST = 0;
				RESISTe = 0;

				ACC = 0;
				ACCe = 0;
			}
			#endregion

			#region Public Static Methods
			public static string GetDesc(EType Type)
			{
				switch (Type)
				{
					case EType.HP: case EType.HPp: return "HP";
					case EType.ATK: case EType.ATKp: return "Attack";
					case EType.DEF: case EType.DEFp: return "Defense";
					case EType.SPD: case EType.SPDp: return "Speed";
					case EType.CRATE: return "Critical rate";
					case EType.CDMG: return "Critical damage";
					case EType.RESIST: return "Resistance";
					case EType.ACC: return "Accuracy";
					case EType.NONE: default: return string.Empty;
				}
			}
			public static bool IsPercentage(EType Type)
			{
				return Type == EType.HPp
					|| Type == EType.ATKp
					|| Type == EType.DEFp
					|| Type == EType.SPDp
					|| Type == EType.CRATE
					|| Type == EType.CDMG;
			}
			public static Stats operator +(Stats a, Stats b)
			{
				return new Stats(-1)
				{
					HP = a.HP + b.HP,
					HPe = a.HPe + b.HPe,
					HPp = a.HPp + b.HPp,
					HPpe = a.HPpe + b.HPpe,

					ATK = a.ATK + b.ATK,
					ATKe = a.ATKe + b.ATKe,
					ATKp = a.ATKp + b.ATKp,
					ATKpe = a.ATKpe + b.ATKpe,

					DEF = a.DEF + b.DEF,
					DEFe = a.DEFe + b.DEFe,
					DEFp = a.DEFp + b.DEFp,
					DEFpe = a.DEFpe + b.DEFpe,

					SPD = a.SPD + b.SPD,
					SPDe = a.SPDe + b.SPDe,
					SPDp = a.SPDp + b.SPDp,

					CRATE = a.CRATE + b.CRATE,
					CRATEe = a.CRATEe + b.CRATEe,

					CDMG = a.CDMG + b.CDMG,
					CDMGe = a.CDMGe + b.CDMGe,

					RESIST = a.RESIST + b.RESIST,
					RESISTe = a.RESISTe + b.RESISTe,

					ACC = a.ACC + b.ACC,
					ACCe = a.ACCe + b.ACCe
				};
			}
			public static Stats operator -(Stats a, Stats b)
			{
				return new Stats(-1)
				{
					HP = a.HP - b.HP,
					HPe = a.HPe - b.HPe,
					HPp = a.HPp - b.HPp,
					HPpe = a.HPpe - b.HPpe,

					ATK = a.ATK - b.ATK,
					ATKe = a.ATKe - b.ATKe,
					ATKp = a.ATKp - b.ATKp,
					ATKpe = a.ATKpe - b.ATKpe,

					DEF = a.DEF - b.DEF,
					DEFe = a.DEFe - b.DEFe,
					DEFp = a.DEFp - b.DEFp,
					DEFpe = a.DEFpe - b.DEFpe,

					SPD = a.SPD - b.SPD,
					SPDe = a.SPDe - b.SPDe,
					SPDp = a.SPDp - b.SPDp,

					CRATE = a.CRATE - b.CRATE,
					CRATEe = a.CRATEe - b.CRATEe,

					CDMG = a.CDMG - b.CDMG,
					CDMGe = a.CDMGe - b.CDMGe,

					RESIST = a.RESIST - b.RESIST,
					RESISTe = a.RESISTe - b.RESISTe,

					ACC = a.ACC - b.ACC,
					ACCe = a.ACCe - b.ACCe
				};
			}
			#endregion
		}
	}
}
