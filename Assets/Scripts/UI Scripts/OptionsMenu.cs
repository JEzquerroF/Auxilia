using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // to reference audio mixer

public class OptionsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
  
}
