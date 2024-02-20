using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeToast;

public class ToastExample : MonoBehaviour
{
    private float delayShort = Toast.LENGTH_SHORT; //2 seconds
    private float delayLong = Toast.LENGTH_LONG; //3.5 seconds

    void Start()
    {
        StartCoroutine(TestToast());
    }

    IEnumerator TestToast()
    {
        Toast.Show(message: "double " + 123.4, debugConsole: msg => Debug.Log(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: "int " + 10, debugConsole: msg => Debug.LogWarning(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: "bool " + true, debugConsole: msg => Debug.LogError(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: "text only", debugConsole: msg => Debug.Log(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: false, debugConsole: msg => Debug.Log(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show("toast without debug console");
        yield return new WaitForSeconds(delayLong);
        Toast.Show("long toast", duration: Toast.LENGTH_LONG);
        yield return new WaitForSeconds(delayLong);
        Toast.Show("long toast with debug console", Toast.LENGTH_LONG, msg => Debug.Log(msg));
    }
}
