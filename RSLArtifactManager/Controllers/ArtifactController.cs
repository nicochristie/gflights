using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RSLArtifactManager.Database;
using RSLArtifactManager.Models;

namespace RSLArtifactManager.Controllers
{
	public class ArtifactController : Controller
	{
		public DBManager DBManager { get; set; }

		public ArtifactController()
		{
			DBManager = new DBManager();
		}

		public ViewResult Index()
		{
			return View(DBManager.AllArtifacts);
		}

		public ViewResult Details(long _id)
		{
			Artifact target = DBManager.AllArtifacts.FirstOrDefault(x => x.ID == _id);
			return View(target);
		}

		public ActionResult Save(Artifact target)
		{
			DBManager.Commit(target);
			DBManager.AllArtifacts.Add(target);
			return RedirectToAction("Details", "Artifact", target.ID);
		}

		public ViewResult Edit(ushort _id)
		{
			Artifact target;
			if (_id > 0) target = DBManager.AllArtifacts.FirstOrDefault(x => x.ID == _id);
			else
			{
				long nextStatsID = DBManager.GetNextID(DBManager.Tables.Stats);
				long nextArtifactID = DBManager.GetNextID(DBManager.Tables.Artifacts);
				Global.Stats stats = new Global.Stats(nextStatsID);
				target = new Artifact(nextArtifactID, nextStatsID);
			}
			return View(target);
		}

		public void SelectedIndexChanged(Artifact artifact)
		{
			//bool test = true;
		}
	}
}