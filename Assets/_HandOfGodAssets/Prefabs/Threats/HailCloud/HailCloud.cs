using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailCloud : Threat , IDragable
{
    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        throw new System.NotImplementedException();
    }
    public void Release(Vector3 ThrowVelocity)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(ThreatSpawner spawner)
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
        Transform walkManTrans = GameplayStatics.GetWalkmanTransform();
        Vector3 SpawnRotUp = new Vector3(Random.Range(0,360),
            0,
            0);
        Vector3 SpawnRotForward = transform.forward * Random.Range(0, 50);

        Quaternion SpawnRot = Quaternion.LookRotation(SpawnRotForward, SpawnRotUp);
        orbitMovementComp.SetRotation(SpawnRot);
    }

    public override void BlowUp()
    {
        throw new System.NotImplementedException();
    }

}
