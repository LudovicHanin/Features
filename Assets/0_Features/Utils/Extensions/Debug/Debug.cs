using _0_Features.Utils.Const;
using UnityEngine;

namespace _0_Features.Utils.Extensions.Debug
{
    public static class Debug
    {
        public enum DebugType
        {
            LOG,
            WARNING,
            ERROR
        }
        public static bool Assert(this bool condition, string errorMessage, DebugType debugType = DebugType.ERROR)
        {
            if (condition)
                return true;
        
            switch (debugType)
            {
                case DebugType.LOG:
                    Log(errorMessage);
                    break;
                case DebugType.WARNING:
                    Warning(errorMessage, DebugColor.Blocked);
                    break;
                case DebugType.ERROR:
                    UnityEngine.Debug.LogError(errorMessage);
                    break;
            }
            
            return false;
        }

        public static void Log(this string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public static void Log(this string message, Color color)
        {
            UnityEngine.Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{message}</color>");
        }

        public static void Warning(this string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public static void Warning(this string message, Color color)
        {
            UnityEngine.Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{message}</color>");
        }
    }
}
