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
        source.PlayOneShot(dash);
    }

    public void playExploseSound()
    {
        source.PlayOneShot(explose);
    }

    public void playUltSound()
    {
        source.PlayOneShot(ult);
    }
}
