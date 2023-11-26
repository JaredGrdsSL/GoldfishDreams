using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed;
    public ParticleSystem hitObsticleParticles;

    private Rigidbody2D rb;
    private bool canCollideWithPlayer = false;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        StartCoroutine(PlayerCountDown());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<EnemyController>().Health--;
            if (!collision.GetComponent<EnemyController>().inRange) {
                PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
                if (players.Length > 0) {
                    collision.GetComponent<EnemyController>().player = players[players.Length - 1].gameObject;
                }
            }
            if (!UpgradeHandeler.piercingBullets) {
                GameObject.Destroy(gameObject);
            }

        }

        if (collision.CompareTag("Fish")) {
            GetCollider(gameObject).enabled = false;
        }

        if ((collision.CompareTag("Wall") || collision.CompareTag("Door") || collision.CompareTag("Player") || collision.CompareTag("PhysicsObject") || collision.CompareTag("Ignore")) && UpgradeHandeler.bulletBouncePower == 0) {
            if (collision.CompareTag("Wall")) {
                GameObject.Destroy(gameObject);
                GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
            }

            if (collision.CompareTag("Door")) {

                GameObject.Destroy(gameObject, .2f);
                GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
            }

            if (collision.CompareTag("PhysicsObject")) {

                GameObject.Destroy(gameObject, .05f);
                GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if ((collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Door") || collision.collider.CompareTag("PhysicsObject") || (collision.collider.CompareTag("Player") && canCollideWithPlayer)) && UpgradeHandeler.bulletBouncePower > 0) {
            ContactPoint2D point = collision.contacts[0];
            Vector2 newDir;
            Vector2 curDire = this.transform.TransformDirection(Vector2.right);

            newDir = Vector2.Reflect(curDire, point.normal);
            transform.rotation = Quaternion.FromToRotation(Vector2.right, newDir);

            rb.velocity = transform.right * (bulletSpeed * UpgradeHandeler.bulletBouncePower);
        }
    }

    IEnumerator PlayerCountDown() {
        yield return new WaitForSeconds(.5f);
        canCollideWithPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Fish")) {
            GetCollider(gameObject).enabled = true;
        }
    }

    public PolygonCollider2D GetCollider(GameObject objectWithCollider) {
        PolygonCollider2D collider = objectWithCollider.GetComponent<PolygonCollider2D>();
        return collider;
    }
}
