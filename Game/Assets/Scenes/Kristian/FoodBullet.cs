using UnityEngine;

public class Food : MonoBehaviour
{
    public AudioClip eatSound; 
    private AudioSource audioSource;
    private bool canBeEaten = false;
    public static int score = 0; //Temporary variable for testing
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (eatSound != null)
        {
            audioSource.clip = eatSound;
        }
        Invoke("EnableTrigger", 1.0f);
    }

    void EnableTrigger()
    {
        canBeEaten = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBeEaten && other.CompareTag("Player"))
        {

            InfectionBehaviour infectionBehaviour = other.GetComponent<InfectionBehaviour>();
            if (infectionBehaviour == null)
            {
                Debug.Log("PLEASE PLACE INFECTIONBEHAVIOUR ON PLAYER, FOOD BULLET NEEDS IT!!!!");
            }
            else
            {
                infectionBehaviour.HealInfection(10);
            }

            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(eatSound);
            }
            else
            {
                Debug.Log("PLEASE PLACE AUDIO SOURCE ON PLAYER, FOOD BULLET NEEDS IT!!!!");
            }
                
                
            Destroy(transform.parent.gameObject, 0.1f);

        }
    }
}