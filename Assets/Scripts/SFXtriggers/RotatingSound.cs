using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSound : MonoBehaviour
{
    public AudioSource speaker;

    private float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //rotate around y axis
        transform.Rotate(Vector3.up * speed * Time.deltaTime);

        //oscillate up and down through Rotate
        float angle = Mathf.Sin(Time.time * 10f) * 50f;
        transform.Rotate(new Vector3(angle, 0, 0) * Time.deltaTime);

    }
}
