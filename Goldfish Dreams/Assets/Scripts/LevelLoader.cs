using System.Collections;
using System.Collections.Generic;
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

    //might couse problems with multiple levelLoaders
    public int enemiesRemaining;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            if (enemiesNeedToBeKilled) {
                if (enemiesRemaining == 0) {

                    SendPlayer();
                }
            }

            else {
                SendPlayer();
            }
        }

    }

    void SendPlayer() {
        switch (whereTo) {
            case (Locations.nextStage):
                int currentSceneIndex1 = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = (currentSceneIndex1 + 1) % SceneManager.sceneCountInBuildSettings;
                
                SceneManager.LoadScene(nextSceneIndex);
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
