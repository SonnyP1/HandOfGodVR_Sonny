using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public interface XRControllerInterface
{
    public Vector2 GetPointerScreenPosition();
}

public class XRInputModule : PointerInputModule
{
    PlayerInput _playerInput;

    PointerEventData _leftControllerData;
    PointerEventData _rightControllerData;

    [SerializeField] GameObject LeftController;
    XRControllerInterface _leftControllerInterface;

    [SerializeField] GameObject RightController;
    XRControllerInterface _rightControllerInterface;


    //old input system
    public override void Process()
    {
        //processes input and fire out event through the event system
        //behaves like a update function
    }

    protected override void Awake()
    {
        if(_playerInput == null)
        {
            _playerInput = new PlayerInput();
        }
    }

    protected override void Start()
    {
        base.Start();

        _leftControllerInterface = LeftController.GetComponent<XRControllerInterface>();
        _leftControllerData = new PointerEventData(eventSystem);

        _rightControllerInterface = RightController.GetComponent<XRControllerInterface>();
        _rightControllerData = new PointerEventData(eventSystem);

        _playerInput.XRRightController.TriggerBtn.performed += RightTriggerPressed;
        _playerInput.XRRightController.TriggerBtn.canceled += RightTriggerCanceled;

        _playerInput.XRLeftController.TriggerBtn.performed += LeftTriggerPressed;
        _playerInput.XRLeftController.TriggerBtn.canceled += LeftTriggerCanceled;
    }

    private void LeftTriggerCanceled(InputAction.CallbackContext obj)
    {
        OnTriggerRelease(_leftControllerInterface, _leftControllerData);
    }

    private void LeftTriggerPressed(InputAction.CallbackContext obj)
    {
        OnTriggerPressed(_leftControllerInterface, _leftControllerData);
    }

    private void RightTriggerCanceled(InputAction.CallbackContext obj)
    {
        OnTriggerRelease(_rightControllerInterface, _rightControllerData);
    }

    private void RightTriggerPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnTriggerPressed(_rightControllerInterface,_rightControllerData);
    }

    private void OnTriggerRelease(XRControllerInterface xRControllerInterface, PointerEventData eventData)
    {
        if (xRControllerInterface == null || eventData == null)
        {
            return;
        }

        eventData.position = xRControllerInterface.GetPointerScreenPosition();
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResults);
        eventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);

        ExecuteEvents.Execute(eventData.pointerPress,eventData,ExecuteEvents.pointerUpHandler);
    }
    private void OnTriggerPressed(XRControllerInterface xRControllerInterface,PointerEventData eventData)
    {
        if (xRControllerInterface == null || eventData == null)
        {
            return;
        }

        eventData.position = xRControllerInterface.GetPointerScreenPosition();
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResults);
        eventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);

        GameObject pointerDownObject = ExecuteEvents.GetEventHandler<IPointerDownHandler>(eventData.pointerCurrentRaycast.gameObject);
        if (pointerDownObject != null)
        {
            ExecuteEvents.Execute(pointerDownObject, eventData, ExecuteEvents.pointerDownHandler);
            eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
            eventData.eligibleForClick = true;
            eventData.pointerPress = pointerDownObject;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(_playerInput != null)
        {
            _playerInput.Enable();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_playerInput != null)
        {
            _playerInput.Disable();
        }
    }

}
