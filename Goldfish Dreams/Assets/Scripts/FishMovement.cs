using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    private LineRenderer lineRenderer;

    public GameObject fishTraveling;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update() {
        ProcessInputs();
    }

    void ProcessInputs() {
        if (Input.GetMouseButtonDown(1)) {
            Time.timeScale = .2f;
            Time.fixedDeltaTime = Time.timeScale * 0.01f;
            lineRenderer.enabled = true;
        }

        if (Input.GetMouseButton(1)) {
            DrawLine();
        }

        if (Input.GetMouseButtonUp(1)) {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.01f;
            lineRenderer.enabled = false;


            GameObject fishTravel = Instantiate(fishTraveling, gameObject.transform.position, gameObject.transform.rotation);
            fishTravel.GetComponent<ParticleSystem>().Play();
            GameObject.Destroy(transform.parent.gameObject);
        }
    }

    void DrawLine() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, mousePosition);
    }
}
