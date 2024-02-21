# Toast-Unity-Plugin

This plugin enables you to display toast messages on mobile platforms (iOS/Android) within your Unity game.<br>

Platform Support:
- iOS recommended version 12.0 or above
- Android recommended version 6.0 Marshmallow(API 23) or above

# Preview

> Preview on Android Device
<img src="https://github.com/yashlan/Toast-Unity-Plugin/blob/main/ss/ss_android.gif" /> 

<br>

> Preview on iOS Device
<img src="https://github.com/yashlan/Toast-Unity-Plugin/blob/main/ss/ss_ios.gif" />

# Usage

## Params
```csharp
//Android
public static void Show(object message, int duration = Toast.LENGTH_SHORT, Action<object> debugConsole = null)

//iOS
public static void Show(object message, float duration = Toast.LENGTH_SHORT, Action<object> debugConsole = null)
```

## Displaying Toast
```csharp
using NativeToast; //Import namescape

public class Test : MonoBehaviour
{
    void Start()
    {
         //show message only
         Toast.Show("Unity toast message");

         //long toast
         Toast.Show("Unity toast message", Toast.LENGTH_LONG);

         //show message and debug log
         Toast.Show("Unity toast message", msg => Debug.LogWarning(msg));

         //use all params
         Toast.Show(message: "int " + 10, duration: Toast.LENGTH_LONG, debugConsole: msg => Debug.Log(msg));
    }
}
```

## Cancel Toast
```csharp
Toast.Cancel();
```



