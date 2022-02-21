using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    [SerializeField] Animator HandAnimator;
    [SerializeField] LineRenderer LineRender;
    private float _triggerInput;
    private float _gripInput;
    private Vector3 _pointerLoc;
    public void UpdateLocalPosition(Vector3 location)
    {
        _pointerLoc = location;
        transform.localPosition = _pointerLoc;
        LookForObjectToPickUp();
    }

    public void UpdateLocalRotation(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    internal void UpdateTriggerValue(float newValue)
    {
        _triggerInput = newValue;
        HandAnimator.SetFloat("Trigger", _triggerInput);
    }

    internal void UpdateGripValue(float newValue)
    {
        _gripInput = newValue;
        HandAnimator.SetFloat("Grip", _gripInput);
    }

    private void LookForObjectToPickUp()
    {
        //Ray Cast to object if it hit something pick it up only do this if trigger value it 1
        if(_triggerInput > 0.9)
        {
            RaycastHit hit;
            if(Physics.Raycast(_pointerLoc,transform.forward,out hit, 10f,LayerMask.NameToLayer("Pickable")))
            {
                LineRender.SetPosition(1,hit.point);
                Debug.DrawRay(_pointerLoc,transform.forward*hit.distance,Color.red);
            }
        }
    }
}
