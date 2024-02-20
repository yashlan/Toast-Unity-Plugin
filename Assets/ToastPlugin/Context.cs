using UnityEngine;

namespace NativeToast
{
    public static class Context
    {
#if UNITY_ANDROID
        private static AndroidJavaObject m_context = null;
        private static AndroidJavaObject context
        {
            get
            {
                if (m_context == null)
                {
                    using (AndroidJavaObject unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        m_context = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
                    }
                }

                return m_context;
            }
        }

        public static AndroidJavaObject getContext()
        {
            return context;
        }
#endif
    }
}
