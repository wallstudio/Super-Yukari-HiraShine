using UnityEngine;
using System.Collections;

public class GameMasterControl : MonoBehaviour {

    private bool isPause;
    public GameObject enemyPrefab;
    int enemyCounter = 0;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    
        StartCoroutine(spown());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPause = !isPause;

            Cursor.visible = !isPause;
            Cursor.lockState = isPause ? CursorLockMode.Locked : CursorLockMode.None;
        }

	}

    IEnumerator spown() {
        while (true) {
            enemyCounter++;
            yield return new WaitForSeconds(1f);
            if (enemyCounter < 100) { spownEnemy(); }
        }
    }

    void spownEnemy() {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position=new Vector3(Random.Range(-20f, 20f), 5f, Random.Range(-20f, 20f));
    }
}
