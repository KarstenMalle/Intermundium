using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeLevel : MonoBehaviour
{

    public AudioSource m_MyAudioSourse;

    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSourse.volume = MainMenu.VolumeLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Options.VolumeValue == 0)
        {
            m_MyAudioSourse.volume = MainMenu.VolumeLevel;
        } 
        else
        {
            m_MyAudioSourse.volume = Options.VolumeValue;
        }
        Debug.Log(m_MyAudioSourse.volume);
    }
}
