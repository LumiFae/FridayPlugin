using Exiled.API.Features;
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
    }
}