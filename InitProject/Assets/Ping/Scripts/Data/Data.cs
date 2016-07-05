using UnityEngine;

public class Data : MonoBehaviour {

    public static Data Instance;
    // Data game here

	void Start () {
        DontDestroyOnLoad(this);
        Instance = this;
    }
}
