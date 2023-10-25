using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private GameObject usingGun;
    private GameObject revolver;
    private GameObject shotgun;
    private GameObject rifle;
    private Vector2 direction;
    private Animator animator;
    private AudioManager audioManager;
    public float health;
    public GameObject corpse;

    //player detection
    public bool inRange = false;
    LayerMask raycastLayerMask;
    public GameObject player;
    private bool angered = false;
    private bool seesPlayer = false;

    //shooting
    private float reloadSpeed;
    private bool reloading = false;
    public GameObject enemyBullet;



    public enum WhatGun {
        revolver,
        shotgun,
        rifle,
    }

    public WhatGun gunEquiped;

    public float Health {
        set {
            health = value;
            if (health <= 0) {
                audioManager.Play("Hurt2");
                Instantiate(corpse, gameObject.transform.position, gameObject.transform.rotation);
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().enemiesRemaining--;
                GameObject.Destroy(gameObject);
            }
            else {
                animator.SetTrigger("Damaged");
                audioManager.Play("Hurt1");
            }
        }
        get {
            return health;
        }
    }

    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        raycastLayerMask = ~(1 << LayerMask.NameToLayer("Enemy"));
        revolver = transform.GetChild(0).gameObject;
        shotgun = transform.GetChild(1).gameObject;
        rifle = transform.GetChild(2).gameObject;
        revolver.SetActive(false);
        shotgun.SetActive(false);
        rifle.SetActive(false);

        switch (gunEquiped) {
            case WhatGun.revolver:
                revolver.SetActive(true);
                usingGun = revolver;
                reloadSpeed = .7f;
                break;
            case WhatGun.shotgun:
                shotgun.SetActive(true);
                usingGun = shotgun;
                reloadSpeed = 1.5f;
                break;
            case WhatGun.rifle:
                rifle.SetActive(true);
                usingGun = rifle;
                reloadSpeed = .25f;
                break;
        }
    }

    private void Update() {
        if (angered && seesPlayer && !reloading) {
            switch (gunEquiped.ToString()) {
                case "revolver":
                    audioManager.Play("RevolverFire");
                    break;
                case "shotgun":
                    audioManager.Play("ShotgunFire");
                    break;
                case "rifle":
                    audioManager.Play("RifleFire");
                    break;
            }
            StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate() {
        if (inRange && player != null) {
            Debug.DrawRay(gameObject.transform.position, player.transform.position - transform.position);
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, player.transform.position - transform.position, 100, raycastLayerMask);
            if (hit.collider != null) {
                if (hit.collider.CompareTag("Player")) {
                    angered = true;
                    seesPlayer = true; ;

                    direction = (player.transform.position) - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
                }
                else {
                    seesPlayer = false;
                }
            }
        }
    }

    IEnumerator Shoot() {
        if (gunEquiped != WhatGun.shotgun) {
            Instantiate(enemyBullet, usingGun.transform.position + transform.forward * 5, gameObject.transform.rotation);
        }
        else {
            for (int i = 3; i > 0; i--) {
                GameObject newBullet = Instantiate(enemyBullet, usingGun.transform.position, gameObject.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30)));
                EnemyBullet enemyBulletReference = newBullet.GetComponent<EnemyBullet>();
                enemyBulletReference.GetComponent<EnemyBullet>().bulletSpeed = 12;
                enemyBulletReference.shooter = gameObject;
            }
        }
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        reloading = false;
    }
}
