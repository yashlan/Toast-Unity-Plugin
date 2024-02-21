using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NativeToast;
using UnityEngine.SceneManagement;

public class ToastExample : MonoBehaviour
{
    [SerializeField] Button restartBtn;
#if UNITY_ANDROID
    private float delayShort = 2f;
    private float delayLong = 3.5f;
#elif UNITY_IOS
    private float delayShort = 4f;
    private float delayLong = 7f;
#endif

    void Start()
    {
        restartBtn.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            Toast.Cancel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        StartCoroutine(TestToast());
    }

    IEnumerator TestToast()
    {
#if UNITY_ANDROID
        Toast.Show(message: "Unity toast message on Android device", debugConsole: msg => Debug.Log(msg));
#elif UNITY_IOS
        Toast.Show(message: "Unity toast message on iOS device", debugConsole: msg => Debug.Log(msg));
#endif
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: "int " + 10, debugConsole: msg => Debug.LogWarning(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: "bool " + true, debugConsole: msg => Debug.LogError(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: "asdasdas dasd asasdasdsad ad aadasd asd adada asda asdasdasdasdasdasd asda ad asdasdsadasd asdasdas asdas dasda sdas dasd as da da sdasdasda asd asd", debugConsole: msg => Debug.Log(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show(message: false, debugConsole: msg => Debug.Log(msg));
        yield return new WaitForSeconds(delayShort);
        Toast.Show("toast without debug console");
        yield return new WaitForSeconds(delayShort);
        Toast.Show("long toast", duration: Toast.LENGTH_LONG);
        yield return new WaitForSeconds(delayLong);
        Toast.Show("long toast with debug console", Toast.LENGTH_LONG, msg => Debug.Log(msg));
    }
}
