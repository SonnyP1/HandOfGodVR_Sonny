using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkMan : MonoBehaviour
{
    [SerializeField] Material MTLTOCHANGE;
    HealthComp healthComp;
    Coroutine healthRegenCore;

    private void Start()
    {
        healthComp = GetComponent<HealthComp>();
        healthComp.onHitPointDepleted += OnDeath;
        MTLTOCHANGE.SetFloat("_Progress", healthComp.GetCurrentHitPoints() / healthComp.GetMaxHitPoints());
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Car"))
        {
            healthComp.CallTakeDmg(1);
            MTLTOCHANGE.SetFloat("_Progress",healthComp.GetCurrentHitPoints()/healthComp.GetMaxHitPoints());
            other.GetComponentInParent<Car>().BlowUp();
            if(healthRegenCore != null)
            {
                StopCoroutine(healthRegenCore);
            }
            healthRegenCore = StartCoroutine(RegenHealth());
        }
    }


    IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(5f);
        while(healthComp.GetCurrentHitPoints() != healthComp.GetMaxHitPoints())
        {
            yield return new WaitForSeconds(.5f);
            healthComp.CallTakeDmg(-1);
            MTLTOCHANGE.SetFloat("_Progress", healthComp.GetCurrentHitPoints() / healthComp.GetMaxHitPoints());
            if(MTLTOCHANGE.GetFloat("_Progress") == 1)
            {
                StopCoroutine(healthRegenCore);
            }
        }
        StopCoroutine(healthRegenCore);
    }
}
