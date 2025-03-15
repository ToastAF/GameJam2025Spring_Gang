using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public int Wave;

    public TMP_Text WaveText;

    public List<ZombieSpawner> ZombieSpawners;
    public List<GameObject> Zombies = new List<GameObject>();

    public GameObject ZombiePrefab;
    public bool HasMadeWave;

    public float Timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var item in ZombieSpawners)
        {
            item.waveSpawner = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HasMadeWave == false) 
        {
            if (Zombies.Count == 0)
            {
                Wave += 1;

                for (int i = 0; i < 3 + 2 * Wave + Mathf.FloorToInt(Wave / 5); i++)
                {
                    ZombieSpawners[Random.Range(0, ZombieSpawners.Count)].Zombies.Add(ZombiePrefab);
                }

                WaveText.text = "Wave" + "\n" + Wave;
                WaveText.gameObject.SetActive(true);

                Timer = 0;


                HasMadeWave = true;
            }
        }
        else
        {
            if (Timer > 10)
            {
                WaveText.gameObject.SetActive(false);
                bool HasNoZombiesLeft = false;

                foreach (var item in ZombieSpawners)
                {
                    if (item.Zombies.Count != 0)
                    {
                        HasNoZombiesLeft = true;

                        item.SpawnZombie();
                    }
                }

                foreach (var item in Zombies.ToArray())
                {
                    if (item == null)
                    {
                        Zombies.Remove(item);
                    }
                }

                Timer = 0;


                if (HasNoZombiesLeft == false)
                {
                    HasMadeWave = false;
                }
            }
            else
            {
                Timer += Time.deltaTime;
            }


            
        }
    }


}
