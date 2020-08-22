using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    public AudioClip dash;
    public AudioClip explose;
    public AudioClip ult;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDashSound()
    {
        transform.GetComponent<AudioSource>().volume = 0.6f;
        source.PlayOneShot(dash);
    }

    public void playExploseSound()
    {
        transform.GetComponent<AudioSource>().volume = 0.6f;
        source.PlayOneShot(explose);
    }

    public void playUltSound()
    {
        transform.GetComponent<AudioSource>().volume = 1.0f;
        source.PlayOneShot(ult);
    }
}
