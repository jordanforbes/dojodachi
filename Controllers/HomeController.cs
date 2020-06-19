using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("Fullness") == null)
            {
                HttpContext.Session.SetInt32("Fullness",20);
            }

            if (HttpContext.Session.GetInt32("Happiness") == null)
            {
                HttpContext.Session.SetInt32("Happiness", 20);
            }

            if (HttpContext.Session.GetInt32("Meals") == null)
            {
                HttpContext.Session.SetInt32("Meals", 20);
            }

            if (HttpContext.Session.GetInt32("Energy") == null)
            {
                HttpContext.Session.SetInt32("Energy", 20);
            }
            if(HttpContext.Session.GetString("Description") == null)
            {
                HttpContext.Session.SetString("Description","What will you do now?");
            }
            int fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int happiness = (int)HttpContext.Session.GetInt32("Happiness");
            if(fullness<1 || happiness <1)
            {
                HttpContext.Session.SetString("Description","Your DojoDachi has died you're a terrible parent");
            }else if(happiness >99){
                HttpContext.Session.SetString("Description", "Your DojoDachi has ascended to the next plane of existence. Parent of the century.");
            }
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness"); 
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Description = HttpContext.Session.GetString("Description");
            
            
            
            return View();
        }

        [HttpGet("feed")]
        public IActionResult Feed(Monster pet)
        {
            int fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int meals = (int)HttpContext.Session.GetInt32("Meals");
            Random Food = new Random();
            int Eat = (int)Food.Next(5,11);
            Console.WriteLine(Eat);
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%#^^^^^^^^^^@#$");
            meals --;
            fullness +=Eat;
            if(meals > 0){
                HttpContext.Session.SetInt32("Fullness", fullness);
                HttpContext.Session.SetInt32("Meals", meals);
                HttpContext.Session.SetString("Description", $"You feed your Dojodachi a meal, its fullness is at {fullness}, and you have {meals} left");
            }else{
                HttpContext.Session.SetString("Description", $"You're all out of food.");
            }
            
            return Redirect("/");
        }

        [HttpGet("play")]
        public IActionResult Play(Monster pet)
        {
            Random Dice = new Random();
            int Throw = (int)Dice.Next(0,4);
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%#^^^^^^^^^^@#$");
            int happiness = (int)HttpContext.Session.GetInt32("Happiness");
            int energy = (int)HttpContext.Session.GetInt32("Energy");

            if(energy > 0 && Throw !=0){
                energy -=5;
                happiness +=5;
                HttpContext.Session.SetInt32("Energy", energy);
                HttpContext.Session.SetInt32("Happiness", happiness);
                HttpContext.Session.SetString("Description", $"You play with your Dojodachi, it Loves it. Your Domodachi gains 5 happiness, its happiness is now {happiness}, its energy is now {energy} ");

            }else if(energy >0){
                energy -=5;
                HttpContext.Session.SetInt32("Energy", energy);
                HttpContext.Session.SetString("Description", $"Your Dojodachi didn't have a great time. its happiness is still {happiness} and its energy is now {energy}");

            }else{
                HttpContext.Session.SetString("Description", $"Your Dojodachi is too tired to play.");

            }
            return Redirect("/");
        }

        [HttpGet("work")]
        public IActionResult Work(Monster pet)
        {
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%#^^^^^^^^^^@#$");
            Random Job = new Random();
            int Earned = (int)Job.Next(1,4);
            int meals = (int)HttpContext.Session.GetInt32("Meals");
            int energy = (int)HttpContext.Session.GetInt32("Energy");
            if(energy > 0 )
            {   
                meals +=Earned;
                HttpContext.Session.SetInt32("Meals", meals);
                HttpContext.Session.SetInt32("Energy", energy - 5);
                HttpContext.Session.SetString("Description",$"Your Dojodachi went to work and earned {meals} meals!");

            }else{
                HttpContext.Session.SetString("Description",$"Your Dojodachi is too tired to work and instead went on strike.");
            }
            return Redirect("/");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep(Monster pet)
        {
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%#^^^^^^^^^^@#$");
            int energy = (int)HttpContext.Session.GetInt32("Energy");
            energy +=15;
            int fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int happiness = (int)HttpContext.Session.GetInt32("Happiness");
            HttpContext.Session.SetInt32("Energy", energy);
            HttpContext.Session.SetInt32("Fullness", fullness - 5);
            HttpContext.Session.SetInt32("Happiness", happiness - 5);
            HttpContext.Session.SetString("Description", $"Your Dojodachi went to sleep and gained 15 energy, its energy is now {energy}");

            return Redirect("/");
        }
        
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
