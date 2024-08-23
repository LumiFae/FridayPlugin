using System.Net;
using System.Text;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Handlers = Exiled.Events.Handlers;

namespace Friday
{
    public class Friday: Plugin<Config>
    {
        public override void OnEnabled()
        {
            Handlers.Server.LocalReporting += LocalReporting;
        }
    
        public override void OnDisabled()
        {
            Handlers.Server.LocalReporting -= LocalReporting;
        }

        void LocalReporting(LocalReportingEventArgs ev)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            client.Headers.Add("Authorization", Config.Token);
            var jsonData = new
            {
                reporterName = ev.Player.Nickname,
                reporterId = ev.Player.UserId,
                reportedName = ev.Target.Nickname,
                reportedId = ev.Target.UserId,
                reason = ev.Reason,
                serverName = Server.Name
            };
            client.UploadData("https://friday.jayxtq.xyz/report", "POST", Encoding.UTF8.GetBytes(jsonData.ToString()));
        }
    }
}