using UnityEngine;
using System.Collections;
using System;

public class Gun : MonoBehaviour {

    public float muzzleVelocity = 10;

    public GameObject bulletPrefab;
    public GameObject cartPrefab;

    public GameObject muzzleFlash;
    private ParticleSystem flash;

    private AudioSource sound;
    private LineRenderer layzer;

    bool cooling = false;
    bool isFullauto = false;

    // Use this for initialization
    void Start () {
        muzzleFlash = gameObject.transform.FindChild("MuzzleFlash").gameObject;
        flash = muzzleFlash.GetComponent<ParticleSystem>();
        sound = gameObject.GetComponent<AudioSource>();
        layzer = gameObject.transform.FindChild("Layzer").GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(layzer.transform.position, layzer.transform.TransformDirection(Vector3.forward),out hit,100f)) {
            layzer.SetPosition(1, layzer.transform.InverseTransformPoint( hit.point));
        } else {
            layzer.SetPosition(1, Vector3.forward*100);
        }
	}

    public bool fire(bool down) {
        if ((!down&&!isFullauto)||cooling == true) { return false; }
        cooling = true;
        
        //弾丸
        GameObject newBullet = Instantiate(bulletPrefab);
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        newBullet.transform.parent = gameObject.transform;

        newBullet.transform.localPosition = newBullet.transform.position;
        newBullet.transform.localRotation = newBullet.transform.rotation;

        bulletRigidbody.AddRelativeForce(Vector3.forward * muzzleVelocity, ForceMode.Impulse);
        newBullet.transform.parent = null;

        //薬莢
        GameObject newCart = Instantiate(cartPrefab);
        Rigidbody cartRigidbody = newCart.GetComponent<Rigidbody>();
        newCart.transform.parent = gameObject.transform;

        newCart.transform.localPosition = newCart.transform.position;
        newCart.transform.localRotation = newCart.transform.rotation;

        cartRigidbody.AddRelativeForce(Vector3.right*0.1f+Vector3.up*0.2f, ForceMode.Impulse);
        cartRigidbody.AddRelativeTorque(Vector3.up * UnityEngine.Random.Range(0,100), ForceMode.Impulse); 
        newCart.transform.parent = null;

        //マズルフラッシュ
        flash.Play();
        sound.PlayOneShot(sound.clip,0.3f);
        

        StartCoroutine("destroy", newCart);
        StartCoroutine("destroy", newBullet);

        StartCoroutine(cool());

        return true;
    }

    private IEnumerator destroy(GameObject destroyObj) {
        yield return new WaitForSeconds(5f);
        Destroy(destroyObj);
    }

    public void select() {
        isFullauto = !isFullauto;

    }

    IEnumerator cool() {
        yield return new WaitForSeconds(60/1000f);
        cooling = false;
    }
}
