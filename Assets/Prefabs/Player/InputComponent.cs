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
        _playerInput?.Enable();
    }

    private void Start()
    {
        _playerInput.XRRightController.position.performed += ctx => RightHand.UpdateLocalPosition(ctx.ReadValue<Vector3>());
        _playerInput.XRRightController.rotation.performed += ctx => RightHand.UpdateLocalRotation(ctx.ReadValue<Quaternion>());
        _playerInput.XRRightController.TriggerAxis.performed += ctx => RightHand.UpdateTriggerValue(ctx.ReadValue<float>());
        _playerInput.XRRightController.GripAxis.performed += ctx => RightHand.UpdateGripValue(ctx.ReadValue<float>());
        _playerInput.XRRightController.TriggerBtn.performed += ctx => RightHand.TriggerButtonPressed();
        _playerInput.XRRightController.TriggerBtn.canceled += ctx => RightHand.TriggerButtonRelease();
    }
}
