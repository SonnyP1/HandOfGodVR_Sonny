using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDamageTaken(float newAmt, float oldAmt, object attacker);
public delegate void OnHitPointDepleted();
public class HealthComp : MonoBehaviour
{
    [SerializeField]  float hitPoints;
    [SerializeField]  float maxHitPoints = 10;
    private object _attacker;
    public OnDamageTaken onDamageTaken;
    public OnHitPointDepleted onHitPointDepleted;

    public float GetMaxHitPoints() { return maxHitPoints;}
    public float GetCurrentHitPoints() { return hitPoints; }

    public void CallTakeDmg(float amt)
    {
        TakeDmg(amt);
    }
    private void TakeDmg(float amt)
    {
        float oldVal = hitPoints;
        hitPoints = Mathf.Clamp(hitPoints-amt,0,maxHitPoints);
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            if (onHitPointDepleted != null)
            {
                onHitPointDepleted.Invoke();
            }
        }
        else
        {
            //notify dmg taken
            if (oldVal != hitPoints)
            {
                if (onDamageTaken != null)
                {
                    onDamageTaken.Invoke(hitPoints, oldVal,_attacker);
                }
            }
        }
    }
}
