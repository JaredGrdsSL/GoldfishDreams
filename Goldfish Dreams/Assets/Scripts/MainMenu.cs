using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject settingsPannel;
    private GameObject credditsPannel;
    private Toggle timerToggler;
    private Slider audioSlider;

    private void Awake() {
        audioSlider = GameObject.Find("AudioSlider").GetComponent<Slider>();
        timerToggler = GameObject.Find("TimerToggle").GetComponent<Toggle>();
    }

    private void Start() {
        settingsPannel = GameObject.Find("SettingsPannel");
        credditsPannel = GameObject.Find("CredditsPannel");
        credditsPannel.SetActive(false);
        settingsPannel.SetActive(false);

        GameSettings.speedrunTimer = 0;
        audioSlider.value = AudioListener.volume;
        timerToggler.isOn = GameSettings.timerOn;
    }

    public void StartGame() {
        if (GameSettings.timerOn) { 
            GameSettings.speedrunTimer = 0;
        }
        GameSettings.deaths = 0;

        UpgradeHandeler.piercingFish = false;
        UpgradeHandeler.piercingFishDoors = false;
        UpgradeHandeler.bulletBouncePower = 0;
        UpgradeHandeler.piercingBullets = false;
        UpgradeHandeler.bulletMultiplyer = 1;

        SceneManager.LoadScene(1);
    }

    public void Settings() {
        settingsPannel.SetActive(true);
    }

    public void Creddits() {
        credditsPannel.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void timerToggle(bool tog) {
        GameSettings.timerOn = tog;
        Debug.Log(GameSettings.timerOn);
    }

    public void BackButtonSettings() {
        settingsPannel.SetActive(false);
    }

    public void BackButtonCreddits() {
        credditsPannel.SetActive(false);
    }

    public void ChangedVolume() {
        AudioListener.volume = audioSlider.value;
    }
}
