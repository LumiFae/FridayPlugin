using System.Security.Cryptography.X509Certificates;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using MEC;

namespace Friday
{
    public class EventHandler
    {
        [PluginEvent(ServerEventType.PlayerReport)]
        public void PlayerReport(PlayerReportEvent ev)
        {
            Timing.RunCoroutine(Plugin.Instance.ReportPlayer(ev));
        }
    }
}