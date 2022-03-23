using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideHand
{
    LeftHand,
    RightHand
}
public class XRHand : MonoBehaviour , XRControllerInterface
{
    [SerializeField] LazerPoint LazerPointer;
    [SerializeField] Animator HandAnimator;
    [SerializeField] GameObject GrabPoint;
    [SerializeField] Transform ThrowVelocityRefPoint;
    IDragable _dragableObjectInHand;
    //GameObject _objectCurrentlyDrag;
    //InventoryComponent _currentObjInventoryComp;
    //InventorySlot _currentInventorySlot;


    [Header("Values")]
    Vector3 _velocity;
    Vector3 _oldPos;
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
        StartCoroutine(CalculateAverageSpeed());
        GameplayStatics.UnPauseGame();
    }

    internal void UpdateGripAxis(float v)
    {
        _gripInput = v;
        HandAnimator.SetFloat("Grip", _gripInput);
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

    InventorySlot GetInventorySlot()
    {
        InventorySlot inventorySlot = null;
        GameObject currentOverUI = LazerPointer.GetCurrentPointingUI();
        if(currentOverUI != null)
        {
            inventorySlot = currentOverUI.GetComponent<InventorySlot>();
        }
        return inventorySlot;
    }

    InventoryComponent GetInventoryComp()
    {
        InventoryComponent inventoryComp = null;
        if (_dragableObjectInHand as UnityEngine.Object)
        {
            inventoryComp = _dragableObjectInHand.GetGameObject().GetComponent<InventoryComponent>();
        }
        return inventoryComp;
    }

    internal void TriggerButtonPressed()
    {
        if (LazerPointer != null)
        {
            InventorySlot inventorySlot = GetInventorySlot();
            if(inventorySlot && !inventorySlot.IsSlotEmpty())
            {
                InventoryComponent inventoryComp = GetInventoryComp();
                if (inventoryComp != null)
                {
                    inventorySlot.GrabItem();
                }   
            }
        }

        if(LazerPointer != null && LazerPointer.GetFocusedObject(out GameObject objectInFocus,out Vector3 contactPoint))
        {
            IDragable objectAsDragable = objectInFocus.GetComponent<IDragable>();
            if(objectAsDragable == null)
            {
                objectAsDragable = objectInFocus.GetComponentInParent<IDragable>();
            }

            if(objectAsDragable != null)
            {
                objectAsDragable.Grab(GrabPoint, contactPoint);
                _dragableObjectInHand = objectAsDragable;
            }

        }

    }
    internal void TriggerButtonRelease()
    {
        if (LazerPointer != null)
        {
            InventorySlot inventorySlot = GetInventorySlot();
            if (inventorySlot && inventorySlot.IsSlotEmpty())
            {
                InventoryComponent inventoryComp = GetInventoryComp();
                if (inventoryComp != null)
                {
                    inventorySlot.StoreItem(inventoryComp);
                    return;
                }
            }
        }

        if (_dragableObjectInHand as UnityEngine.Object)
        {
            _dragableObjectInHand.Release(_velocity);
        }
    }

    internal void MenuButtonPressed()
    {
        if (!_pauseBool)
        {
            FindObjectOfType<UIManager>().VisibleSwitch();
            GameplayStatics.PauseGame();
            _pauseBool = true;
        }
        else if(_pauseBool)
        {
            FindObjectOfType<UIManager>().VisibleSwitch();
            GameplayStatics.UnPauseGame();
            _pauseBool = false;
        }

    }

    public Vector2 GetPointerScreenPosition()
    {
        if(LazerPointer!=null)
        {
            return LazerPointer.GetPointerScreenPosition();
        }
        return Vector2.zero;
    }
}
