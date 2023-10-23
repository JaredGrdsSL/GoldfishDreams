using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 direction;

    //gun sprites
    private GameObject revolverSprite;


    public GameObject Bullet;

    public float moveSpeed;

    public int ammo;

    public enum WhatGun { 
        revolver,
        shotgun,
        rifle,
    }

    public WhatGun gunEquiped;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        revolverSprite = GameObject.Find("Revolver");

        //change sprite depending on what gun is equiped
        switch (gunEquiped) { 
            case WhatGun.revolver:
                ammo = 6;
                break;
            case WhatGun.shotgun:
                ammo = 2;
                break;
            case WhatGun.rifle:
                ammo = 16;
                break;
        }
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

        if (Input.GetMouseButtonDown(0) && ammo > 0) {
            Shoot();
            ammo--;
        }

        //debug
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            UpgradeHandeler.piercingFish = true;
        }
    }

    void MovePlayer() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void Shoot() {
        switch (gunEquiped) {
            case WhatGun.revolver:
                Instantiate(Bullet, revolverSprite.transform.position, revolverSprite.transform.rotation);
                break;
            case WhatGun.shotgun:
                
                break;
            case WhatGun.rifle:
                
                break;
        }
    }
}
