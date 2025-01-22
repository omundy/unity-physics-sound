using System.Collections.Generic;
using UnityEngine;

public class BasketballAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> floorClips = new List<AudioClip>();
    public List<AudioClip> brickClips = new List<AudioClip>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            // play random bounce clip
            // each cropped from this file using Audacity: https://freesound.org/people/14GPanskaValaChristoffer/sounds/420160/
            audioSource.clip = GetRandomClip(floorClips);
        }
        else if (other.gameObject.tag == "Rim")
        {
            // play random brick clip
            // https://freesound.org/people/zmobie/sounds/319784/
            audioSource.clip = GetRandomClip(brickClips);
        }

        if (audioSource.clip != null)
            audioSource.Play();
    }

    AudioClip GetRandomClip(List<AudioClip> clips)
    {
        return clips[Random.Range(0, clips.Count - 1)];
    }
}
