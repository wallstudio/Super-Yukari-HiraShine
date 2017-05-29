using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCam : MonoBehaviour {

    public bool Around = true;

    public float speed = 1f;
    public float Mag = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Around) {
            transform.Rotate(Vector3.up * speed);
        } else {
            transform.Rotate(Vector3.up * Mathf.Cos(Time.time * speed) * Mag);

        }
    }
}
