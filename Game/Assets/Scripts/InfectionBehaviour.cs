using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InfectionBehaviour : MonoBehaviour
{
    public float infectionTimer, infectionPerTick, currentInfection, maxInfection;

    bool hasInfected; //Used to time the infection in intervals

    public float amountOfZombiesNearby;

    public RectTransform infectionBarUI;

    void Start()
    {
        currentInfection = 0;
        hasInfected = false;
        UpdateInfectionUI();
    }

    void Update()
    {
        UpdateInfection();

        if(currentInfection >= maxInfection)
        {
            Debug.Log("You died!");
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
