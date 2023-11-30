using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCorpse : MonoBehaviour
{
    private void Start() {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().enemiesRemaining--;
    }
}
