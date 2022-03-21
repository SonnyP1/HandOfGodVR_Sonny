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
        _lookRef.transform.position = grabPoint;
        _lookRef.transform.parent = grabber.transform;
        SpinOffset.LookAt(_lookRef.transform,Vector3.up);
        ObjectSpined.transform.parent = SpinOffset;
    }

    public void Release(Vector3 ThrowVelocity)
    {
        ObjectSpined.parent = transform;
        _lookRef.transform.parent = null;
    }

    void Start()
    {
        _lookRef = new GameObject($"{gameObject.name} look ref");
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
