using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBegone : MonoBehaviour
{

    MeshRenderer trigger_cube;
    BoxCollider trigger_box;

    // Start is called before the first frame update
    void Start()
    {
        trigger_cube = GetComponent<MeshRenderer>();
        trigger_box = GetComponent<BoxCollider>();
        trigger_cube.enabled = false;
        trigger_box.enabled = false;

        if (trigger_cube.name == "Trigger") {
            trigger_box.enabled = true;
        }
    }
}
