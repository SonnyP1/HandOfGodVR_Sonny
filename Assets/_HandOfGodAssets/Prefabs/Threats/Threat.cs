using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Threat : MonoBehaviour
{
    private ThreatSpawner _spawner;
    public virtual void Init(ThreatSpawner spawner)
    {
        _spawner = spawner;
        _spawner.AddThreatToSpawnerList(this);
    }

    public virtual void BlowUp()
    {
        _spawner.RemoveThreatInSpawnerList(this);
    }
}
