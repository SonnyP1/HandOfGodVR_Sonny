using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiner : MonoBehaviour , IDragable
{
    [SerializeField] [Range(0,1)] float Damping = 1f;
    [SerializeField] float SpinSpeed = 20f;
    [SerializeField] Transform ObjectSpined;
    [SerializeField] Transform SpinOffset;
    GameObject _lookRef;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        OrbitMovementComp orbitMovementComp =  GetComponent<OrbitMovementComp>();
        if (orbitMovementComp)
        {
            orbitMovementComp.enabled = false;
            HailCloud cloud = GetComponent<HailCloud>();
            if(cloud != null)
            {
                cloud.StopBlowUpTimer();
            }
        }
        _lookRef.transform.position = grabPoint;
        _lookRef.transform.parent = grabber.transform;
        SpinOffset.LookAt(_lookRef.transform,Vector3.up);
        ObjectSpined.transform.parent = SpinOffset;
    }

    public void Release(Vector3 ThrowVelocity)
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
        if (orbitMovementComp)
        {
            orbitMovementComp.enabled = true;
            HailCloud cloud = GetComponent<HailCloud>();
            if (cloud != null)
            {
                cloud.StartBlowUpTimer();
            }
        }
        ObjectSpined.parent = transform;
        _lookRef.transform.parent = null;
    }

    void Start()
    {
        _lookRef = new GameObject($"{gameObject.name} look ref");
        GameObject spinOffset = GameObject.FindGameObjectWithTag("SpinOffset");
        if(spinOffset)
        {
            SpinOffset = spinOffset.transform;
        }
    }

    void Update()
    {
        if(_lookRef.transform.parent)
        {
            Quaternion goalRotation = Quaternion.LookRotation((_lookRef.transform.position - SpinOffset.position).normalized,Vector3.up);
            float lerpAlpha = Mathf.Clamp((1-Damping) * SpinSpeed * Time.deltaTime,0,1f);
            SpinOffset.rotation =  Quaternion.Slerp(SpinOffset.transform.rotation, goalRotation, lerpAlpha);
        }
    }


}
