using UnityEngine;
using TMPro;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class WaveSpawner : MonoBehaviour
{
    public int Wave;

    public TMP_Text WaveText;

    public GameObject Player;

    public int ZombiesToSpawn;
    public List<GameObject> ZombieSpawners;
    public List<GameObject> Zombies = new List<GameObject>();

    public GameObject ZombiePrefab;
    public bool HasMadeWave;

    public float Timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (HasMadeWave == false) 
        {
            if (Zombies.Count == 0)
            {
                Wave += 1;

                ZombiesToSpawn = 3 + 2 * Wave + Mathf.FloorToInt(Wave / 5);

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

                List<GameObject> ZombieSpawners2 = new List<GameObject>(ZombieSpawners);

                for (int i = 0; i < ZombieSpawners.Count; i++)
                {
                    if (ZombiesToSpawn > 0)
                    {
                        if (ZombieSpawners2.Count > 0)
                        {
                            int _rr = Random.Range(0, ZombieSpawners2.Count);


                            if (Vector3.Distance(ZombieSpawners2[_rr].transform.position, Player.transform.position) > 30)
                            {
                                GameObject Zombie = Instantiate(ZombiePrefab, ZombieSpawners2[_rr].transform.position, Quaternion.identity);
                                Zombies.Add(Zombie);

                                ZombiesToSpawn -= 1;
                            }

                            ZombieSpawners2.Remove(ZombieSpawners2[_rr]);

                           
                        }
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


                if (ZombiesToSpawn <= 0)
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
