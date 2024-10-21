using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UnityMembers.Models;

public class FFXIVController : Controller
{
    private readonly string mountApiUrl = "https://ffxivcollect.com/api/mounts/186";
    private readonly string achievementApiUrl = "https://ffxivcollect.com/api/achievements/1";
    public async Task<ActionResult> Details()
    {
        var mount = await GetMountDetails(mountApiUrl);
        var achievement = await GetAchievementDetails(achievementApiUrl);

        mount.Achievement = achievement;

        return View(mount);
    }
    private async Task<Mount> GetMountDetails(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Mount>(result);
        }
    }

    private async Task<Achievement> GetAchievementDetails(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Achievement>(result);
        }
    }
}
