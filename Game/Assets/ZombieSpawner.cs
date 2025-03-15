using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public WaveSpawner waveSpawner;

    public List<GameObject> Zombies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnZombie()
    {
        GameObject gm = Instantiate(Zombies[0], transform.position, transform.rotation);

        Zombies.Remove(Zombies[0]);

        waveSpawner.Zombies.Add(gm);
    }
}
