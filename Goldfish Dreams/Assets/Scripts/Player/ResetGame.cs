using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audioManager.Stop("Wind");
        audioManager.Play("Death");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            //idk if ill reset the whole game or just the scene
            Time.timeScale = 1;
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}
