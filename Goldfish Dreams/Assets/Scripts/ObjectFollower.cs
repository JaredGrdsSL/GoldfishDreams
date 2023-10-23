using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    public GameObject objectToFollow;
    public bool AlsoFollowRotation = true;

    void Update()
    {
        if (objectToFollow != null)
        if (AlsoFollowRotation) {
            gameObject.transform.position = objectToFollow.transform.position;
            gameObject.transform.rotation = objectToFollow.transform.rotation;
        }
        else {
            gameObject.transform.position = objectToFollow.transform.position;
        }
    }
}
