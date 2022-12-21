using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerJumpScare : MonoBehaviour
{
    public GameObject monster;

    void OnTriggerEnter(Collider other)
    {
        monster.SetActive(true);
    }
}
