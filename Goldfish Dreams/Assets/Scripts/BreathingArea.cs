using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingArea : MonoBehaviour {
    private AudioManager audioManager;
    private AudioSource[] audioSource;
    private void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioSource = GameObject.Find("AudioManager").GetComponents<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log(audioSource.Length);
            for (int i = audioSource.Length - 1; i > -1; i--) {
                if (i != 12) {
                    audioSource[i].volume = 0f;
                }
                else {
                    audioManager.Play("Breathing");
                    audioSource[i].volume = 1f;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("BBBB");
            for (int i = audioSource.Length - 1; i > -1; i--) {
                if (i != 12) {
                    audioSource[i].volume = .5f;
                }
                else {
                    audioManager.Stop("Breathing");
                    audioSource[i].volume = 0f;
                }
                
            }
        }
    }
}