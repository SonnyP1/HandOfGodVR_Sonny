using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    [SerializeField] Animator HandAnimator;
    [SerializeField] LineRenderer LineRender;
    [SerializeField] Transform PickUpTransform;
    private float _triggerInput;
    private float _gripInput;
    private Vector3 _pointerLoc;
    private Transform _objectCurrentlyPickUpTransform;
    private bool _isObjectPickUp = false;
    public void UpdateLocalPosition(Vector3 location)
    {
        _pointerLoc = location;
        transform.localPosition = _pointerLoc;
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

    private void LookForObjectToPickUpAndIfTriggerActivePickUp()
    {
        PickUpObject();

        LineRender.SetPosition(0, _pointerLoc);
        RaycastHit hit;
        if (Physics.Raycast(_pointerLoc, transform.forward, out hit, Mathf.Infinity))
        {
            LineRender.SetPosition(1, hit.point);
            LineRender.enabled = true;
            if (_triggerInput > 0.9)
            {
                _objectCurrentlyPickUpTransform = hit.collider.GetComponent<Transform>();
                _isObjectPickUp = true;
            }
        }
        else
        {
            LineRender.enabled = false;
        }
    }

    private void PickUpObject()
    {
        if (_isObjectPickUp)
        {
            _objectCurrentlyPickUpTransform.position = PickUpTransform.position;
            if (_triggerInput < 0.5)
            {
                LineRender.enabled = false;
                _isObjectPickUp = false;
                _objectCurrentlyPickUpTransform = null;
            }
        }
    }

    private void Update()
    {
        LookForObjectToPickUpAndIfTriggerActivePickUp();
    }
}
