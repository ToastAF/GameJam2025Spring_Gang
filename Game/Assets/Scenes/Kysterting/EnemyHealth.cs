using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    WaveSpawner waveSpawner;

    public int health = 100;

    AI_Movement AI_Movement;

    public GameObject HealthPackPrefab;

    private void Start()
    {
        waveSpawner = FindAnyObjectByType<WaveSpawner>();

        AI_Movement = GetComponent<AI_Movement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining Health: " + health);

        if (health <= 0)
        {
            Die();     
        }
        else
        {
            int _rr = Random.Range(1, 21);
            if (_rr == 1)
            {
                AI_Movement.Cripel = true;
            }
            else
            {
                AI_Movement.Ani.SetBool("Hit", true);
            }

            AI_Movement.WaitAMoment = true;
        }
    }

    private void Die()
    {
        waveSpawner.Zombies.Remove(this.gameObject);
        AI_Movement.Ani.SetBool("Death", true);
        AI_Movement.Death = true;
        
        GetComponent<Rigidbody>().isKinematic = true;

        FindAnyObjectByType<InfectionBehaviour>().amountOfZombiesNearby -= 1;

        int _rr = Random.Range(1, 101);

        if (_rr > 80)
        {
            Instantiate(HealthPackPrefab, transform.position, Quaternion.identity);
        }

        Debug.Log(gameObject.name + " Dead ");
    }

    IEnumerator PlayRandomSound()
    {
        yield return new WaitForSeconds(60);

        Destroy(this.gameObject);
    }
}
