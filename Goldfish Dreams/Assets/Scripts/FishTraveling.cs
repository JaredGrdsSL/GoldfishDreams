using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTraveling : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10;
    public GameObject player;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Instantiate(player, collision.transform.position, collision.transform.rotation);

            GameObject.Destroy(collision.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}
