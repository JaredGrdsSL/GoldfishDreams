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
    public GameObject coolFish;
    public GameObject bouncingFish; 
    public GameObject movementSpeedUp;

    private void Awake() {
        finalTimeText = FinishedScreen.transform.GetChild(2).gameObject;
    }

    private void OnEnable() {
        

        if (isReady && (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings != 0) {
            for (int i = 3; i > 0; i--) {
                //only spawns the speedup if the speedruntimer is off otherwize it could make speedruns rely on rng. 
                int e = Random.Range(1, GameSettings.timerOn ? 7 + 1 : 8 + 1) ;
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
                    case 6:
                        if (UpgradeHandeler.coolFIsh) {
                            i++;
                            break;
                        }
                        Instantiate(coolFish, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                    case 7:
                        Instantiate(bouncingFish, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                    //KEEP MOVESPEEDUPGRADE AT LAST!!
                    case 8:
                        Instantiate(movementSpeedUp, EnterUpgrade(i).transform.position, gameObject.transform.rotation, EnterUpgrade(i).transform);
                        break;
                }
            }
        }
        else if (isReady && (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings == 0) {
            FinishedScreen.SetActive(true);
            GameObject.Find("Deaths").GetComponent<TextMeshProUGUI>().text = "Deaths: " + GameSettings.deaths;
            if (GameSettings.timerOn) {
                finalTimeText.SetActive(true);
                int minutes = Mathf.FloorToInt(GameSettings.speedrunTimer / 60);
                int seconds = Mathf.FloorToInt(GameSettings.speedrunTimer % 60);
                int milliseconds = Mathf.FloorToInt((GameSettings.speedrunTimer * 1000) % 1000);

                string timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
                finalTimeText.GetComponent<TextMeshProUGUI>().text = "FinalTime: " + timerText;
            }
        }
        isReady = true;
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
        GameSettings.deaths = 0;
        GameSettings.speedrunTimer = 0;
        GameSettings.timerOn = false;
        SceneManager.LoadScene(1);
    }
}