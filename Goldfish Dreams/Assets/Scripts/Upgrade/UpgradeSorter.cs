using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSorter : MonoBehaviour {
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

    private void OnEnable() {

        if (isReady) {
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
}
