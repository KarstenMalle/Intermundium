using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathtubInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    [SerializeField] private Animator myAnimator = null;

    private bool isOpen;
    private GameObject player;
    public GameObject myObject;
    private bool _ePressed = false;
    public GameObject toolTip;

    [SerializeField]
    public AudioSource faucetSound;

    void Start()
    {
        player = GameObject.Find("Capsule Mesh");
        isOpen = false;
        toolTip.SetActive(false);
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
        myAnimator.Play("TurnKnobOn", 0, 0.0f);
        yield return new WaitForSeconds(1);
        isOpen = true;
    }

    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        _ePressed = true;

        if (!isOpen && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !myAnimator.IsInTransition(0))
        {
            faucetSound.Play();
            StartCoroutine(DoorOpenAnimation());
            return true;
        }

        return true;

    }
}
