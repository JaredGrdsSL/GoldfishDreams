using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private GameObject usingGun;
    private GameObject revolver;
    private GameObject shotgun;
    private GameObject rifle;
    private Vector2 direction;

    public GameObject player;
    public bool inRange = false;
    LayerMask raycastLayerMask;
    private bool seenPlayer = false;

    //public GameObject player;

    public enum WhatGun {
        revolver,
        shotgun,
        rifle,
    }

    public WhatGun gunEquiped;

    void Start() {
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
                break;
            case WhatGun.shotgun:
                shotgun.SetActive(true);
                usingGun = shotgun;
                break;
            case WhatGun.rifle:
                rifle.SetActive(true);
                usingGun = rifle;
                break;
        }
    }

    private void FixedUpdate() {
        if (inRange && player != null) {
            Debug.DrawRay(gameObject.transform.position, player.transform.position - transform.position);
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, player.transform.position - transform.position, 100, raycastLayerMask);
            Debug.Log(hit.collider);
            if (hit.collider != null) {
                if (hit.collider.CompareTag("Player")) {
                    seenPlayer = true;

                    direction = (player.transform.position) - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
                }
            }
        }
    }
}
