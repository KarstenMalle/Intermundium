using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

//[CustomEditor(typeof(ScriptableObject))]
public class ScriptableObjectInspectManager : MonoBehaviour
{

    [SerializeField] public ScriptObject_AudioInteractable ObjectInfoAudioInteractable = null;
    
    //Add more if there is any more of these needed
    //[SerializeField] public new Interactable ObjectInfoInteractable = null;
    //[SerializeField] public new AudioInteractable ObjectInfoAudioInteractable = null;

    //[SerializeField] public AudioInteractable ObjectInfo = null;
    //[SerializeField] public AudioInteractable ObjectInfo = null;
    //[SerializeField] public AudioInteractable ObjectInfo = null;

    public ScriptableObject ObjectInfo;

    private void Awake()
    {
        if (ObjectInfoAudioInteractable != null)
        {
            ObjectInfo = (ScriptableObject)ObjectInfoAudioInteractable;
        }
        //else if()
        //{
        //
        //}else if ()
        //{
        //
        //}
    }



    //public enum ScriptObjTypes
    //{
    //    None,
    //    Observable,
    //    AudioObservable,
    //    AudioInteractable, 
    //    ReadableObservable,
    //    AnimationInteractable,
    //    AnimationAudioInteractable
    //}
    //
    //
    //public ScriptObjTypes ObjectType;
    //
    //public object script;
    //
    //public override void OnInspectorGUI()
    //{
    //
    //    categoryToDisplay = (ScriptObjTypes)EditorGUILayout.EnumPopup("Type", ScriptObjTypes);
    //    EditorGUILayout.Space();
    //
    //    switch (categoryToDisplay)
    //    {
    //        case None:
    //            Debug.LogWarning("Choose ScriptableObject SubClass");
    //            break;
    //
    //        case ObjectType.Observable:
    //            DisplayObservable();
    //            break;
    //
    //        case ObjectType.AudioObservable:
    //            DisplayAudioObservable();
    //            break;
    //
    //        case ObjectType.AudioInteractable:
    //            DisplayAudioInteractable();
    //            break;
    //
    //        case ObjectType.ReadableObservable:
    //            DisplayReadableObservable();
    //            break;
    //
    //        case ObjectType.AnimationInteractable:
    //            DisplayAnimationInteractable();
    //            break;
    //        case ObjectType.AnimationAudioInteractable:
    //            DisplayAnimationAudioInteractable();
    //            break;
    //    }
    //
    //    
    //    serializedObject.ApplyModifiedProperties();
    //}
    //
    //void DisplayAudioInteractable()
    //{
    //    EditorGUILayout.ObjectField(Object, AudioInteractable);
    //}
    //
    //void DisplayAudioObservable()
    //{
    //    throw new NotImplementedException();
    //}
    //
    //void DisplayObservable()
    //{
    //    throw new NotImplementedException();
    //}
    //
    //void DisplayReadableObservable()
    //{
    //    throw new NotImplementedException();
    //}
    //
    //void DisplayAnimationInteractable()
    //{
    //    throw new NotImplementedException();
    //}
    //
    //void DisplayAnimationAudioInteractable()
    //{
    //    throw new NotImplementedException();
    //}

}   
    