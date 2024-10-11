using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Exiled.API.Features;
using Newtonsoft.Json;
using PluginAPI.Events;

namespace Friday
{
    public class Plugin: Plugin<Config>
    {
        public static Plugin Instance;
        
        EventHandler handler;
        
        public override void OnEnabled()
        {
            Instance = this;
            handler = new EventHandler();
            EventManager.RegisterEvents(this, handler);
            base.OnEnabled();
        }
    
        public override void OnDisabled()
        {
            EventManager.UnregisterEvents(this, handler);
            handler = null;
            base.OnDisabled();
        }
        
        public IEnumerator<float> ReportPlayer(PlayerReportEvent ev)
        {
            Log.Info("Report incoming...");
            var jsonData = new
            {
                reporterName = ev.Player.Nickname,
                reporterId = ev.Player.UserId,
                reportedName = ev.Target.Nickname,
                reportedId = ev.Target.UserId,
                reason = ev.Reason,
                serverName = Server.Name,
                serverType = 0
            };
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Plugin.Instance.Config.Token);
                var content = new StringContent(JsonConvert.SerializeObject(jsonData));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("https://friday.jayxtq.xyz/report",
                    content).GetAwaiter().GetResult();
                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Log.Error("Error from server: " + responseString);
                }
                else
                {
                    Log.Info(responseString);
                }
            }

            yield return 0;
        }
    }
}