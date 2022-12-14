//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""3abc4c70-4c1d-46cd-a2e1-c085297007b4"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""858d83fb-af04-47f4-8edb-bef65b7f6b3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""9515615b-d99c-4288-8837-58f563037a4d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""8a35b0b0-5953-47de-997e-ed65112efde7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""8bc180b4-4457-4225-acfe-34fc1cf44d88"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""d1aaef6a-ffd0-4b88-9d24-3a0b0b073080"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FullscreenToggle"",
                    ""type"": ""Button"",
                    ""id"": ""088086e8-bfcb-4732-9bb2-67bdac61f0ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0e6955aa-cfb1-4803-b31c-bc137a0473c2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""b2e19a98-b0f5-4817-920c-e6adf5e6e6a7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""db5f8d78-3a75-4741-ba0a-4a5ff4552505"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9fbf0057-3ee4-420a-916e-493f26167387"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b123a691-58d1-4acf-a55f-4128a8acb89d"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcccfe7a-7c24-49d7-a366-5f920d7f0377"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7141fd07-2bf3-4329-85e4-1e5f42f9b140"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7d9b9b1-242c-49f5-8fa0-14b67f15540f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FullscreenToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Jump = m_Default.FindAction("Jump", throwIfNotFound: true);
        m_Default_Move = m_Default.FindAction("Move", throwIfNotFound: true);
        m_Default_Dash = m_Default.FindAction("Dash", throwIfNotFound: true);
        m_Default_Pause = m_Default.FindAction("Pause", throwIfNotFound: true);
        m_Default_Attack = m_Default.FindAction("Attack", throwIfNotFound: true);
        m_Default_FullscreenToggle = m_Default.FindAction("FullscreenToggle", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_Jump;
    private readonly InputAction m_Default_Move;
    private readonly InputAction m_Default_Dash;
    private readonly InputAction m_Default_Pause;
    private readonly InputAction m_Default_Attack;
    private readonly InputAction m_Default_FullscreenToggle;
    public struct DefaultActions
    {
        private @PlayerInputActions m_Wrapper;
        public DefaultActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Default_Jump;
        public InputAction @Move => m_Wrapper.m_Default_Move;
        public InputAction @Dash => m_Wrapper.m_Default_Dash;
        public InputAction @Pause => m_Wrapper.m_Default_Pause;
        public InputAction @Attack => m_Wrapper.m_Default_Attack;
        public InputAction @FullscreenToggle => m_Wrapper.m_Default_FullscreenToggle;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Dash.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDash;
                @Pause.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnPause;
                @Attack.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnAttack;
                @FullscreenToggle.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnFullscreenToggle;
                @FullscreenToggle.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnFullscreenToggle;
                @FullscreenToggle.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnFullscreenToggle;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @FullscreenToggle.started += instance.OnFullscreenToggle;
                @FullscreenToggle.performed += instance.OnFullscreenToggle;
                @FullscreenToggle.canceled += instance.OnFullscreenToggle;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnFullscreenToggle(InputAction.CallbackContext context);
    }
}
