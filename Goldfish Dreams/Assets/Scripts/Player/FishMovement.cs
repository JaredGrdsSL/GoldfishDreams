using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;


public class FishMovement : MonoBehaviour {

    private LineRenderer lineRenderer;
    private CinemachineVirtualCamera virtualCamera;
    private PlayerMovement playerMovement;

    public GameObject fishTraveling;
    public GameObject enemyDead;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        virtualCamera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    void Update() {
        ProcessInputs();
    }

    void ProcessInputs() {
        if (playerMovement.ammo == 0) {
            if (Input.GetMouseButtonDown(1)) {
                Time.timeScale = .2f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                lineRenderer.enabled = true;
            }

            if (Input.GetMouseButton(1)) {
                DrawLine();
            }

            if (Input.GetMouseButtonUp(1)) {
                Instantiate(enemyDead, gameObject.transform.position, gameObject.transform.rotation);
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                lineRenderer.enabled = false;

                GameObject fishTravel = Instantiate(fishTraveling, gameObject.transform.position + transform.forward * 2, gameObject.transform.rotation);
                if (UpgradeHandeler.piercingFish) {
                    fishTravel.GetComponent<CircleCollider2D>().enabled = false;
                }
                virtualCamera.Follow = fishTravel.transform;
                GameObject.Destroy(transform.parent.gameObject);
            }
        }
    }

    void DrawLine() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, mousePosition);
    }
}
