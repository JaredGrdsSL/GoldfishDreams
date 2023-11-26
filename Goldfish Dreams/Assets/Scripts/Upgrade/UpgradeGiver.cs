using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeGiver : MonoBehaviour {
    public enum UpGrades {
        piercingFish,
        doorPiercingFish,
        bouncingBullets,
        piercingBullets,
        bullterMultiplyer,
        coolFish,
        bouncingFish,
        playerMoveSpeedUp,
    }

    public UpGrades upgrade;

    private GameObject upgradeUIPannel;

    public void Clicked() {
        switch (upgrade) {
            case (UpGrades.piercingFish):
                UpgradeHandeler.piercingFish = true;
                break;
            case (UpGrades.doorPiercingFish):
                UpgradeHandeler.piercingFishDoors = true;
                break;
            case (UpGrades.bouncingBullets):
                UpgradeHandeler.bulletBouncePower += 0.5f;
                break;
            case (UpGrades.piercingBullets):
                UpgradeHandeler.piercingBullets = true;
                break;
            case (UpGrades.bullterMultiplyer):
                UpgradeHandeler.bulletMultiplyer *= 2;
                break;
            case (UpGrades.coolFish):
                UpgradeHandeler.coolFIsh = true;
                break;
            case (UpGrades.bouncingFish):
                UpgradeHandeler.fishBouncePower += 2f;
                UpgradeHandeler.bouncingFish = true;
                break;
            case (UpGrades.playerMoveSpeedUp):
                UpgradeHandeler.playerMoveSpeed *= 1.2f;
                break;
        }

        int currentSceneIndex1 = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex1 + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
