using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpookTrigger : MonoBehaviour
{
    public GameObject spook;

    public GameObject old_door;
    public GameObject new_door;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        spook.SetActive(true);
        old_door.SetActive(false);
        new_door.SetActive(true);
    }
}
