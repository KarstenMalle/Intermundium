using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShelfInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    [SerializeField] private Animator myAnimator = null;

    public static bool isOpen;
    private GameObject player;
    public GameObject myObject;
    private bool _ePressed = false;
    public GameObject toolTip;

    public GameObject page3Object;

    [SerializeField]
    public AudioSource doorOpenSound, doorCloseSound;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        isOpen = false;
        toolTip.SetActive(false);
        page3Object.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, myObject.transform.position) < (2 * 1) && _ePressed == false)
        {
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }
    }

    private IEnumerator DoorOpenAnimation()
    {
        page3Object.SetActive(true);
        myAnimator.Play("DoorOpen", 0, 0.0f);
        yield return new WaitForSeconds(1);
        isOpen = true;
    }

    private IEnumerator DoorCloseAnimation()
    {
        myAnimator.Play("DoorClose", 0, 0.0f);
        yield return new WaitForSeconds(1);
        isOpen = false;
        page3Object.SetActive(false);
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        _ePressed = true;

        if (!isOpen && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !myAnimator.IsInTransition(0))
        {
            doorOpenSound.Play();
            StartCoroutine(DoorOpenAnimation());
            return true;
        }
        if (isOpen && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !myAnimator.IsInTransition(0))
        {
            doorCloseSound.Play();
            StartCoroutine(DoorCloseAnimation());
            return true;
        }

        return true;

    }
}




