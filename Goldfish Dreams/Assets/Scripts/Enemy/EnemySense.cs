using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : MonoBehaviour
{
    private EnemyController enemyController;

    private void Start() {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            enemyController.player = collision.gameObject;
            enemyController.inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            enemyController.player = null;
            enemyController.inRange = false;
        }
    }

}
