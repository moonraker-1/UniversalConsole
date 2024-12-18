using System.ComponentModel;

namespace ConsoleAccess
{
    public static class CheckAccess
    {
        private struct AccessTypes
        {
            public const string Free = "Free";
            public const string Advanced = "Advanced";
            public const string Premium = "Premium";
        }


        public static bool Check()
        {
            return true;
        }
    }
}
