using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish;
    public bool canSpawn = true;
    public float coolDown;

    private void Update() {
        if (canSpawn) {
            StartCoroutine(SpawnFish());
        }
    }

    IEnumerator SpawnFish() {
        canSpawn = false;
        GameObject.Destroy(Instantiate(fish, new Vector2(Random.Range(-26,26), 17), gameObject.transform.rotation), 3);
        yield return new WaitForSeconds(coolDown);
        canSpawn = true;
    }
}
