using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomTimedTrigger : MonoBehaviour
{
    private AudioManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
         _manager = FindObjectOfType<AudioManager>();

        }
        catch{ Debug.Log("Could not find audioManager"); }
        StartCoroutine(CorRandomTimedTrigger());
    }

    private IEnumerator CorRandomTimedTrigger()
    {
        while (true)
        {

            float waitTime = Random.Range(3, 6);
            _manager.PlayOneShot("Ding");
            yield return new WaitForSeconds(waitTime);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
