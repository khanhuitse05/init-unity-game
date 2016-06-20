using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneFromOther : MonoBehaviour {
    static bool isLoad = false;
	// Use this for initialization
	void Start () {
        if (LoadSceneFromOther.isLoad == false)
        {
            LoadSceneFromOther.isLoad = true;
            SceneManager.LoadScene(0);
        }
	}
}
