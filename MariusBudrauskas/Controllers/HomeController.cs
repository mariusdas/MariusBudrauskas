using MariusBudrauskas.Context;
using MariusBudrauskas.Models;
using Microsoft.Ajax.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MariusBudrauskas.Controllers
{
    public class HomeController : Controller
    {
        ScoreContext db = new ScoreContext();

        public ActionResult Index()
        {
            var scores = db.Scores;

            var realScores = new List<Score>();

            foreach (Score s in scores.DistinctBy(p => new { p.Task, p.Nickname }).ToList())
            {
                var find = realScores.FirstOrDefault(x => x.Nickname == s.Nickname);
                if (find != null)
                {
                    find.Point += s.Point;
                }
                else
                {
                    realScores.Add(s);
                }
            }

            ViewBag.Scores = realScores;

            return View();
        }

        public ActionResult Task()
        {
            TasksList();

            return View();
        }

        [HttpPost]
        public ActionResult Task(Task model)
        {
            if (ModelState.IsValid)
            {
                var client = new RestClient("http://phpcodechecker.com/api/?code=" + model.Solution);
                var request = new RestRequest(Method.POST);

                request.RequestFormat = DataFormat.Json;

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    db.Scores.Add(new Score() { Nickname = model.Nickname, Point = 1, Task = model.TaskQuestion, Solution = model.Solution});
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Scores.Add(new Score() { Nickname = model.Nickname, Point = 0, Task = model.TaskQuestion, Solution = model.Solution });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            TasksList();

            return View();
        }

        private void TasksList()
        {
            ViewBag.Message = "Attention! Solution is checked only with PHP compiler!";

            var taskList = new List<TaskClass>()
            {
                new TaskClass{ Id=0, Text = "Fibonacci" },
                new TaskClass{ Id=1, Text = "Bubble Sort" }
            };

            ViewBag.TaskList = taskList;
        }
    }
    
    class TaskClass
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}