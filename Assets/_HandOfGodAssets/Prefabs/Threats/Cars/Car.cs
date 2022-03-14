using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Threat, IDragable
{
    [SerializeField] float LaneChangeSpeed = 10f;
    [SerializeField] Transform CarPivot;
    [SerializeField] Transform[] Lanes;
    [SerializeField] LayerMask LaneDetectionLaymask;
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] Transform ExplosionSpawn;
    GameObject DragRef;
    Transform destinationLane;
    Quaternion startRotation;

    void Start()
    {
        if(!HasAvaliableLane())
        {
            Destroy(gameObject);
            return;
        }

        DragRef = new GameObject($"{gameObject.name} drag ref");
        PickRandomLane();
    }
    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        Debug.Log("Car being grab");
        DragRef.transform.position = grabPoint;
        DragRef.transform.parent = grabber.transform;
    }

    public override void Init()
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
        Transform walkManTrans = GameplayStatics.GetWalkmanTransform();
        Vector3 SpawnRotUp = -walkManTrans.up;
        Vector3 SpawnRotForward = walkManTrans.forward;


        Quaternion SpawnRot = Quaternion.LookRotation(SpawnRotForward,SpawnRotUp);
        orbitMovementComp.SetRotation(SpawnRot);
        startRotation = SpawnRot;
    }

    private void PickRandomLane()
    {
        if(Lanes.Length == 00)
        {
            return;
        }
        int randomIndex = Random.Range(0, Lanes.Length);
        if(CanMoveToLane(Lanes[randomIndex]))
        {
            destinationLane = Lanes[randomIndex];
            return;
        }
        PickRandomLane();
    }
    bool HasAvaliableLane()
    {
        foreach(var lane in Lanes)
        {
            if(CanMoveToLane(lane))
            {
                return true;
            }
        }
        return false;
    }
    void Update()
    {
        if(DragRef.transform.parent != null && Lanes.Length != 0)
        {
            Transform closestLane = Lanes[0];
            float closestDistance = Vector3.Distance(DragRef.transform.position, closestLane.position);
            foreach(var lane in Lanes)
            {
                float distance = Vector3.Distance(DragRef.transform.position,lane.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestLane = lane;
                }
            }

            if(CanMoveToLane(closestLane))
            {
                destinationLane = closestLane;
            }
        }

        float lerpAlpha = Mathf.Clamp(Time.deltaTime * LaneChangeSpeed,0f,1f);
        CarPivot.rotation = Quaternion.Lerp(CarPivot.rotation, destinationLane.parent.rotation, lerpAlpha);

        //if(Quaternion.Angle(startRotation,transform.rotation) <= 0)
       // {
          //  BlowUp();
        //}
    }

    public void Release(Vector3 ThrowVelocity)
    {
        DragRef.transform.parent = null;
    }

    bool CanMoveToLane(Transform lane)
    {
        BoxCollider CarCollider = GetComponentInChildren<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(lane.position, CarCollider.size/2, lane.rotation, LaneDetectionLaymask);

        foreach(Collider col in colliders)
        {
            if(col.gameObject != CarCollider.gameObject)
            {
                return false;
            }
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        BoxCollider CarCollider = GetComponentInChildren<BoxCollider>();
        foreach(var lane in Lanes)
        {
            if(!CanMoveToLane(lane))
            {
                Gizmos.DrawCube(lane.position, CarCollider.size);
            }
        }
    }

    public void BlowUp()
    {
        GameObject newEffect = Instantiate(ExplosionEffect, ExplosionSpawn);
        newEffect.transform.parent = null;
        Destroy(gameObject);
    }
}
