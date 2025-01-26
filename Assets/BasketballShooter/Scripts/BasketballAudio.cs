using System.Collections.Generic;
using UnityEngine;

public class BasketballAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> floorClips = new List<AudioClip>();
    public List<AudioClip> brickClips = new List<AudioClip>();
    public List<AudioClip> swishClips = new List<AudioClip>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter(Collision other)
    {
        // resets
        audioSource.clip = null;
        audioSource.pitch = 1;

        if (other.gameObject.tag == "Ground")
            // files cropped w/ Audacity from
            // https://freesound.org/people/14GPanskaValaChristoffer/sounds/420160/
            audioSource.clip = GetRandomClip(floorClips);
        else if (other.gameObject.tag == "Rim")
            // https://freesound.org/people/zmobie/sounds/319784/
            audioSource.clip = GetRandomClip(brickClips);
        else if (other.gameObject.tag == "Net")
            // https://freesound.org/people/akennedybrewer/sounds/389170/
            audioSource.clip = GetRandomClip(swishClips);

        if (audioSource.clip != null)
            audioSource.Play();
    }

    AudioClip GetRandomClip(List<AudioClip> clips)
    {
        if (clips.Count <= 1)
            // there's only one sounds so vary the pitch to make it sound realistic
            audioSource.pitch = Random.Range(.8f, 1.2f);
        return clips[Random.Range(0, clips.Count - 1)];
    }
}
