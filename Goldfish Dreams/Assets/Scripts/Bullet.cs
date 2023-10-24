using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed;
    public ParticleSystem hitObsticleParticles;

    private Rigidbody2D rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Wall")) {
            GameObject.Destroy(gameObject);
            GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation), .4f);
        }

        if (collision.CompareTag("Door")) {

            GameObject.Destroy(gameObject, .2f);
            GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation), .4f);
        }

        if (collision.CompareTag("Enemy")) { 
            //enemy takes damage

            PolygonCollider2D poly = collision.GetComponent<PolygonCollider2D>();
            if (poly != null) {
            }
            else { 
            
                GameObject.Destroy (gameObject);
            }

        }

        if (collision.CompareTag("PhysicsObject")) {

            GameObject.Destroy(gameObject, .05f);
            GameObject.Destroy(Instantiate(hitObsticleParticles, gameObject.transform.position, gameObject.transform.rotation), .4f);
        }
    }
}
