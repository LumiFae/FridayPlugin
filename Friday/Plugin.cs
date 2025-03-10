using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Exiled.API.Enums;
using Exiled.API.Features;
using Newtonsoft.Json;
using PluginAPI.Events;

namespace Friday
{
    public class Plugin: Plugin<Config>
    {
        public override string Name => "Friday";
        public override string Author => "LumiFae";
        public override Version Version => new (1, 2, 5);
        public override Version RequiredExiledVersion => new (8, 11, 0);
        
        public static Plugin Instance;
        
        private EventHandler _handler;
        
        private HttpClient _client;
        
        public override void OnEnabled()
        {
            Instance = this;
            _handler = new();
            
            _client = new();
            _client.DefaultRequestHeaders.Authorization = new ("Bearer", Config.Token);
            
            EventManager.RegisterEvents(this, _handler);
            base.OnEnabled();
        }
    
        public override void OnDisabled()
        {
            EventManager.UnregisterEvents(this, _handler);
            _handler = null;
            _client = null;
            base.OnDisabled();
        }
        
        public async Task ReportPlayer(PlayerReportEvent ev)
        {
            Log.Debug("Report incoming...");
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
            
            StringContent content = new (JsonConvert.SerializeObject(jsonData));
            
            content.Headers.ContentType = new ("application/json");
            
            HttpResponseMessage response = await _client.PostAsync($"https://{Config.fqdn}/report", content);
            
            string responseString = await response.Content.ReadAsStringAsync();
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("Error from server: " + responseString);
            }
            else
            {
                Log.Debug(responseString);
            }
        }
    }
}