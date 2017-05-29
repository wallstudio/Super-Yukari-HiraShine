using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour {

    public bool Limited = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick() {

        if(!Limited)
        SceneManager.LoadScene("Main");
        return;

        if (System.DateTime.Now.Ticks < new System.DateTime(2016,10,7).Ticks) {
            Debug.Log(System.DateTime.Now.Ticks);
            Debug.Log(new System.DateTime(2016, 10, 7).Ticks);

            SceneManager.LoadScene("Main");
        }
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene("Main");
        }

    }
}
