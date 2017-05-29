using UnityEngine;
using System.Collections;

public class UserControl : MonoBehaviour {

    public GameObject userEyeCamera;
    public Gun gun;

    public Rigidbody charRigidbody;

    public float walkingPower = 500;
    public float jumpingPower = 800;
    public float torcingPower = 0.8f;

    private bool isAds = false;
    Animator anim;
    

    // Use this for initialization
    void Start () {
        gameObject.name += Random.Range(0, 99999).ToString("D5");

        userEyeCamera = GameObject.Find("UserEyeCamera");

        charRigidbody = gameObject.GetComponent<Rigidbody>();

        gun = gun ?? transform.FindChild("Weapon").gameObject.GetComponent<Gun>();

        anim = transform.FindChild("yukari33").gameObject.GetComponent<Animator>();

	}

    // Update is called once per frame
    void FixedUpdate() {

        float tempWP = walkingPower;
        float tempTP = torcingPower;


        //速度調整
        if (Input.GetKey(KeyCode.LeftShift)) {
            tempWP *= 1.2f;
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            tempWP *= 0.8f;
        }
        if (charRigidbody.velocity.magnitude < 2) {
            tempWP *= 2f;
        }


        //移動の向き
        Vector3 moveAngle = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            moveAngle += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveAngle += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A)) {
            moveAngle += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveAngle += Vector3.right;
        }
        moveAngle = moveAngle.normalized;


        //移動
        if (moveAngle.magnitude > 0) {
            charRigidbody.AddRelativeForce(moveAngle * tempWP, ForceMode.Force);
        }
        //anim.SetFloat(Animator.StringToHash("velocity"), charRigidbody.velocity.magnitude);


        //視点回転
        transform.Rotate(Input.GetAxis("Mouse X") * tempTP * Vector3.up);
        userEyeCamera.transform.parent.Rotate(Input.GetAxis("Mouse Y") * tempTP * Vector3.left);



    }

    void Update() {

        float tempJP = jumpingPower;



        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space)) {
            charRigidbody.AddRelativeForce(Vector3.up * tempJP, ForceMode.Impulse);
        }


        //ADS
        if (Input.GetMouseButtonDown(1)) {
            isAds = !isAds;
            userEyeCamera.GetComponent<Camera>().fieldOfView = isAds ? 50 : 60;
        }


        //攻撃
        if (Input.GetKeyDown(KeyCode.C)) {
            gun.select();
        }
        if (Input.GetMouseButton(0)) {
            if (gun.fire(Input.GetMouseButtonDown(0))) {
                userEyeCamera.transform.Rotate(Vector3.left * 0.1f);
                anim.SetTrigger(Animator.StringToHash("fire"));
                //charRigidbody.AddRelativeForce(Vector3.back * 10, ForceMode.Impulse);
            }
        }




    }
}
