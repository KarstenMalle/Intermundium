using UnityEngine;
using Unity;

[CreateAssetMenu(fileName= "ScriptObject_AudioInteractable", menuName = "ScriptableObjects/Audio Interactable Script Object")]
public class ScriptObject_AudioInteractable : ScriptableObject, IAnimatable, _IInteractable, IsAudible
{
    //Our current Observable object properties
    [SerializeField]    public bool isInteractable = false;
    [SerializeField]    public bool isAnimateable = false;
    [SerializeField]    public bool isAudible = false;

    [SerializeField]    public AudioClip audioClip;
    [SerializeField]    public Animation animate;

    private bool resetInspectable;
    private bool resetObservable;

    //Animate Interface:
    bool IAnimatable.isAnimateable { get => isAnimateable; set => isAnimateable = value;}
    Animation IAnimatable.Animator { get => animate; }

    //Interact Interface:
    bool _IInteractable.IsInteractable { get => isInteractable; set => isInteractable = value; }

    //Audible Interface
    AudioClip IsAudible.audioClip { get => audioClip; }
    bool IsAudible.IsAudible { get => isAudible; set => isAudible = value; }


    private void Awake()
    {
        resetInspectable = isInteractable;
        resetInspectable = isAnimateable;
    }

    public void resetAudioObservableObject()
    {
        isInteractable = resetInspectable;
        isAnimateable = resetObservable;
    }

}

