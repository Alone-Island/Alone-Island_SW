using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private bool effectOn = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        effectOn = true;
    }

    public void Play(string objectName)
    {
        if (effectOn)
        {
            audioSource = GameObject.Find(objectName).GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}
