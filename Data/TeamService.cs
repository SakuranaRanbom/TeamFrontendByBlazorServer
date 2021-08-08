using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Http.HttpClient;


namespace TeamFrontEndByBlazorServer.Data
{
    public class TeamService
    {
        HttpClient http = new HttpClient{BaseAddress = new Uri(Startup.baseUrl)};//{BaseAddress = new Uri("http://47.94.89.77:31905")};
        public async Task<List<Team>> GetTeamsAsync()
        {
            
             
            var result =await http.GetAsync("team/getOK");
            if (result.IsSuccessStatusCode)
            {
                var str = await result.Content.ReadAsStringAsync();
                //JObject obj = JObject.Parse(str);
                var v = JsonConvert.DeserializeObject<List<Team>>(str);
                return v;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> HasTeam(string token)
        {
            
            http.DefaultRequestHeaders.Remove("Authorization");
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = await http.GetAsync("team/hasteam");
            if (result.IsSuccessStatusCode)
            {
                var str = await result.Content.ReadAsStringAsync();
                return bool.Parse(str);

            }
            else
            {
                return null;
            }
        }
    }
}