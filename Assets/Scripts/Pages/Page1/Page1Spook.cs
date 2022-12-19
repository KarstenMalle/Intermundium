using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page1Spook : MonoBehaviour
{
    private GameObject playerView = null;

    private bool spooked = false;
    private bool sound = false;

    public GameObject monster;
    public AudioSource speaker;
    // Start is called before the first frame update
    void Start()
    {
        playerView = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        float view = playerView.transform.eulerAngles.y;

        if (FirstPage.pageCollected && !spooked) {
            monster.SetActive(true);
            if (!sound) {
                Debug.Log("play sound");
                speaker.Play();
                sound = true;
                monster.SetActive(true);
            }
            if (view < 220) {
                Debug.Log("vanish");
                monster.SetActive(false);
                spooked = true;
            }
        }
    }
}
