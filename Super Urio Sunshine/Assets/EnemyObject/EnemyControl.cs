using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour {

    public float walkingPower = 500;
    public GameObject ui;
    private Text uiText;
    public float _hitPoint = 1000;
    private float hitPoint {
        get {
            return _hitPoint;
        }
        set {
            _hitPoint = value;
            int hp = Mathf.FloorToInt(_hitPoint);
            uiText.text = "HP <b>" + (hp < 0 ? 0 : hp) + "</b> / 1000";
        }
    }

    private Rigidbody enemyRigidbody;
    GameObject player;
    ParticleSystem blooding;
    Animator zomAni;
    public bool isDeath = false;
    AudioSource se;


	// Use this for initialization
	void Start () {
        uiText = ui.transform.FindChild("HitPoint").GetComponent<Text>();
        enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        blooding = transform.FindChild("Blooding").GetComponent<ParticleSystem>();
        zomAni = transform.FindChild("ZombieRig").GetComponent<Animator>();
        se = gameObject. GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isDeath) { return; }

        //zomAni.SetFloat(Animator.StringToHash("velocity"), enemyRigidbody.velocity.magnitude);

        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 myVerocity = enemyRigidbody.velocity;
        Vector3 distanceMeToPlayer = player.transform.position - gameObject.transform.position;
        if (myVerocity.magnitude < 0.011) {
            enemyRigidbody.AddForce(distanceMeToPlayer.normalized* walkingPower,ForceMode.VelocityChange);
        }
        transform.LookAt(player.transform);
        transform.eulerAngles = Vector3.Scale(transform.rotation.eulerAngles, new Vector3(0, 1, 0));
        transform.Rotate(new Vector3(0,180, 0));


        ui.transform.LookAt(player.transform);
        ui.transform.eulerAngles = Vector3.Scale(ui.transform.rotation.eulerAngles, new Vector3(0, 1, 0));
        ui.transform.Rotate(new Vector3(0, 180, 0));


    }

    void OnCollisionEnter(Collision otherCollision) {
        if (isDeath) { return; }
        if(otherCollision.rigidbody == null) { return; }
        if (otherCollision.rigidbody.velocity.magnitude > 5) {
            hitPoint -= otherCollision.rigidbody.velocity.magnitude*20 * otherCollision.rigidbody.mass;
            zomAni.SetTrigger(Animator.StringToHash("hit"));
            se.PlayOneShot(se.clip, 1f);
            blooding.Play();
            if (hitPoint < 0) {
                isDeath = true;
                ui.SetActive(false);
                enemyRigidbody.isKinematic = true;
                transform.FindChild("Collider").gameObject.SetActive(false);
                zomAni.SetTrigger(Animator.StringToHash("death"));
                StartCoroutine( delayDestroy(gameObject));
            }
        }
    }

    IEnumerator delayDestroy(GameObject gameObject) {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }
}
