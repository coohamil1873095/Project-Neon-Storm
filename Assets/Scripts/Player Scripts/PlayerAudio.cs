using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip shootingClip;
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Main Attack SFX
        if (Input.GetMouseButtonDown(0))
        {
            m_AudioSource.PlayOneShot(shootingClip);
        }
        if (Input.GetMouseButton(0) && !m_AudioSource.isPlaying)
        {
            m_AudioSource.PlayOneShot(shootingClip);
        }
            if (Input.GetMouseButtonUp(0))
        {
            m_AudioSource.Stop();
        }
    }
}
