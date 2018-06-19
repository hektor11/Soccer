using Newtonsoft.Json;
using RestSharp;
using Soccer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soccer.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        ///         This is used to total available leagues and output them as a list for the user to
        ///         choose from.
        /// </summary>
        public ActionResult LeagueSelection()
        {
            var client = new RestClient("http://api.football-data.org/v1/competitions");
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            List<Competition> model = JsonConvert.DeserializeObject<List<Competition>>(response.Content);
            return View(model);
        }

        /// <summary>
        ///         This is used to request the league table model which is used to list the 
        ///         placement results for the leage(i.e 1st, 2nd, etc)
        /// </summary>


        public ActionResult LeagueStandings(int? id)
        {
            if(id == null)
            {
                return View("RestrictedAccess");
            }

            else
            {
                var client = new RestClient("http://api.football-data.org/v1/competitions/" + id.ToString() + "/leagueTable");
                var request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                var response = client.Execute(request);
                LeagueStanding model = JsonConvert.DeserializeObject<LeagueStanding>(response.Content);
                return View(model);
            }

        }
        /// <summary>
        /// This returns the model for a team (team crest, name, etc)
        /// </summary>

        public ActionResult TeamView(string url)
        {
            if(url == null)
            {
                return View("RestrictedAccess");
            }

            else
            {
                List <Object> teamDetails = new List<Object>();
                var teamClient = new RestClient(url);
                var teamRequest = new RestRequest(Method.GET);
                teamRequest.RequestFormat = DataFormat.Json;
                var response = teamClient.Execute(teamRequest);

                Team teamModel = JsonConvert.DeserializeObject<Team>(response.Content);
                return View(teamModel);
            }

        }

        //This will be used to gather the list of players for a team
        /*public ActionResult PlayerList(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
        }*/


        public ActionResult Index()
        {
            return View();            
        }

        public ActionResult Teams()
        {
            ViewBag.Message = "Your about page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}