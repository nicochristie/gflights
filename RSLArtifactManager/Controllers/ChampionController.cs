using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSLArtifactManager.Database;
using RSLArtifactManager.Models;

namespace RSLArtifactManager.Controllers
{
  public class ChampionController : Controller
  {
    public DBManager DBManager { get; set; }

    public ChampionController()
    {
      DBManager = new DBManager();
    }

    public IActionResult Index()
    {
      return View(DBManager.AllChampions);
    }

    public ViewResult Details(long _id)
    {
      Champion target = DBManager.AllChampions.FirstOrDefault(x => x.ID == _id);
      return View(target);
    }
  }
}