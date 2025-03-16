using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class InfectionBehaviour : MonoBehaviour
{
    public float infectionTimer, infectionPerTick, currentInfection, maxInfection;

    bool hasInfected; //Used to time the infection in intervals

    public float amountOfZombiesNearby;

    public RectTransform infectionBarUI;

    public WaveSpawner waveSpawner;

    private AudioSource audioSourceGameplay;
    public AudioClip audioClipSourceGameplay;

    private AudioSource audioSourceZombiesClose;
    public AudioClip audioClipSourceZombiesClose;

    void Start()
    {
        waveSpawner = FindAnyObjectByType<WaveSpawner>();

        currentInfection = 0;
        hasInfected = false;
        UpdateInfectionUI();

        audioSourceGameplay = gameObject.AddComponent<AudioSource>();
        audioSourceGameplay.clip = audioClipSourceGameplay;
        audioSourceGameplay.loop = true;
        audioSourceGameplay.volume = 0;
        audioSourceGameplay.Play();


        audioSourceZombiesClose = gameObject.AddComponent<AudioSource>();
        audioSourceZombiesClose.clip = audioClipSourceZombiesClose;
        audioSourceZombiesClose.loop = true;
        audioSourceZombiesClose.volume = 0;
        audioSourceZombiesClose.Play();
    }

    void Update()
    {
        UpdateInfection();

        if(currentInfection >= maxInfection)
        {
            waveSpawner.DeathScreen.SetActive(true);
        }

        if (amountOfZombiesNearby > 1)
        {
            if (audioSourceGameplay.volume > 0f)
            {
                audioSourceGameplay.volume -= 0.002f;
                audioSourceGameplay.volume = Mathf.Clamp(audioSourceGameplay.volume, 0f, 0.05f);
            }
            
            if (audioSourceZombiesClose.volume < 0.05f)
            {
                audioSourceZombiesClose.volume += 0.002f;
                audioSourceZombiesClose.volume = Mathf.Clamp(audioSourceZombiesClose.volume, 0f, 0.1f);
            }
        }
        else if (amountOfZombiesNearby <= 1)
        {
            if (audioSourceGameplay.volume < 0.05f)
            {
                audioSourceGameplay.volume += 0.002f;
                audioSourceGameplay.volume = Mathf.Clamp(audioSourceGameplay.volume, 0f, 0.05f);
            }
            if (audioSourceZombiesClose.volume > 0f)
            {
                audioSourceZombiesClose.volume -= 0.002f;
                audioSourceZombiesClose.volume = Mathf.Clamp(audioSourceZombiesClose.volume, 0f, 0.1f);
            }
        }

    }

    public void UpdateInfection()
    {
        if(hasInfected == false && amountOfZombiesNearby > 0)
        {
            StartCoroutine(InfectCD(infectionTimer));

            UpdateInfectionUI();
        }
    }

    public void UpdateInfectionUI()
    {
        float barWidth = currentInfection;
        Mathf.Clamp(barWidth, 0, maxInfection);
        infectionBarUI.sizeDelta = new Vector2(barWidth, infectionBarUI.sizeDelta.y);
    }

    public void HealInfection(float healValue)
    {
        currentInfection -= healValue; //We "heal"
        UpdateInfectionUI();
    }

    public void DamageInfection(float damageValue)
    {
        currentInfection += damageValue;
        UpdateInfectionUI();
    }

    IEnumerator InfectCD(float time)
    {
        hasInfected = true;
        currentInfection += infectionPerTick * amountOfZombiesNearby;
        yield return new WaitForSeconds(time);
        hasInfected = false;
    }
}
