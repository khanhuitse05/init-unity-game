using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public static ScreenShot Instance { get; private set; }
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
		Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string prefix = "Ping";
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(TackScreenshot());
        }
    }
    private IEnumerator TackScreenshot()
    {
        int index = PlayerPrefs.GetInt("keyindex", 0);
        string _name = prefix + index + ".png";
        PlayerPrefs.SetInt("keyindex", index + 1);
        yield return new WaitForEndOfFrame();
        // take screen shot
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();
        byte[] dataToSave = screenTexture.EncodeToPNG();
        // save
        string destination = Path.Combine(Application.persistentDataPath, _name);
        File.WriteAllBytes(destination, dataToSave);
        Debug.Log("<color=red> Save Image: " + Application.persistentDataPath + "/" + _name + "</color>");
    }
#endif
}
