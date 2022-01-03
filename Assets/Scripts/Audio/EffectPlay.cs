using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private bool effectOn = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("FindBookEffect").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        effectOn = true;
    }

    public void Play()
    {
        if (effectOn)
        {
            audioSource.Play();
        }
    }
}
