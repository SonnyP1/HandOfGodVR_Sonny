using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputComponent : MonoBehaviour
{
    [SerializeField] XRHand RightHand;
    [SerializeField] XRHand LeftHand;
    PlayerInput _playerInput;


    private void Awake()
    {
        if(_playerInput == null)
        {
            _playerInput = new PlayerInput();
        }
    }
    private void OnEnable()
    {
        _playerInput?.Enable();
    }

    private void OnDisable()
    {
        _playerInput?.Disable();
    }

    private void Start()
    {
        if(_playerInput != null)
        {
            RightHandInputs();
            LeftHandInputs();
        }
    }


    private void RightHandInputs()
    {
        if (RightHand == null)
        {
            return;
        }
        _playerInput.XRRightController.position.performed += ctx => RightHand.UpdateLocalPosition(ctx.ReadValue<Vector3>());
        _playerInput.XRRightController.rotation.performed += ctx => RightHand.UpdateLocalRotation(ctx.ReadValue<Quaternion>());

        _playerInput.XRRightController.GripAxis.performed += ctx => RightHand.UpdateGripAxis(ctx.ReadValue<float>());

        _playerInput.XRRightController.TriggerAxis.performed += ctx => RightHand.UpdateTriggerValue(ctx.ReadValue<float>());
        _playerInput.XRRightController.TriggerBtn.performed += ctx => RightHand.TriggerButtonPressed();
        _playerInput.XRRightController.TriggerBtn.canceled += ctx => RightHand.TriggerButtonRelease();

        //_playerInput.XRRightController.AButton.performed += ctx => RightHand.PrimaryButtonPressed();
    }
    private void LeftHandInputs()
    {
        if(LeftHand == null)
        {
            return;
        }
        _playerInput.XRLeftController.position.performed += ctx => LeftHand.UpdateLocalPosition(ctx.ReadValue<Vector3>());
        _playerInput.XRLeftController.rotation.performed += ctx => LeftHand.UpdateLocalRotation(ctx.ReadValue<Quaternion>());

        _playerInput.XRLeftController.GripAxis.performed += ctx => LeftHand.UpdateGripAxis(ctx.ReadValue<float>());

        _playerInput.XRLeftController.TriggerBtn.performed += ctx => LeftHand.TriggerButtonPressed();
        _playerInput.XRLeftController.TriggerBtn.canceled += ctx => LeftHand.TriggerButtonRelease();
        _playerInput.XRLeftController.TriggerAxis.performed += ctx => LeftHand.UpdateTriggerValue(ctx.ReadValue<float>());

        _playerInput.XRLeftController.MenuButton.performed += ctx => LeftHand.MenuButtonPressed();
    }
}
