using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlwaysOnScript : MonoBehaviour {
    public TextMeshProUGUI speedrunTimerText;

    int minutes;
    int seconds;
    int milliseconds;

    void Start() {
        if (!GameSettings.timerOn) {
            speedrunTimerText.enabled = false;
        }

        GameObject.Find("AudioManager").GetComponent<AudioManager>().enabled = GameSettings.music;
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            Application.Quit();
        }

        if (GameSettings.timerOn) {
            GameSettings.speedrunTimer += Time.deltaTime;

            minutes = Mathf.FloorToInt(GameSettings.speedrunTimer / 60);
            seconds = Mathf.FloorToInt(GameSettings.speedrunTimer % 60);
            milliseconds = Mathf.FloorToInt((GameSettings.speedrunTimer * 1000) % 1000);

            string timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            speedrunTimerText.text = timerText;
        }
    }
}
