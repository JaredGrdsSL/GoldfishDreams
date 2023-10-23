using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 direction;

    public float moveSpeed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
        processInputs();
        Debug.Log(moveDirection);
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

    void processInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void MovePlayer() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
