namespace SkyMod.RemoteConsole.Client
{
    public class ConsoleClient
    {
        public static void Info(string message, params object[] args)
        {
            if (args != null)
                LogMessage("info", string.Format(message, args));
            else
                LogMessage("info", message);
        }

        public static void Warn(string message, params object[] args)
        {
            if (args != null)
                LogMessage("warn", string.Format(message, args));
            else
                LogMessage("warn", message);
        }

        public static void Error(string message, params object[] args)
        {
            if (args != null)
                LogMessage("error", string.Format(message, args));
            else
                LogMessage("error", message);
        }


        private static void LogMessage(string level, string message)
        {
            var request = new ColossalFramework.HTTP.Request("POST", "http://127.0.0.1:44324/skymod-remotecon/?level=" + level, System.Text.Encoding.Default.GetBytes(message));
            request.Send();
        }
    }
}
