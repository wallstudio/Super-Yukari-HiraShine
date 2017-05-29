using UnityEngine;
using System.Collections;

public class BulletControll : MonoBehaviour {

    public GameObject markPrefab;
    public GameObject bloodPrefab;
    Rigidbody rigi;

	// Use this for initialization
	void Start () {
        rigi = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision otherCollision) {
        if (otherCollision.transform.name == "Map" && rigi.velocity.magnitude > 4) {
            GameObject mark = Instantiate(markPrefab);
            mark.transform.position = transform.position;
            mark.transform.rotation = transform.rotation;
            mark.transform.Rotate(new Vector3(-90, 0, 0));
            StartCoroutine(delayDestroy(mark));
        } else if (otherCollision.transform.tag == "Lifer" && rigi.velocity.magnitude > 4) {
            GameObject mark = Instantiate(bloodPrefab);
            mark.transform.position = transform.position;
            mark.transform.rotation = transform.rotation;
            mark.transform.Rotate(new Vector3(-90, 0, 0));
            mark.GetComponent<ParticleSystem>().Play();
            StartCoroutine(delayDestroy(mark));
        }
    }

    IEnumerator delayDestroy(GameObject mark) {
        yield return new WaitForSeconds(4f);
        Destroy(mark);
    }
}
