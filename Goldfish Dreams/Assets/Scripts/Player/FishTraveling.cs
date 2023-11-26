using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class FishTraveling : MonoBehaviour {
    private Rigidbody2D rb;
    private ParticleSystem partic;
    private GameObject particle;
    private TextMeshProUGUI deathText;
    public ParticleSystem fishEnterParticles;
    public Sprite coolFishSprite;
    public float speed;
    public GameObject player;
    public GameObject particles;
    public GameObject corpse;
    private CinemachineVirtualCamera virtualCamera;
    private AudioManager audioManager;
    void Start() {
        deathText = GameObject.Find("DeathText").GetComponent<TextMeshProUGUI>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        particle = Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
        ObjectFollower particleFollower = particle.GetComponent<ObjectFollower>();
        particleFollower.objectToFollow = this.gameObject;
        partic = particle.GetComponent<ParticleSystem>();
        partic.Play();
        virtualCamera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (UpgradeHandeler.coolFIsh) {
            gameObject.GetComponent<SpriteRenderer>().sprite = coolFishSprite;
        }
        
        audioManager.Play("Wind");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            GameObject newPlayer = Instantiate(player, collision.transform.position, collision.transform.rotation);
            ParticleSystem newParticle = Instantiate(fishEnterParticles, collision.transform.position, collision.transform.rotation);
            audioManager.Play("WaterSplash");
            virtualCamera.Follow = newPlayer.transform;

            PlayerMovement playerMovement = newPlayer.GetComponent<PlayerMovement>();
            switch (collision.GetComponent<EnemyController>().gunEquiped.ToString()) {
                case "revolver":
                    audioManager.Play("RevolverPickup");
                    playerMovement.gunEquiped = PlayerMovement.WhatGun.revolver;
                    break;
                case "shotgun":
                    audioManager.Play("ShotgunPickup");
                    playerMovement.gunEquiped = PlayerMovement.WhatGun.shotgun;
                    break;
                case "rifle":
                    audioManager.Play("RiflePickup");
                    playerMovement.gunEquiped = PlayerMovement.WhatGun.rifle;
                    break;
            }

            partic.Stop();
            audioManager.Stop("Wind");

            GameObject.Destroy(newParticle.gameObject, .5f);
            GameObject.Destroy(particle, .5f);
            GameObject.Destroy(collision.gameObject);
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().enemiesRemaining--;
            if (!UpgradeHandeler.piercingFish) {
                GameObject.Destroy(gameObject);
            }
        }
        if ((collision.CompareTag("Wall") || collision.CompareTag("Door") || collision.CompareTag("Player") || collision.CompareTag("PhysicsObject") || collision.CompareTag("Ignore")) && !UpgradeHandeler.piercingFish) {
            if (collision.CompareTag("Wall") && !UpgradeHandeler.piercingFish) {
                GameObject.Destroy(gameObject);
                partic.Stop();
                GameObject.Destroy(Instantiate(fishEnterParticles, gameObject.transform.position, gameObject.transform.rotation), .5f);
                EndGame();
            }

            if (collision.CompareTag("Door") && !UpgradeHandeler.piercingFish && !UpgradeHandeler.piercingFishDoors) {
                GameObject.Destroy(gameObject);
                partic.Stop();
                GameObject.Destroy(Instantiate(fishEnterParticles, gameObject.transform.position, gameObject.transform.rotation), .5f);
                EndGame();
            }

            if (collision.CompareTag("PhysicsObject")) {
                GameObject.Destroy(gameObject);
                partic.Stop();
                GameObject.Destroy(Instantiate(fishEnterParticles, gameObject.transform.position, gameObject.transform.rotation), .5f);
                EndGame();
            }
        }
    }

    void EndGame() {
        Instantiate(corpse, gameObject.transform.position, gameObject.transform.rotation);
        deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, 1);
    }
}
