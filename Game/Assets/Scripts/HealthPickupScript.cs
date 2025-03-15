using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    public string playerTag = "Player"; // Set this to the tag of the player object
    public float HealAmount = 10f; // The value to add
    public InfectionBehaviour infectionBehaviour;
    public GameObject gameObjectToDestroy;

    private void OnTriggerEnter(Collider other) // Use OnCollisionEnter if not using triggers
    {
        
        infectionBehaviour = other.GetComponent<InfectionBehaviour>();
        
        if (other.CompareTag(playerTag)) // Check if the player collides
        {
            infectionBehaviour.HealInfection(HealAmount);
            Destroy(gameObjectToDestroy); // Remove the object after pickup
        }
    }
}