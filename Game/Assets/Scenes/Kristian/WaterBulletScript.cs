using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public ParticleSystem waterSpray;  // Reference to the Particle System
    public float pushForce = 10f;      // Strength of the force applied to enemies
    public float duration = 3f;        // How long the water spray lasts

    private Transform gun;             // Reference to the Gun GameObject
    private float timer = 0f;          // Timer to track duration

    private void Start()
    {
        // Find the "Gun" GameObject
        gun = GameObject.Find("Gun")?.transform;

        if (gun != null)
        {
            // Make WaterGun a child of "Gun" and match its transform
            transform.SetParent(gun);
            transform.position = gun.position;
            transform.rotation = gun.rotation;
            
            
            //HAHHAHAHAHH
            transform.position += gun.forward * 1.5f;
            transform.position += gun.right * +0.5f;
            transform.position += gun.up * +0.5f;
        }
        else
        {
            Debug.LogWarning("Gun GameObject not found! Make sure it's named 'Gun' in the hierarchy.");
        }

        // Enable particle collision
        var collision = waterSpray.collision;
        collision.enabled = true;
        collision.sendCollisionMessages = true;

        // Start the particle system
        waterSpray.Play();
    }

    private void Update()
    {
        // Keep track of duration and stop/destroy effect
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            waterSpray.Stop(); // Stop the effect
            Destroy(gameObject, 1f); // Destroy after a short delay
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // Check if the object hit is an enemy
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply force in the direction of the spray
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;
            rb.AddForce(forceDirection * pushForce, ForceMode.Impulse);
        }
    }
}