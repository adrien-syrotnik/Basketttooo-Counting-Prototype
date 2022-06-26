using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.Play();
    }
}
