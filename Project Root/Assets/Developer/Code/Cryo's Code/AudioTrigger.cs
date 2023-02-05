using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private AudioManager _manager;
    void Start()
    {
        try
        {
            _manager = FindObjectOfType<AudioManager>();
        }
        catch { Debug.Log("Could not find audioManager"); }
        
        _manager.Play("EndSong");
    }
}
