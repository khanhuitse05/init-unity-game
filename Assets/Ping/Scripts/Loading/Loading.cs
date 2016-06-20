using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Finish", 2);
    }
	
	// Update is called once per frame
	void Finish () {
        SceneManager.LoadScene(1);
	}
}
