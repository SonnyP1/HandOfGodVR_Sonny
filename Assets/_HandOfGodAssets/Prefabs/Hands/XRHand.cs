using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideHand
{
    LeftHand,
    RightHand
}
public class XRHand : MonoBehaviour
{
    [SerializeField] LazerPoint lazerPointer;
    [SerializeField] Animator HandAnimator;
    [SerializeField] GameObject GrabPoint;
    [SerializeField] Transform ThrowVelocityRefPoint;
    [SerializeField] SideHand HandSide;
    IDragable dragableObjectInHand;


    [Header("Values")]
    Vector3 _velocity;
    Vector3 _oldPos;
    Vector3 PositionOneSecondBefore;
    private float _triggerInput;
    private float _gripInput;
    private Vector3 _pointerLoc;
    private bool _pauseBool = false;

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

    internal void PrimaryButtonPressed()
    {
        Debug.Log("Primary Button Pressed");
        if(HandSide.Equals(SideHand.RightHand))
        {
            if(lazerPointer !=null && lazerPointer.GetFocusedObject(out GameObject objectInFocus,out Vector3 contactPoint))
            {
                Debug.Log(objectInFocus);
            }
        }
    }

    internal void UpdateGripValue(float newValue)
    {
        _gripInput = newValue;
        HandAnimator.SetFloat("Grip", _gripInput);

        if(HandSide.Equals(SideHand.LeftHand))
        {
            if (_gripInput > 0.9f)
            {
                FindObjectOfType<Earth>().RotateBasedOnVelocity(_velocity.x);
                return;
            }
        }
    }

    internal void TriggerButtonPressed()
    {
        if (HandSide.Equals(SideHand.RightHand))
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
    }
    internal void MenuButtonPressed()
    {
        if(HandSide.Equals(SideHand.LeftHand))
        {
            if (!_pauseBool)
            {
                FindObjectOfType<PauseMenu>().VisibleSwitch();
                GameplayStatics.PauseGame();
                _pauseBool = true;
            }
            else if(_pauseBool)
            {
                FindObjectOfType<PauseMenu>().VisibleSwitch();
                GameplayStatics.UnPauseGame();
                _pauseBool = false;
            }
        }
    }

    internal void UpdateStickValue(Vector2 stickInput)
    {
        if(HandSide.Equals(SideHand.LeftHand) && _gripInput < 0.1f)
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
    }

    internal void TriggerButtonRelease()
    {
        if(HandSide.Equals(SideHand.RightHand))
        {
            if(dragableObjectInHand != null)
            {
                dragableObjectInHand.Release(_velocity);
            }
        }
    }
}