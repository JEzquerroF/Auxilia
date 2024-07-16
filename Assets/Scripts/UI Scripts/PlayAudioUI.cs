using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioUI : MonoBehaviour
{
    public GameObject audioObj;
    public GameObject audioObjClick;

    public void DropAudio()
    {
        Instantiate(audioObj, transform.position, transform.rotation);
    }
    public void ClickAudio()
    {
        Instantiate(audioObjClick, transform.position, transform.rotation);
    }
}
