using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Assign your footstep sounds in the Inspector
    public float stepInterval = 0.5f; // Base time between steps

    private AudioSource audioSource;
    private Transform playerTransform;
    private Vector3 lastPosition;
    private float stepTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = transform.parent; // Get player object
        lastPosition = playerTransform.position;

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on " + gameObject.name);
        }
    }

    void FixedUpdate()
    {
        float distanceMoved = Vector3.Distance(playerTransform.position, lastPosition);

        // Dynamically adjust step interval based on movement speed
        float speedFactor = Mathf.Clamp(distanceMoved * 10f, 0.4f, 1.2f);
        float dynamicStepInterval = stepInterval / speedFactor; // Faster movement = faster steps

        if (distanceMoved > 0.01f) // If the player is moving
        {
            stepTimer += Time.fixedDeltaTime;

            if (stepTimer >= dynamicStepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = Mathf.Max(0f, stepTimer - Time.fixedDeltaTime); // Small delay before stopping
        }

        lastPosition = playerTransform.position;
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip randomClip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            
            audioSource.pitch = Random.Range(0.9f, 1.1f); // Randomize pitch
            float randomVolume = Random.Range(0.8f, 1.0f); // Randomize volume
            
            audioSource.PlayOneShot(randomClip, randomVolume);
        }
        else
        {
            Debug.LogWarning("No footstep sounds assigned!");
        }
    }
}