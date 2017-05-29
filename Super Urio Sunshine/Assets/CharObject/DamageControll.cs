using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System;
using System.Text.RegularExpressions;

public class DamageControll : MonoBehaviour {

    public GameObject cam;
    ColorCorrectionCurves camV;
    BlurOptimized camB;

    public AudioClip damagedSe; 
    private AudioSource speaker;
    Animator anim;
    

    private bool isDeath;
    public float hitPoint = 1000;

    // Use this for initialization
    void Start () {
        camV = cam.GetComponent<ColorCorrectionCurves>();
        camB = cam.GetComponent<BlurOptimized>();

        speaker = gameObject.AddComponent<AudioSource>();

        anim = transform.FindChild("yukari33").gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision otherCollision) {
        if (isDeath) { return; }
        if (otherCollision.rigidbody == null) { return; }
        if(new Regex("EnemyChar").IsMatch(otherCollision.gameObject.name)&&!otherCollision.gameObject.GetComponent<EnemyControl>().isDeath) {
            hitPoint -= 100;
            speaker.PlayOneShot(damagedSe, 1f);
            StartCoroutine(delayCam(camV, camB));
            camV.saturation = 0.5f * hitPoint / 1000;
            if (hitPoint < 0) {
                isDeath = true;
                anim.SetTrigger(Animator.StringToHash("dead"));
                anim.transform.parent = null;
                GameObject w = GameObject.Find("Weapon");
                w.transform.parent = null;
                w.GetComponent<Rigidbody>().isKinematic = false;
                w.GetComponent<BoxCollider>().enabled = true;

            }
        }
        if (otherCollision.rigidbody.velocity.magnitude > 5) {
            hitPoint -= otherCollision.rigidbody.velocity.magnitude * 3 * otherCollision.rigidbody.mass;
            speaker.PlayOneShot(damagedSe, 1f);
            StartCoroutine(delayCam(camV, camB));
            camV.saturation = 0.5f * hitPoint / 1000;
            if (hitPoint < 0) {
                isDeath = true;
                anim.SetTrigger(Animator.StringToHash("dead"));
                GameObject.Find("Weapon").transform.parent = null;
                anim.transform.parent = null;
            }
        }
    }

    IEnumerator delayCam(ColorCorrectionCurves v,BlurOptimized b) {
        b.enabled = true;
        b.blurSize += 2;
        for(int i = 0; i < 21; i++) {
            yield return new WaitForSeconds(0.1f);
            b.blurSize -= 0.1f;
            if (b.blurSize < 0) {
                b.enabled = false;
                break;
            }
        }
    }

}
