#if UNITY_IOS
using System.Runtime.InteropServices;
#endif
using UnityEngine;
using System;

namespace NativeToast
{
    public static class Toast
    {
#if UNITY_ANDROID
        public const int LENGTH_SHORT = 0;
        public const int LENGTH_LONG = 1;
        private static AndroidJavaObject m_toast = null;
        private static AndroidJavaObject toast
        {
            get
            {
                if (m_toast == null)
                    m_toast = new AndroidJavaObject("com.yashlan.toast.ToastUnity", Context.getContext());

                return m_toast;
            }
        }
#elif UNITY_IOS
    public const float LENGTH_SHORT = 2f;
    public const float LENGTH_LONG = 3.5f;
    [DllImport("__Internal")]
    private static extern void showToast(string message, float duration);
#endif

        public static void Show(
            object message,
#if UNITY_ANDROID
            int duration = Toast.LENGTH_SHORT,
#elif UNITY_IOS
        float duration = Toast.LENGTH_SHORT,
#endif
            Action<object> debugConsole = null
            )
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        toast.Call("showToast", message, duration);
#elif UNITY_IOS && !UNITY_EDITOR
        showToast(message.ToString(), duration);
#else
            debugConsole?.Invoke(message);
#endif
        }
    }
}

