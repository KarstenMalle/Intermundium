using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBegone : MonoBehaviour
{

    MeshRenderer trigger_cube;

    // Start is called before the first frame update
    void Start()
    {
        trigger_cube = GetComponent<MeshRenderer>();
        trigger_cube.enabled = false;
    }
}
