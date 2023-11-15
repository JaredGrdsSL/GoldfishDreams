using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float bulletSpeed;
    public ParticleSystem hitObsticleParticles;

    private Rigidbody2D rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }


    //might copy the changes over to the player bullet script
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Wall") || collision.CompareTag("Door") || collision.CompareTag("Player") || collision.CompareTag("PhysicsObject") || collision.CompareTag("Ignore")) {

            if (collision.CompareTag("Wall")) {
                GameObject.Destroy(gameObject);
                GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
            }

            if (collision.CompareTag("Door")) {

                GameObject.Destroy(gameObject, .2f);
                GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
            }

            if (collision.CompareTag("Player")) {
                collision.GetComponent<PlayerMovement>().Health--;
            }

            if (collision.CompareTag("PhysicsObject")) {

                GameObject.Destroy(gameObject, .05f);
                GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
            }
        }
        else {
            GameObject.Destroy(gameObject);
            GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation).gameObject, .4f);
        }

    }
}
