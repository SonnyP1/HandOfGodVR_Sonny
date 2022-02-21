//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Prefabs/Player/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""XRRightController"",
            ""id"": ""9cd9c337-236f-465f-98e4-5fe9226fd843"",
            ""actions"": [
                {
                    ""name"": ""position"",
                    ""type"": ""Value"",
                    ""id"": ""b236dec2-b493-4a8c-acfd-08f9e5f8d4ee"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""rotation"",
                    ""type"": ""Value"",
                    ""id"": ""3e34ed57-e8aa-4baa-a2c8-e90c5b7b6819"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TriggerAxis"",
                    ""type"": ""Value"",
                    ""id"": ""7e598f47-46ba-41c2-8eae-aec576b2442c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GripAxis"",
                    ""type"": ""Value"",
                    ""id"": ""bb1975ef-fe1e-4271-aafa-e64cf09bd716"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""612436d9-34f2-4d04-b30d-97b90ce4a171"",
                    ""path"": ""<XRController>{RightHand}/pointerPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f803e5f7-18a1-4fc2-957d-6e69db28dcc8"",
                    ""path"": ""<XRController>{RightHand}/pointerRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94ba05bd-647e-4cca-b5d9-72264298ee91"",
                    ""path"": ""<XRController>{RightHand}/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""262a708c-83da-4460-9d75-225580630aef"",
                    ""path"": ""<XRController>{RightHand}/grip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // XRRightController
        m_XRRightController = asset.FindActionMap("XRRightController", throwIfNotFound: true);
        m_XRRightController_position = m_XRRightController.FindAction("position", throwIfNotFound: true);
        m_XRRightController_rotation = m_XRRightController.FindAction("rotation", throwIfNotFound: true);
        m_XRRightController_TriggerAxis = m_XRRightController.FindAction("TriggerAxis", throwIfNotFound: true);
        m_XRRightController_GripAxis = m_XRRightController.FindAction("GripAxis", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // XRRightController
    private readonly InputActionMap m_XRRightController;
    private IXRRightControllerActions m_XRRightControllerActionsCallbackInterface;
    private readonly InputAction m_XRRightController_position;
    private readonly InputAction m_XRRightController_rotation;
    private readonly InputAction m_XRRightController_TriggerAxis;
    private readonly InputAction m_XRRightController_GripAxis;
    public struct XRRightControllerActions
    {
        private @PlayerInput m_Wrapper;
        public XRRightControllerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @position => m_Wrapper.m_XRRightController_position;
        public InputAction @rotation => m_Wrapper.m_XRRightController_rotation;
        public InputAction @TriggerAxis => m_Wrapper.m_XRRightController_TriggerAxis;
        public InputAction @GripAxis => m_Wrapper.m_XRRightController_GripAxis;
        public InputActionMap Get() { return m_Wrapper.m_XRRightController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XRRightControllerActions set) { return set.Get(); }
        public void SetCallbacks(IXRRightControllerActions instance)
        {
            if (m_Wrapper.m_XRRightControllerActionsCallbackInterface != null)
            {
                @position.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnPosition;
                @position.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnPosition;
                @position.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnPosition;
                @rotation.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnRotation;
                @rotation.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnRotation;
                @rotation.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnRotation;
                @TriggerAxis.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerAxis;
                @TriggerAxis.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerAxis;
                @TriggerAxis.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerAxis;
                @GripAxis.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnGripAxis;
                @GripAxis.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnGripAxis;
                @GripAxis.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnGripAxis;
            }
            m_Wrapper.m_XRRightControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @position.started += instance.OnPosition;
                @position.performed += instance.OnPosition;
                @position.canceled += instance.OnPosition;
                @rotation.started += instance.OnRotation;
                @rotation.performed += instance.OnRotation;
                @rotation.canceled += instance.OnRotation;
                @TriggerAxis.started += instance.OnTriggerAxis;
                @TriggerAxis.performed += instance.OnTriggerAxis;
                @TriggerAxis.canceled += instance.OnTriggerAxis;
                @GripAxis.started += instance.OnGripAxis;
                @GripAxis.performed += instance.OnGripAxis;
                @GripAxis.canceled += instance.OnGripAxis;
            }
        }
    }
    public XRRightControllerActions @XRRightController => new XRRightControllerActions(this);
    public interface IXRRightControllerActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnTriggerAxis(InputAction.CallbackContext context);
        void OnGripAxis(InputAction.CallbackContext context);
    }
}
