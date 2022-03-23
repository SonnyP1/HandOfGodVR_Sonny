using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkMan : MonoBehaviour
{
    [SerializeField] Material MTLTOCHANGE;
    [SerializeField] LayerMask DamagableLayerMask;
    HealthComp healthComp;
    Coroutine healthRegenCore;
    Coroutine dmgOverTimeCore;

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
        int otherLayerAsDigit = other.gameObject.layer;
        int LayerMaskData = DamagableLayerMask;

        //Debug.Log(other.gameObject.name);
        if ( (LayerMaskData & (1 << otherLayerAsDigit)) != 0)
        {
            if (otherLayerAsDigit == LayerMask.NameToLayer("Hail"))
            {
                Debug.Log("IT IS HAIL");
                dmgOverTimeCore = StartCoroutine(DmgOverTime());
                return;
            }
            healthComp.CallTakeDmg(1);
            MTLTOCHANGE.SetFloat("_Progress",healthComp.GetCurrentHitPoints()/healthComp.GetMaxHitPoints());
            other.GetComponentInParent<Threat>().BlowUp();
            StartHealthRegen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Hail"))
        {
            StopCoroutine(dmgOverTimeCore);
            dmgOverTimeCore = null;
            StartHealthRegen();
        }
    }

    private void StartHealthRegen()
    {
        if (healthRegenCore != null)
        {
            StopCoroutine(healthRegenCore);
        }
        healthRegenCore = StartCoroutine(RegenHealth());
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

    IEnumerator DmgOverTime()
    {
        if(healthRegenCore != null)
        {
            StopCoroutine(healthRegenCore);
        }
        yield return new WaitForSeconds(0.1f);
        healthComp.CallTakeDmg(0.4f);
        MTLTOCHANGE.SetFloat("_Progress", healthComp.GetCurrentHitPoints() / healthComp.GetMaxHitPoints());
        dmgOverTimeCore = StartCoroutine(DmgOverTime());
    }
}
