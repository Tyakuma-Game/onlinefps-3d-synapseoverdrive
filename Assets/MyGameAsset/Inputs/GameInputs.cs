//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/MyGameAsset/Inputs/GameInputs.inputactions
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

public partial class @GameInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""18460cc0-6bfb-485e-9e8c-a4a853bea44a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""049196bd-57ca-4bd9-bd4e-2ab109a2cfd1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a985fd72-b3b9-4001-8c70-27df283fb7ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""78aa6cd9-accc-4b40-9d4f-f5e120e6b10f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""71dfe6b0-5873-4b29-a4bc-a3a43c42468f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5020fb44-5f38-41af-bcfa-f9e9621f928e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b9e0645-ed08-43f8-9793-2b0827860580"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b27117c-ba1b-4295-939a-b8f71cab1e69"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fbc4aa7-ea5a-4893-a3cf-02bed9fa2a73"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6e4beb66-f0c9-4047-a675-f7bfcc70b1bc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9389a2be-6e2e-4263-aa8c-6933dca553b2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d2aed238-f937-4fd7-ae12-b541cf5aaeef"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""334b97f5-9ae4-48e6-8eba-da6c7de3e8b8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d775b68-8ea8-46b5-9cd3-907c602b6d40"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Gun"",
            ""id"": ""9130d120-103c-4ec5-9a7a-c869e5dcb2fb"",
            ""actions"": [
                {
                    ""name"": ""Shot"",
                    ""type"": ""Button"",
                    ""id"": ""2d23d80f-8040-4187-9a5d-3f58720b8f98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""60827d95-533c-4a88-a429-c837cb6658d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponChange"",
                    ""type"": ""Button"",
                    ""id"": ""cf9e95e9-0821-45cf-b844-ba2b85bec878"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9fac4631-3761-4439-82cb-9d750357891d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c40a026c-b163-4645-9a74-149016094f9d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b79f47a5-80c1-4219-a7da-ada8a3f66611"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""580f7da9-b8ae-4d28-94e6-b248c4a95bfd"",
            ""actions"": [
                {
                    ""name"": ""ChangedCursorlock"",
                    ""type"": ""Button"",
                    ""id"": ""1dd7b4a4-47f5-49c9-b2ed-7fb025c1f40f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""77a2cae2-0dda-46c6-a2c3-5338259c8b6f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangedCursorlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        // Gun
        m_Gun = asset.FindActionMap("Gun", throwIfNotFound: true);
        m_Gun_Shot = m_Gun.FindAction("Shot", throwIfNotFound: true);
        m_Gun_Zoom = m_Gun.FindAction("Zoom", throwIfNotFound: true);
        m_Gun_WeaponChange = m_Gun.FindAction("WeaponChange", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_ChangedCursorlock = m_Menu.FindAction("ChangedCursorlock", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Look;
    public struct PlayerActions
    {
        private @GameInputs m_Wrapper;
        public PlayerActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Gun
    private readonly InputActionMap m_Gun;
    private List<IGunActions> m_GunActionsCallbackInterfaces = new List<IGunActions>();
    private readonly InputAction m_Gun_Shot;
    private readonly InputAction m_Gun_Zoom;
    private readonly InputAction m_Gun_WeaponChange;
    public struct GunActions
    {
        private @GameInputs m_Wrapper;
        public GunActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shot => m_Wrapper.m_Gun_Shot;
        public InputAction @Zoom => m_Wrapper.m_Gun_Zoom;
        public InputAction @WeaponChange => m_Wrapper.m_Gun_WeaponChange;
        public InputActionMap Get() { return m_Wrapper.m_Gun; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GunActions set) { return set.Get(); }
        public void AddCallbacks(IGunActions instance)
        {
            if (instance == null || m_Wrapper.m_GunActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GunActionsCallbackInterfaces.Add(instance);
            @Shot.started += instance.OnShot;
            @Shot.performed += instance.OnShot;
            @Shot.canceled += instance.OnShot;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @WeaponChange.started += instance.OnWeaponChange;
            @WeaponChange.performed += instance.OnWeaponChange;
            @WeaponChange.canceled += instance.OnWeaponChange;
        }

        private void UnregisterCallbacks(IGunActions instance)
        {
            @Shot.started -= instance.OnShot;
            @Shot.performed -= instance.OnShot;
            @Shot.canceled -= instance.OnShot;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @WeaponChange.started -= instance.OnWeaponChange;
            @WeaponChange.performed -= instance.OnWeaponChange;
            @WeaponChange.canceled -= instance.OnWeaponChange;
        }

        public void RemoveCallbacks(IGunActions instance)
        {
            if (m_Wrapper.m_GunActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGunActions instance)
        {
            foreach (var item in m_Wrapper.m_GunActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GunActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GunActions @Gun => new GunActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_ChangedCursorlock;
    public struct MenuActions
    {
        private @GameInputs m_Wrapper;
        public MenuActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangedCursorlock => m_Wrapper.m_Menu_ChangedCursorlock;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @ChangedCursorlock.started += instance.OnChangedCursorlock;
            @ChangedCursorlock.performed += instance.OnChangedCursorlock;
            @ChangedCursorlock.canceled += instance.OnChangedCursorlock;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @ChangedCursorlock.started -= instance.OnChangedCursorlock;
            @ChangedCursorlock.performed -= instance.OnChangedCursorlock;
            @ChangedCursorlock.canceled -= instance.OnChangedCursorlock;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
    public interface IGunActions
    {
        void OnShot(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnWeaponChange(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnChangedCursorlock(InputAction.CallbackContext context);
    }
}