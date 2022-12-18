using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableInteractable : Interactable
{
    [SerializeField] protected ScriptObject_ReadableInteractable ObjectInfo;

    public override bool GetIsReadableText() { return ObjectInfo.isReadable; }
    public override bool GetIsInteractable() { return ObjectInfo.isInteractable; }
    public override string GetText() { return ObjectInfo.text; }

}
