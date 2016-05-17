//para interactuar con el Message Center
namespace CareApp
{
    public class StartRunningTaskMessage { }

    public class StopRunningTaskMessage { }

    public class CancelledMessage { }

    public class TickedMessage
    {
        public string Message { get; set; }
    }
}
