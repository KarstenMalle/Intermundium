using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlay_pages : MonoBehaviour
{
    public GameObject toolTip;
    private GameObject player;

    [SerializeField] private GameObject[] pages;

    public GameObject[] deco;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        toolTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject pageObject in pages) {
            if (Vector3.Distance(player.transform.position, pageObject.transform.position) < (2 * 1))
            {
                //Debug.Log((player.transform.position - this.transform.position).sqrMagnitude);
                toolTip.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    foreach (GameObject page in pages){
                        page.SetActive(false);
                    }
                    foreach (GameObject dec in deco){
                        dec.SetActive(true);
                    }
                    overlay_activation.have_pages = true;
                }
            }
            else
            {
                toolTip.SetActive(false);
            }   
        }
    }
}
