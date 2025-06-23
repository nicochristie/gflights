using Microsoft.CodeAnalysis.CSharp.Syntax;
using RSLArtifactManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RSLArtifactManager.Database
{
	public class DBManager
	{
		protected static class Queries
		{
			public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RSLArtifactManager;Integrated Security=true;";
			public const string AllArtifacts = "select * from [Artifact]";
			public const string SaveArtifact = "insert into" + " [Artifact] " + 
				"([Level], [Stars], [Set], [Type], [Rarity], [Main], [Sub1], [Sub2], [Sub3], [Sub4], [UpgradesSub1], [UpgradesSub2], [UpgradesSub3], [UpgradesSub4], [IdStats]) " + 
				" values (@Level, @Stars, @Set, @Type, @Rarity, @Main, @Sub1, @Sub2, @Sub3, @Sub4, @UpgradesSub1, @UpgradesSub2, @UpgradesSub3, @UpgradesSub4, @IdStats)";

			public const string StatById = "select * from [Stats] where Id = {0}";
		}
		public static class Tables
		{
			public const string Artifacts = "Artifact";
			public const string Champion = "Champion";
			public const string Global = "Global";
			public const string Stats = "Stats";
		}

		public List<Artifact> AllArtifacts { get; protected set; }
		public List<Champion> AllChampions { get; protected set; }

		protected long _idStat;
		protected long _idArtifact;
		protected long _idChampion;

		private SqlConnection SQLConnection { get; set; }

		public DBManager()
		{
			AllArtifacts = new List<Artifact>();

			SQLConnection = new SqlConnection();
			SQLConnection.ConnectionString = Queries.ConnectionString;

			GetAllArtifacts(-1);

			_idStat = 1;
			_idArtifact = AllArtifacts.Select(x => x.ID).Max() + 1;
			_idChampion = 1;

			#region Weapon
			Artifact weapon = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Weapon,
				Rarity = Artifact.ERarity.Epic,
				Set = Artifact.ESet.Shield,
			};
			weapon.SetStars(5);
			weapon.SetLevel(12);

			weapon.Main = Global.Stats.EType.ATK;
			weapon.Sub1 = Global.Stats.EType.HPp;
			weapon.Sub2 = Global.Stats.EType.CRATE;
			weapon.Sub3 = Global.Stats.EType.HP;

			weapon.Stats.ATK = 143;
			weapon.Stats.CRATE = 5;
			weapon.Stats.HPp = 9;
			weapon.Stats.HP = 1047;
			#endregion
			#region Helmet
			Artifact helmet = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Helmet,
				Rarity = Artifact.ERarity.Epic,
				Set = Artifact.ESet.Immortal
			};
			helmet.SetStars(5);
			helmet.SetLevel(16);

			helmet.Main = Global.Stats.EType.HP;
			helmet.Sub1 = Global.Stats.EType.CDMG;
			helmet.Sub2 = Global.Stats.EType.SPD;
			helmet.Sub3 = Global.Stats.EType.ATKp;
			helmet.Sub4 = Global.Stats.EType.DEF;

			helmet.Stats.HP = 3480;
			helmet.Stats.CDMG = 4;
			helmet.Stats.SPD = 13;
			helmet.Stats.ATKp = 10;
			helmet.Stats.DEF = 21;

			helmet.UpgradesSub2 = 2;
			helmet.UpgradesSub3 = 1;
			#endregion
			#region Shield
			Artifact shield = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Shield,
				Rarity = Artifact.ERarity.Legendary,
				Set = Artifact.ESet.Immortal
			};
			shield.SetStars(4);
			shield.SetLevel(16);

			shield.Main = Global.Stats.EType.DEF;
			shield.Sub1 = Global.Stats.EType.SPD;
			shield.Sub2 = Global.Stats.EType.RESIST;
			shield.Sub3 = Global.Stats.EType.HP;
			shield.Sub4 = Global.Stats.EType.HPp;

			shield.Stats.DEF = 190;
			shield.Stats.SPD = 3;
			shield.Stats.RESIST = 14;
			shield.Stats.HP = 415;
			shield.Stats.HPe = 100;
			shield.Stats.HPp = 11;
			shield.Stats.HPpe = 2;

			shield.UpgradesSub2 = 1;
			shield.UpgradesSub3 = 1;
			shield.UpgradesSub4 = 2;
			#endregion
			#region Gauntlets
			Artifact gauntlets = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Gauntlets,
				Rarity = Artifact.ERarity.Rare,
				Set = Artifact.ESet.Shield
			};
			gauntlets.SetStars(5);
			gauntlets.SetLevel(16);

			gauntlets.Main = Global.Stats.EType.HPp;
			gauntlets.Sub1 = Global.Stats.EType.CRATE;
			gauntlets.Sub2 = Global.Stats.EType.DEF;
			gauntlets.Sub3 = Global.Stats.EType.ATKp;
			gauntlets.Sub4 = Global.Stats.EType.DEFp;

			gauntlets.Stats.HPp = 50;
			gauntlets.Stats.CRATE = 10;
			gauntlets.Stats.DEF = 28;
			gauntlets.Stats.ATKp = 5;
			gauntlets.Stats.DEFp = 4;

			gauntlets.UpgradesSub1 = 1;
			gauntlets.UpgradesSub2 = 1;
			#endregion
			#region Chestplate
			Artifact chestplate = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Chestplate,
				Rarity = Artifact.ERarity.Legendary,
				Set = Artifact.ESet.Shield
			};
			chestplate.SetStars(4);
			chestplate.SetLevel(12);

			chestplate.Main = Global.Stats.EType.HPp;
			chestplate.Sub1 = Global.Stats.EType.CRATE;
			chestplate.Sub2 = Global.Stats.EType.CDMG;
			chestplate.Sub3 = Global.Stats.EType.ACC;
			chestplate.Sub4 = Global.Stats.EType.ATK;

			chestplate.Stats.HPp = 26;
			chestplate.Stats.CRATE = 8;
			chestplate.Stats.CDMG = 4;
			chestplate.Stats.ACC = 21;
			chestplate.Stats.ATK = 6;

			chestplate.UpgradesSub1 = 1;
			chestplate.UpgradesSub3 = 2;
			#endregion
			#region Boots
			Artifact boots = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Boots,
				Rarity = Artifact.ERarity.Rare,
				Set = Artifact.ESet.Shield
			};
			boots.SetStars(5);
			boots.SetLevel(16);

			boots.Main = Global.Stats.EType.HPp;
			boots.Sub1 = Global.Stats.EType.CDMG;
			boots.Sub2 = Global.Stats.EType.CRATE;
			boots.Sub3 = Global.Stats.EType.ACC;
			boots.Sub4 = Global.Stats.EType.DEFp;

			boots.Stats.HPp = 50;
			boots.Stats.CDMG = 16;
			boots.Stats.CRATE = 6;
			boots.Stats.ACC = 10;
			boots.Stats.DEFp = 5;

			boots.UpgradesSub1 = 2;
			#endregion

			#region Ring
			Artifact ring = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Ring,
				Rarity = Artifact.ERarity.Legendary,
				Set = Artifact.ESet.None
			};
			ring.SetStars(6);
			ring.SetLevel(12);

			ring.Main = Global.Stats.EType.ATK;
			ring.Sub1 = Global.Stats.EType.HP;
			ring.Sub2 = Global.Stats.EType.DEFp;
			ring.Sub3 = Global.Stats.EType.HPp;
			ring.Sub4 = Global.Stats.EType.ATKp;

			ring.Stats.ATK = 170;
			ring.Stats.HP = 238;
			ring.Stats.DEFp = 13;
			ring.Stats.HPp = 11;
			ring.Stats.ATKp = 10;

			ring.UpgradesSub2 = 1;
			ring.UpgradesSub3 = 1;
			ring.UpgradesSub4 = 1;
			#endregion
			#region Amulet
			Artifact amulet = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Amulet,
				Rarity = Artifact.ERarity.Epic,
				Set = Artifact.ESet.None
			};
			amulet.SetStars(5);
			amulet.SetLevel(16);

			amulet.Main = Global.Stats.EType.HP;
			amulet.Sub1 = Global.Stats.EType.ATK;
			amulet.Sub2 = Global.Stats.EType.CDMG;
			amulet.Sub3 = Global.Stats.EType.ACC;
			amulet.Sub4 = Global.Stats.EType.DEF;

			amulet.Stats.HP = 3480;
			amulet.Stats.ATK = 49;
			amulet.Stats.CDMG = 8;
			amulet.Stats.ACC = 11;
			amulet.Stats.DEF = 22;

			amulet.UpgradesSub1 = 2;
			amulet.UpgradesSub2 = 1;
			#endregion
			#region Banner
			Artifact banner = new Artifact(_idArtifact++, _idStat++)
			{
				Type = Artifact.EType.Banner,
				Rarity = Artifact.ERarity.Uncommon,
				Set = Artifact.ESet.None
			};
			banner.SetStars(4);
			banner.SetLevel(12);

			banner.Main = Global.Stats.EType.ACC;
			banner.Sub1 = Global.Stats.EType.HPp;
			banner.Sub2 = Global.Stats.EType.ATKp;
			banner.Sub3 = Global.Stats.EType.HP;

			banner.Stats.ACC = 41;
			banner.Stats.HPp = 8;
			banner.Stats.ATKp = 3;
			banner.Stats.HP = 138;

			banner.UpgradesSub1 = 1;
			#endregion

			//AllArtifacts.Add(weapon);
			AllArtifacts.Add(helmet);
			AllArtifacts.Add(shield);
			AllArtifacts.Add(gauntlets);
			AllArtifacts.Add(chestplate);
			AllArtifacts.Add(boots);
			AllArtifacts.Add(ring);
			AllArtifacts.Add(amulet);
			AllArtifacts.Add(banner);

			AllChampions = new List<Champion>()
			{
				//MiscreatedMonster(),
				//MausoleumMage()
			};
		}

		protected Champion MiscreatedMonster()
		{
			Champion MiscreatedMonster = new Champion(_idChampion++)
			{
				Name = "Miscreated Monster",
				Faction = Champion.EFaction.KnightRevenant,
				Affinity = Champion.EAffinity.Magic,
				Type = Champion.EType.HP,
				Rarity = Champion.ERarity.Epic
			};
			MiscreatedMonster.SetLevel(60);
			MiscreatedMonster.SetStars(6);
			MiscreatedMonster.SetAscendedStars(6);

			MiscreatedMonster.Base.HP = 22965;
			MiscreatedMonster.Base.ATK = 958;
			MiscreatedMonster.Base.DEF = 815;
			MiscreatedMonster.Base.SPD = 95;
			MiscreatedMonster.Base.CRATE = 15;
			MiscreatedMonster.Base.CDMG = 50;
			MiscreatedMonster.Base.RESIST = 45;
			MiscreatedMonster.Base.ACC = 0;

			MiscreatedMonster.Masteries.HP = 517;
			MiscreatedMonster.Masteries.ATK = 0;
			MiscreatedMonster.Masteries.DEF = 0;
			MiscreatedMonster.Masteries.SPD = 0;
			MiscreatedMonster.Masteries.CRATE = 0;
			MiscreatedMonster.Masteries.CDMG = 0;
			MiscreatedMonster.Masteries.RESIST = 10;
			MiscreatedMonster.Masteries.ACC = 10;

			MiscreatedMonster.Artifacts.Weapon = AllArtifacts.First(x => x.ID == 0);
			MiscreatedMonster.Artifacts.Helmet = AllArtifacts.First(x => x.ID == 1);
			MiscreatedMonster.Artifacts.Shield = AllArtifacts.First(x => x.ID == 2);
			MiscreatedMonster.Artifacts.Gauntlets = AllArtifacts.First(x => x.ID == 3);
			MiscreatedMonster.Artifacts.Chestplate = AllArtifacts.First(x => x.ID == 4);
			MiscreatedMonster.Artifacts.Boots = AllArtifacts.First(x => x.ID == 5);
			MiscreatedMonster.Artifacts.Ring = AllArtifacts.First(x => x.ID == 6);
			MiscreatedMonster.Artifacts.Amulet = AllArtifacts.First(x => x.ID == 7);
			MiscreatedMonster.Artifacts.Banner = AllArtifacts.First(x => x.ID == 8);

			return MiscreatedMonster;
		}
		protected Champion MausoleumMage()
		{
			Champion MausoleumMage = new Champion(_idChampion++)
			{
				Name = "Mausoleum Mage",
				Faction = Champion.EFaction.UndeadHordes,
				Affinity = Champion.EAffinity.Force,
				Type = Champion.EType.Support,
				Rarity = Champion.ERarity.Epic
			};
			MausoleumMage.SetLevel(53);
			MausoleumMage.SetStars(6);
			MausoleumMage.SetAscendedStars(6);

			MausoleumMage.Base.HP = 18135;
			MausoleumMage.Base.ATK = 984;
			MausoleumMage.Base.DEF = 881;
			MausoleumMage.Base.SPD = 104;
			MausoleumMage.Base.CRATE = 15;
			MausoleumMage.Base.CDMG = 50;
			MausoleumMage.Base.RESIST = 30;
			MausoleumMage.Base.ACC = 0;

			MausoleumMage.Masteries.HP = 517;
			MausoleumMage.Masteries.ATK = 0;
			MausoleumMage.Masteries.DEF = 0;
			MausoleumMage.Masteries.SPD = 0;
			MausoleumMage.Masteries.CRATE = 0;
			MausoleumMage.Masteries.CDMG = 0;
			MausoleumMage.Masteries.RESIST = 10;
			MausoleumMage.Masteries.ACC = 10;

			return MausoleumMage;
		}

		protected void GetAllArtifacts(long userID)
		{
			DataTable tableArtifacts = new DataTable();
			using (SqlDataAdapter adapterArtifacts = new SqlDataAdapter(Queries.AllArtifacts, SQLConnection))
				adapterArtifacts.Fill(tableArtifacts);

			foreach (DataRow objectArtifact in tableArtifacts.Rows)
			{
				_idArtifact = objectArtifact["Id"] as long? ?? -1;
				_idStat = objectArtifact["IdStats"] as long? ?? -1;

				if (_idArtifact != -1 && _idStat != -1)
				{
					Artifact newArtifact = new Artifact(_idArtifact, _idStat)
					{
						Set = (Artifact.ESet)Enum.Parse(typeof(Artifact.ESet), ((byte)objectArtifact[nameof(Artifact.Set)]).ToString()),
						Type = (Artifact.EType)Enum.Parse(typeof(Artifact.EType), ((byte)objectArtifact[nameof(Artifact.Type)]).ToString()),
						Rarity = (Artifact.ERarity)Enum.Parse(typeof(Artifact.ERarity), ((byte)objectArtifact[nameof(Artifact.Rarity)]).ToString())
					};
					newArtifact.SetStars((byte)objectArtifact[nameof(Artifact.Stars)]);
					newArtifact.SetLevel((byte)objectArtifact[nameof(Artifact.Level)]);

					newArtifact.Main = (Global.Stats.EType)Enum.Parse(typeof(Global.Stats.EType), ((byte)objectArtifact[nameof(Artifact.Main)]).ToString());
					newArtifact.Sub1 = (Global.Stats.EType)Enum.Parse(typeof(Global.Stats.EType), ((byte)objectArtifact[nameof(Artifact.Sub1)]).ToString());
					newArtifact.Sub2 = (Global.Stats.EType)Enum.Parse(typeof(Global.Stats.EType), ((byte)objectArtifact[nameof(Artifact.Sub2)]).ToString());
					newArtifact.Sub3 = (Global.Stats.EType)Enum.Parse(typeof(Global.Stats.EType), ((byte)objectArtifact[nameof(Artifact.Sub3)]).ToString());
					newArtifact.Sub4 = (Global.Stats.EType)Enum.Parse(typeof(Global.Stats.EType), ((byte)objectArtifact[nameof(Artifact.Sub4)]).ToString());

					newArtifact.UpgradesSub1 = objectArtifact[nameof(Artifact.UpgradesSub1)] as byte? ?? 0;
					newArtifact.UpgradesSub2 = objectArtifact[nameof(Artifact.UpgradesSub2)] as byte? ?? 0;
					newArtifact.UpgradesSub3 = objectArtifact[nameof(Artifact.UpgradesSub3)] as byte? ?? 0;
					newArtifact.UpgradesSub4 = objectArtifact[nameof(Artifact.UpgradesSub4)] as byte? ?? 0;

					DataTable tableStat = new DataTable();
					tableStat.Columns.Add("Id", typeof(long));
					tableStat.PrimaryKey = new DataColumn[] { tableStat.Columns["Id"] };
					using (SqlDataAdapter adapterStat = new SqlDataAdapter(string.Format(Queries.StatById, _idStat), SQLConnection))
						adapterStat.Fill(tableStat);

					DataRow objectStat = tableStat.Rows.Find(_idStat);

					newArtifact.Stats.HP = objectStat[Global.Stats.EType.HP.ToString()] as int? ?? 0;
					newArtifact.Stats.HPe = objectStat[Global.Stats.EType.HPe.ToString()] as int? ?? 0;
					newArtifact.Stats.HPp = objectStat[Global.Stats.EType.HPp.ToString()] as int? ?? 0;
					newArtifact.Stats.HPpe = objectStat[Global.Stats.EType.HPpe.ToString()] as int? ?? 0;

					newArtifact.Stats.ATK = objectStat[Global.Stats.EType.ATK.ToString()] as int? ?? 0;
					newArtifact.Stats.ATKe = objectStat[Global.Stats.EType.ATKe.ToString()] as int? ?? 0;
					newArtifact.Stats.ATKp = objectStat[Global.Stats.EType.ATKp.ToString()] as int? ?? 0;
					newArtifact.Stats.ATKpe = objectStat[Global.Stats.EType.ATKpe.ToString()] as int? ?? 0;

					newArtifact.Stats.DEF = objectStat[Global.Stats.EType.DEF.ToString()] as int? ?? 0;
					newArtifact.Stats.DEFe = objectStat[Global.Stats.EType.DEFe.ToString()] as int? ?? 0;
					newArtifact.Stats.DEFp = objectStat[Global.Stats.EType.DEFp.ToString()] as int? ?? 0;
					newArtifact.Stats.DEFpe = objectStat[Global.Stats.EType.DEFpe.ToString()] as int? ?? 0;

					newArtifact.Stats.SPD = objectStat[Global.Stats.EType.SPD.ToString()] as int? ?? 0;
					newArtifact.Stats.SPDe = objectStat[Global.Stats.EType.SPDe.ToString()] as int? ?? 0;
					newArtifact.Stats.SPDp = objectStat[Global.Stats.EType.SPDp.ToString()] as int? ?? 0;

					newArtifact.Stats.CRATE = objectStat[Global.Stats.EType.CRATE.ToString()] as int? ?? 0;
					newArtifact.Stats.CRATEe = objectStat[Global.Stats.EType.CRATEe.ToString()] as int? ?? 0;

					newArtifact.Stats.CDMG = objectStat[Global.Stats.EType.CDMG.ToString()] as int? ?? 0;
					newArtifact.Stats.CDMGe = objectStat[Global.Stats.EType.CDMGe.ToString()] as int? ?? 0;

					newArtifact.Stats.RESIST = objectStat[Global.Stats.EType.RESIST.ToString()] as int? ?? 0;
					newArtifact.Stats.RESISTe = objectStat[Global.Stats.EType.RESISTe.ToString()] as int? ?? 0;

					newArtifact.Stats.ACC = objectStat[Global.Stats.EType.ACC.ToString()] as int? ?? 0;
					newArtifact.Stats.ACCe = objectStat[Global.Stats.EType.ACCe.ToString()] as int? ?? 0;

					AllArtifacts.Add(newArtifact);
				}
			}
		}

		public long GetNextID(string table)
		{
			string sqlCommand = string.Format("select IDENT_CURRENT('{0}') + IDENT_INCR('{0}') as NextIndentity", table);
			DataTable nextID = new DataTable();
			using SqlDataAdapter adapterArtifacts = new SqlDataAdapter(sqlCommand, SQLConnection);
			adapterArtifacts.Fill(nextID);
			return (long)(decimal)nextID.Rows[0].ItemArray[0];
		}

		public bool Commit(Artifact artifact)
		{
			using (SqlCommand cmd = new SqlCommand(Queries.SaveArtifact, SQLConnection))
			{
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Level", SqlDbType.TinyInt).Value = artifact.Level;
				cmd.Parameters.AddWithValue("@Stars", SqlDbType.TinyInt).Value = artifact.Stars;
				cmd.Parameters.AddWithValue("@Set", SqlDbType.TinyInt).Value = (byte)artifact.Set;
				cmd.Parameters.AddWithValue("@Type", SqlDbType.TinyInt).Value = (byte)artifact.Type;
				cmd.Parameters.AddWithValue("@Rarity", SqlDbType.TinyInt).Value = (byte)artifact.Rarity;
				cmd.Parameters.AddWithValue("@Main", SqlDbType.TinyInt).Value = (byte)artifact.Main;
				cmd.Parameters.AddWithValue("@Sub1", SqlDbType.TinyInt).Value = (byte)artifact.Sub1;
				cmd.Parameters.AddWithValue("@Sub2", SqlDbType.TinyInt).Value = (byte)artifact.Sub2;
				cmd.Parameters.AddWithValue("@Sub3", SqlDbType.TinyInt).Value = (byte)artifact.Sub3;
				cmd.Parameters.AddWithValue("@Sub4", SqlDbType.TinyInt).Value = (byte)artifact.Sub4;
				cmd.Parameters.AddWithValue("@UpgradesSub1", SqlDbType.TinyInt).Value = artifact.UpgradesSub1;
				cmd.Parameters.AddWithValue("@UpgradesSub2", SqlDbType.TinyInt).Value = artifact.UpgradesSub2;
				cmd.Parameters.AddWithValue("@UpgradesSub3", SqlDbType.TinyInt).Value = artifact.UpgradesSub3;
				cmd.Parameters.AddWithValue("@UpgradesSub4", SqlDbType.TinyInt).Value = artifact.UpgradesSub4;

				cmd.Parameters.AddWithValue("@IdStats", SqlDbType.BigInt).Value = artifact.Stats.ID;

				try
				{
					SQLConnection.Open();

					SqlDataReader dr = cmd.ExecuteReader();
					string text = string.Empty;
					while (dr.Read())
					{
						text += dr.GetString(0);
					}

					cmd.ExecuteNonQuery();
				}
				catch (Exception)
				{
					// Log error
				}
			}
			return true;
		}
	}
}
