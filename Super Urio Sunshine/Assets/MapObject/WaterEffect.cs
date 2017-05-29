using UnityEngine;
using System.Collections;

public class WaterEffect : MonoBehaviour {

    public float updown = 0;
    Material mat;

	// Use this for initialization
	void Start () {
        mat = gameObject.GetComponent<Renderer>().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset += new Vector2(Mathf.Sin(Time.time * 2 - 10) * 0.0003f,0.001f);
        transform.Translate(Vector3.up * Mathf.Cos(Time.time)*updown*0.005f,Space.World);
    }
}
