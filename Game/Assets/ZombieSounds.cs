using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class ZombieSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] soundClips;

    void Start()
    {
        // Add an AudioSource component to the game object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1;

        // Start the coroutine to play sounds randomly
        StartCoroutine(PlayRandomSound());
    }

    IEnumerator PlayRandomSound()
    {
        while (true)
        {
            // Wait for one minute
            yield return new WaitForSeconds(Random.Range(10, 60));

            // 50% chance to play a random sound
            if (Random.value > 0.7f && soundClips.Length > 0)
            {
                AudioClip clip = soundClips[Random.Range(0, soundClips.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
