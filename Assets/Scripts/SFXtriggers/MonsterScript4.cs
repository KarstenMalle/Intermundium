using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript4 : MonoBehaviour
{
    private AudioSource monsterSound;

    private bool spooked = false;

    private bool clocked = false;
    private float time_at_trigger = 0;
    private float scream_duration = 2.040f;

    private float speed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        monsterSound = GetComponent<AudioSource>();
        monsterSound.Play();
    }

    void Update() 
    {
        if (!spooked){
            if (!clocked) {
                clocked = true;
                time_at_trigger = Time.time;
                monsterSound.Play();
            }

            if ((Time.time - time_at_trigger) < scream_duration) {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if (Time.time - time_at_trigger > scream_duration) {
                spooked = true;

            }
        }
    }
}
