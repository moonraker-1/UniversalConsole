using System.ComponentModel;

namespace UniversalConsoleShared
{
    public static class CheckAccess
    {
        private struct AccessTypes
        {
            public const string Free = "Free";
            public const string Advanced = "Advanced";
            public const string Premium = "Premium";
        }


        public static string Check()
        {
            return AccessTypes.Advanced;
        }

        public static string CheckEndDate()
        {
            return DateTime.Now.ToString();
        }
    }
}
