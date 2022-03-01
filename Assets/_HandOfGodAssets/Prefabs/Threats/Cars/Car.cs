using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Threat , IDragable
{
    [SerializeField] Transform[] Lanes;
    [SerializeField] Transform CarTransform;
    [SerializeField] float MoveCarLaneSpeed = 5f;
    Vector3 desiredPos;

    public override void Init()
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
        Transform walkManTrans = GameplayStatics.GetWalkmanTransform();
        Vector3 SpawnRotUp = -walkManTrans.up;
        Vector3 SpawnRotForward = walkManTrans.forward;


        Quaternion SpawnRot = Quaternion.LookRotation(SpawnRotForward,SpawnRotUp);
        orbitMovementComp.SetRotation(SpawnRot);
        desiredPos = CarTransform.localPosition;
        StartCoroutine(MoveCarToDesiredPos());
    }
    private Vector3 GetClosestLanePos(Vector3 pointToCompare)
    {
        Transform closestLane = null;
        float closestDist = float.MaxValue;

        foreach(Transform laneTrans in Lanes)
        {
            float distance = Vector3.Distance(laneTrans.localPosition, pointToCompare);
            if(distance < closestDist)
            {
                closestLane = laneTrans;
                closestDist = distance;
            }
        }
        return closestLane.localPosition;
    }

    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        Vector3 posToMoveCar = GetClosestLanePos(grabPoint);
        desiredPos = posToMoveCar;
    }

    public void Release(Vector3 ThrowVelocity)
    {

    }
    IEnumerator MoveCarToDesiredPos()
    {
        while(true)
        {
            CarTransform.localPosition = new Vector3(Mathf.Lerp(CarTransform.localPosition.x,desiredPos.x, MoveCarLaneSpeed *Time.deltaTime) ,CarTransform.localPosition.y, CarTransform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
