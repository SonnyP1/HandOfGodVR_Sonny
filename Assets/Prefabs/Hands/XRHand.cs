using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    [SerializeField] LazerPoint lazerPointer;
    [SerializeField] Animator HandAnimator;
    [SerializeField] GameObject GrabPoint;
    [SerializeField] Transform ThrowVelocityRefPoint;
    IDragable dragableObjectInHand;


    [Header("Values")]
    Vector3 _velocity;
    Vector3 _oldPos;
    Vector3 PositionOneSecondBefore;
    private float _triggerInput;
    private float _gripInput;
    private Vector3 _pointerLoc;

    IEnumerator CalculateAverageSpeed()
    {
        while(true)
        {
            _velocity = (ThrowVelocityRefPoint.position - _oldPos) / 0.1f;
            _oldPos = ThrowVelocityRefPoint.position;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Start()
    {
        PositionOneSecondBefore = transform.position;
        StartCoroutine(CalculateAverageSpeed());
    }
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

    internal void TriggerButtonPressed()
    {
        if(lazerPointer != null && lazerPointer.GetFocusedObject(out GameObject objectInFocus,out Vector3 contactPoint))
        {
            IDragable objectAsDragable = objectInFocus.GetComponent<IDragable>();
            if(objectAsDragable == null)
            {
                objectAsDragable = objectInFocus.GetComponentInParent<IDragable>();
            }

            if(objectAsDragable != null)
            {
                objectAsDragable.Grab(GrabPoint, contactPoint);
                dragableObjectInHand = objectAsDragable;
            }
        }
    }

    internal void UpdateStickValue(Vector2 stickInput)
    {
        if (stickInput.x > 0.5)
        {
            FindObjectOfType<Earth>().RotateRight();
        }
        else if (stickInput.x < -0.5f)
        {
            FindObjectOfType<Earth>().RotateLeft();
        }
    }

    internal void TriggerButtonRelease()
    {
        if(dragableObjectInHand != null)
        {
            dragableObjectInHand.Release(_velocity);
        }
    }
}
