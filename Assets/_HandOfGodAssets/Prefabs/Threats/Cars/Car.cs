using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Threat
{
    public override void Init()
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
        Transform walkManTrans = GameplayStatics.GetWalkmanTransform();
        Vector3 SpawnRotUp = -walkManTrans.up;
        Vector3 SpawnRotForward = walkManTrans.forward;


        Quaternion SpawnRot = Quaternion.LookRotation(SpawnRotForward,SpawnRotUp);
        orbitMovementComp.SetRotation(SpawnRot);
    }
}
