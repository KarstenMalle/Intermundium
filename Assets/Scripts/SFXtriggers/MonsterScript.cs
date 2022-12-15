using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private AudioSource monsterSound;

    private bool spooked = false;
    static public bool ready = false;

    private bool clocked = false;
    private float time_at_trigger = 0;
    private float scream_duration = 2.040f;

    private float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        monsterSound = GetComponent<AudioSource>(); 
    }

    void Update() 
    {
        if (!spooked){
            if (ready && !clocked) {
                clocked = true;
                time_at_trigger = Time.time;
                monsterSound.Play();
            }

            if (ready && (Time.time - time_at_trigger) < scream_duration) {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }

            if (Time.time - time_at_trigger > scream_duration) {
                spooked = true;

            }
        }
    }
}
