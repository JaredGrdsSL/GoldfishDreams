using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public bool enemiesNeedToBeKilled;
    private TextMeshProUGUI levelLoaderText;
    private GameObject upgradeUIElement;

    public int enemiesRemaining;

    private void Start() {
        upgradeUIElement = GameObject.Find("Upgrades");
        upgradeUIElement.SetActive(false);
        levelLoaderText = GameObject.Find("LevelLoadingText").GetComponent<TextMeshProUGUI>();
        levelLoaderText.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            if (enemiesNeedToBeKilled) {
                if (UpgradeHandeler.piercingFish) {
                    CheckPiercingFish();
                }

                if (enemiesRemaining <= 0) {
                    SendPlayer(collision);
                }

                else {
                    levelLoaderText.text = enemiesRemaining + " Soulless Oppressors Left TO Kill";
                    levelLoaderText.color = new Color(1, 1, 1, 1);
                }
            }

            else {
                SendPlayer(collision);
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (levelLoaderText.color == new Color(1, 1, 1, 1)) {
            levelLoaderText.color = new Color(1, 1, 1, 0);
        }
    }

    private void CheckPiercingFish() {
        LevelLoader levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        PlayerMovement[] playersMovements = FindObjectsOfType<PlayerMovement>();
        for (int i = playersMovements.Length -1;  i > 0; i--) {
            playersMovements[i].Health = 0;
        }
    }

    void SendPlayer(Collider2D collision) {
        Time.timeScale = 1;
        GameObject.Destroy(collision.gameObject);
        upgradeUIElement.SetActive(true);
    }
}
