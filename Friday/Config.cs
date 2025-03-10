using System.ComponentModel;

namespace Friday {
    using Exiled.API.Interfaces;
    
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public string Token { get; set; } = "your_token_here";
        [Description("If you aren't self hosting, don't edit this.")]
        public string fqdn { get; set; } = "friday.jxtq.moe";
    }
}