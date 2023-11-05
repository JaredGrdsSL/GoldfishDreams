using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public enum Locations {
        nextStage,
        previousStage,
        stage1,
        stage2,
        stage3,
        stage4,
        stage5,
        creddits,
    }

    public Locations whereTo;
    public bool enemiesNeedToBeKilled;
    private TextMeshProUGUI levelLoaderText;
    public GameObject upgradeUIElement;

    public int enemiesRemaining;

    private void Start() {
        upgradeUIElement = GameObject.Find("Upgrades");
        upgradeUIElement.SetActive(false);
        levelLoaderText = GameObject.Find("LevelLoadingText").GetComponent<TextMeshProUGUI>();
        levelLoaderText.color = new Color(1,1,1,0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            if (enemiesNeedToBeKilled) {
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
        if (levelLoaderText.color == new Color(1,1,1,1)) {
            levelLoaderText.color = new Color(1, 1, 1, 0);
        }
    }

    void SendPlayer(Collider2D collision) {
        switch (whereTo) {
            case (Locations.nextStage):
                Time.timeScale = 1;
                GameObject.Destroy(collision.gameObject);
                upgradeUIElement.SetActive(true);
                break;
            case (Locations.previousStage):
                int currentSceneIndex2 = SceneManager.GetActiveScene().buildIndex;
                int previousSceneIndex = (currentSceneIndex2 - 1) % SceneManager.sceneCountInBuildSettings;
                SceneManager.LoadScene(previousSceneIndex);
                break;
            case (Locations.stage1):
                SceneManager.LoadScene(1);
                break;
            case (Locations.stage2):
                SceneManager.LoadScene(2);
                break;
            case (Locations.stage3):
                SceneManager.LoadScene(3);
                break;
            case (Locations.stage4):
                SceneManager.LoadScene(4);
                break;
            case (Locations.stage5):
                SceneManager.LoadScene(5);
                break;
            case (Locations.creddits):
                SceneManager.LoadScene(6);
                break;
        }
    }
}
