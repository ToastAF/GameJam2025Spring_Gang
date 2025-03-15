using UnityEngine;

public class Firework : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public float moveSpeed = 2f; // Speed at which it moves to the right
    public float spinSpeed = 100f; // Speed of rotation

    void Start()
    {
        
        Invoke("Explode", 1f); // Explode after 2 seconds
    }

    void Update()
    {
        //GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange);
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject); // Destroy the firework object after exploding
    }
}