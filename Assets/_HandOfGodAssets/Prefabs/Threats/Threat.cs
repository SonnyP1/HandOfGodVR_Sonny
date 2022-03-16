using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Threat : MonoBehaviour
{
    public abstract void Init(ThreatSpawner spawner);

    public abstract void BlowUp();
}
