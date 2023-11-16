using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSorter : MonoBehaviour {

    public GameObject FinishedScreen;
    private AudioManager audioManager;
    private GameObject finalTimeText;

    bool isReady = false;
    public GameObject upgradeSlot1;
    public GameObject upgradeSlot2;
    public GameObject upgradeSlot3;

    //Upgrades
    public GameObject piercingFish;
    public GameObject piercingFishDoors;
    public GameObject bouncingBullets;
    public GameObject piercingBullets;
    public GameObject bulletMultiplier;

    private void Awake() {
        finalTimeText = GameObject.Find("FinalTimeText");
        finalTimeText.SetActive(false);
    }

    private void OnEnable() {
        

        if (isReady && (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings != 0) {
            for (int i = 3; i > 0; i--) {
                int e = Random.Range(1, 5 + 1);
                switch (e) {
                    case 1:
                        if (UpgradeHandeler.piercingFish) {
                            i++;
                            break;
                        }
                        Instantiate(piercingFish, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                    case 2:
                        if (UpgradeHandeler.piercingFishDoors) {
                            i++;
                            break;
                        }
                        Instantiate(piercingFishDoors, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                    case 3:
                        Instantiate(bouncingBullets, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                    case 4:
                        if (UpgradeHandeler.piercingBullets) {
                            i++;
                            break;
                        }
                        Instantiate(piercingBullets, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                    case 5:
                        Instantiate(bulletMultiplier, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                }
            }
        }
        isReady = true;
        if (isReady && (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings == 0) {
            FinishedScreen.SetActive(true);
            if (GameSettings.timerOn) {
                finalTimeText.SetActive(true);
                int minutes = Mathf.FloorToInt(GameSettings.speedrunTimer / 60);
                int seconds = Mathf.FloorToInt(GameSettings.speedrunTimer % 60);
                int milliseconds = Mathf.FloorToInt((GameSettings.speedrunTimer * 1000) % 1000);

                string timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
                GameObject.Find("FinalTimeText").GetComponent<TextMeshProUGUI>().text = "FinalTime: " + timerText;
            }
        }
    }

    public GameObject EnterUpgrade(int i) {
        switch (i) {
            case 3:
                return upgradeSlot1;
            case 2:
                return upgradeSlot2;
            case 1:
                return upgradeSlot3;
            default:
                return null;
        }
    }

    public void BackToMenu() {
        int currentSceneIndex1 = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex1 + 1) % SceneManager.sceneCountInBuildSettings;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.Stop("Theme");
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Loop() {
        GameSettings.timerOn = false;
        SceneManager.LoadScene(1);
    }
}