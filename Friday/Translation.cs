using Exiled.API.Interfaces;

namespace Friday
{
    public class Translation : ITranslation

    {
        public string ReportInServer { get; private set; } =
            "Your report has been sent and a ticket has been opened under {0}";
        public string ReportNotInServer { get; private set; } =
            "Your report has been sent and a ticket has been opened under {0}. Please connect your Steam to Discord to view the ticket.";
    }
}