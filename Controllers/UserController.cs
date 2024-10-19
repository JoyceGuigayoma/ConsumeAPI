using UnityMembers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace UnityMembers.Controllers
{
    public class UserController : Controller
    {

       
        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://localhost:7083/api/user"; 
            List<User> users = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(result);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    
                    throw new Exception($"Error: {response.StatusCode}, Content: {errorContent}");
                }
            }

            return View(users);
        }


        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            string apiUrl = "https://localhost:7083/api/user";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

              HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { recruitStatus = user.recruit });
                }
            }
            return View(user);
        }

        
        public async Task<ActionResult> Edit(string username)
        {
            string apiUrl = "https://localhost:7083/api/user";
            User user = null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{apiUrl}?username={username}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(result);

                    user = users?.FirstOrDefault();
                }
            }

            if (user == null)
            {
                return NotFound(); 
            }

            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            string apiUrl = $"https://localhost:7083/api/user/{user.username}"; 
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PatchAsync(apiUrl, content); 

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { recruitStatus = user.recruit });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error updating user: {errorContent}");
                    return View(user); 
                }
            }
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public async Task<ActionResult> Delete(string username)
        {
            
            string apiUrl = $"https://localhost:7083/api/user?username={username}";
            List<User> users = new List<User>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(result); 
                }
            }

            var user = users.FirstOrDefault();

            if (user == null)
            {
                return NotFound(); 
            }

            return View(user); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string username, string password)
        {
            string apiUrl = "https://localhost:7083/api/user";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}?username={username}&password={password}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error deleting user: {errorContent}");
                }
            }    
            return View(new User { username = username }); 
        }

    }
}
