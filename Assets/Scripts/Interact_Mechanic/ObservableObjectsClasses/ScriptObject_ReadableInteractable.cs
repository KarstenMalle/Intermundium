using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObject_ReadableInteractable", menuName = "ScriptableObjects/Readable Interactable Script Object")]
public class ScriptObject_ReadableInteractable : ScriptableObject, IReadable, _IInteractable 
{
    [SerializeField] public bool isReadable;
    [SerializeField] public bool isInteractable;
    [SerializeField][TextArea(15, 20)]public string text;

    private bool resetReadable;
    private bool resetInteractable;

    bool IReadable.IsReadable { get => isReadable; set => isReadable = value; }

    string IReadable.text { get => text; set => text = value; }
    
    bool _IInteractable.IsInteractable { get => isInteractable; set => isInteractable = value; }

    private void Awake()
    {
        resetReadable = isReadable;
        resetInteractable = isInteractable;
}

    public void resetReadableObject()
    {
        isReadable = resetReadable;
        isInteractable = resetInteractable;
    }
}
