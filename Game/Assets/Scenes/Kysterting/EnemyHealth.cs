using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    WaveSpawner waveSpawner;

    public int health = 100;

    private void Start()
    {
        waveSpawner = FindAnyObjectByType<WaveSpawner>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        waveSpawner.Zombies.Remove(this.gameObject);

        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Replace with death animation if needed
    }
}
