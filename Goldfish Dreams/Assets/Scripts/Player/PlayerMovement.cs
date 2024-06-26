using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 direction;
    private AudioManager audioManager;

    //gun sprites
    private GameObject revolverSprite;
    private GameObject shotgunSprite;
    private GameObject rifleSprite;

    //UI Elements
    //private TextMeshProUGUI ammoCounter;
    private Image[] revolverRoundFull;
    private Image[] revolverRoundEmpty;
    private Image[] shotgunRoundFull;
    private Image[] shotgunRoundEmpty;
    private Image[] rifleRoundFull;
    private Image[] rifleRoundEmpty;

    public GameObject Bullet;
    public GameObject corpse;
    private TextMeshProUGUI deathText;
    public int ammo;
    public float health = 1;
    private float downTime;
    private bool isDown = false;

    public enum WhatGun {
        revolver,
        shotgun,
        rifle,
    }

    public WhatGun gunEquiped;

    public float Health {
        set {
            health = value;
            Instantiate(corpse, gameObject.transform.position, gameObject.transform.rotation);
            deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, 1);
            GameObject.Destroy(gameObject);
        }
        get {
            return health;
        }
    }

    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        //getting and disableling UI Elements
        //ammoCounter = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();

        for (int i = 6; i > 0; i--) { new Color(1, 1, 1, 0);
            GameObject.Find("RevolverRoundFull" + i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            GameObject.Find("RevolverRoundEmpty" + i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        for (int i = 2; i > 0; i--) {
            GameObject.Find("ShotgunShellFull" + i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            GameObject.Find("ShotgunShellEmpty" + i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        for (int i = 16; i > 0; i--) {
            GameObject.Find("RifleRoundFull" + i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            GameObject.Find("RifleRoundEmpty" + i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        deathText = GameObject.Find("DeathText").GetComponent<TextMeshProUGUI>();
        deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, 0);
        rb = GetComponent<Rigidbody2D>();
        revolverSprite = transform.GetChild(0).gameObject;
        shotgunSprite = transform.GetChild(1).gameObject;
        rifleSprite = transform.GetChild(2).gameObject;
        revolverSprite.SetActive(false);
        shotgunSprite.SetActive(false);
        rifleSprite.SetActive(false);


        switch (gunEquiped) {
            case WhatGun.revolver:
                ammo = 6;
                downTime = 0.01f;
                revolverSprite.SetActive(true);
                for (int i = 6; i > 0; i--) {
                    GameObject.Find("RevolverRoundFull" + i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                break;
            case WhatGun.shotgun:
                ammo = 2;
                downTime = 0.3f;
                shotgunSprite.SetActive(true);
                for (int i = 2; i > 0; i--) {
                    GameObject.Find("ShotgunShellFull" + i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                break;
            case WhatGun.rifle:
                ammo = 16;
                downTime = 0.1f;
                rifleSprite.SetActive(true);
                for (int i = 16; i > 0; i--) {
                    GameObject.Find("RifleRoundFull" + i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                break;
        }
        //ammoCounter.text = ammo.ToString();
    }


    void Update() {
        ProcessInputs();
    }

    void FixedUpdate() {
        MovePlayer();
        RotatePlayer();

    }

    void RotatePlayer() {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetMouseButtonDown(0) && ammo > 0 && !isDown && gunEquiped != WhatGun.rifle) {
            for (int i = 0; i < UpgradeHandeler.bulletMultiplyer; i++) {
                StartCoroutine(Shoot());
            }
            ammo--;
            //ammoCounter.text = ammo.ToString();
        }

        if (Input.GetMouseButton(0) && ammo > 0 && !isDown && gunEquiped == WhatGun.rifle) {
            for (int i = 0; i < UpgradeHandeler.bulletMultiplyer; i++) {
                StartCoroutine(Shoot());
            }
            ammo--;
            //ammoCounter.text = ammo.ToString();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            //idk if ill reset the whole game or just the scene
            Time.timeScale = 1;
            GameSettings.deaths++;
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }

    void MovePlayer() {
        rb.velocity = new Vector2(moveDirection.x * UpgradeHandeler.playerMoveSpeed, moveDirection.y * UpgradeHandeler.playerMoveSpeed);
    }

    IEnumerator Shoot() {
        isDown = true;
        switch (gunEquiped) {
            case WhatGun.revolver:
                CinemachineShake.instance.ShakeCamra(5f, .1f);
                Instantiate(Bullet, revolverSprite.transform.position, revolverSprite.transform.rotation);
                audioManager.Play("RevolverFire");
                    GameObject.Find("RevolverRoundFull" + ammo).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    GameObject.Find("RevolverRoundEmpty" + ammo).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case WhatGun.shotgun:
                CinemachineShake.instance.ShakeCamra(7f, .2f);
                for (int i = 5; i > 0; i--) {
                    Instantiate(Bullet, shotgunSprite.transform.position, shotgunSprite.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30)));
                }
                audioManager.Play("ShotgunFire");
                GameObject.Find("ShotgunShellFull" + ammo).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("ShotgunShellEmpty" + ammo).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case WhatGun.rifle:
                CinemachineShake.instance.ShakeCamra(5f, .05f);
                Instantiate(Bullet, rifleSprite.transform.position, rifleSprite.transform.rotation);
                audioManager.Play("RifleFire");
                GameObject.Find("RifleRoundFull" + ammo).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.Find("RifleRoundEmpty" + ammo).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
        }

        yield return new WaitForSeconds(downTime);

        isDown = false;
    }
}
