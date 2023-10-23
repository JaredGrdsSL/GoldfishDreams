using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FishTraveling : MonoBehaviour
{
    private Rigidbody2D rb;
    private ParticleSystem partic;
    private GameObject particle;
    public ParticleSystem fishEnterParticles;
    public float speed;
    public GameObject player;
    public GameObject particles;
    private CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        particle = Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
        ObjectFollower particleFollower = particle.GetComponent<ObjectFollower>();
        particleFollower.objectToFollow = this.gameObject;
        partic = particle.GetComponent<ParticleSystem>();
        partic.Play();

        virtualCamera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            GameObject newPlayer = Instantiate(player, collision.transform.position, collision.transform.rotation);
            ParticleSystem newParticle = Instantiate(fishEnterParticles, collision.transform.position, collision.transform.rotation);
            virtualCamera.Follow = newPlayer.transform;


            partic.Stop();

            GameObject.Destroy(newParticle.gameObject, .5f);
            GameObject.Destroy(particle, .5f);
            GameObject.Destroy(collision.gameObject);
            if (!UpgradeHandeler.piercingFish) { 
                GameObject.Destroy(gameObject);
            }
        }
    }
}
