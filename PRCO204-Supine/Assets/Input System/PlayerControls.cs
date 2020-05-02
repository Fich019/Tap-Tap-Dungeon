// GENERATED AUTOMATICALLY FROM 'Assets/Input System/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""c5b59e3c-15f4-47e7-aafa-910bf83c8023"",
            ""actions"": [
                {
                    ""name"": ""PlayerMoveX"",
                    ""type"": ""Value"",
                    ""id"": ""0fbcc303-8bb6-4f4a-86c1-8209fae554ac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerMoveZ"",
                    ""type"": ""Value"",
                    ""id"": ""092d8343-2340-4dcc-b88e-20bb99cb6298"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Value"",
                    ""id"": ""f6ba4a6e-7970-44ea-9484-c5d8d78789a9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerRotY"",
                    ""type"": ""Value"",
                    ""id"": ""09a099f2-9125-4502-965d-47ebaf48057d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerAttack"",
                    ""type"": ""Value"",
                    ""id"": ""7abd3f0c-015b-41cc-bad8-b461b23d84a8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerPickUpWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""8964473c-a5c1-46c2-9a40-f461ef3692b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""219d7c91-30c0-43df-a2b8-a4cb87ab3034"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMoveX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""774ebc34-3468-4673-a371-e18d866f9bae"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMoveZ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20be2de1-9d4d-4bff-93d1-d24ce19a4c01"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c39a695c-5834-4e12-8a38-972c191bb33b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerRotY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97d5451b-1225-4637-930b-4254e5effc6c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c976105-abd6-49b0-ade7-309a3057e681"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerPickUpWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""978ed63d-88a6-41a9-9e61-cc96b7cb2314"",
            ""actions"": [
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Value"",
                    ""id"": ""3eee323e-abfb-4394-8bac-84f7bcf566b2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveAcross"",
                    ""type"": ""Value"",
                    ""id"": ""0660e1be-6d82-42d7-8bed-f844c28ad421"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""98121fd4-a9e1-4ead-932d-10cc5a66f877"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""04799e02-a9de-4f93-8809-3f01c513e24a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4d02551c-0b59-4b7a-972d-c38a0ebda362"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAcross"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd8e1147-09fa-49f6-a7cd-24e9c4414f2c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4804ff47-2ae2-42fd-8299-5b8bffa89ab1"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7bceaac-f85b-44e6-aa38-501613fcf649"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_PlayerMoveX = m_Gameplay.FindAction("PlayerMoveX", throwIfNotFound: true);
        m_Gameplay_PlayerMoveZ = m_Gameplay.FindAction("PlayerMoveZ", throwIfNotFound: true);
        m_Gameplay_CameraRotate = m_Gameplay.FindAction("CameraRotate", throwIfNotFound: true);
        m_Gameplay_PlayerRotY = m_Gameplay.FindAction("PlayerRotY", throwIfNotFound: true);
        m_Gameplay_PlayerAttack = m_Gameplay.FindAction("PlayerAttack", throwIfNotFound: true);
        m_Gameplay_PlayerPickUpWeapon = m_Gameplay.FindAction("PlayerPickUpWeapon", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_MoveDown = m_UI.FindAction("MoveDown", throwIfNotFound: true);
        m_UI_MoveAcross = m_UI.FindAction("MoveAcross", throwIfNotFound: true);
        m_UI_Select = m_UI.FindAction("Select", throwIfNotFound: true);
        m_UI_Back = m_UI.FindAction("Back", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_PlayerMoveX;
    private readonly InputAction m_Gameplay_PlayerMoveZ;
    private readonly InputAction m_Gameplay_CameraRotate;
    private readonly InputAction m_Gameplay_PlayerRotY;
    private readonly InputAction m_Gameplay_PlayerAttack;
    private readonly InputAction m_Gameplay_PlayerPickUpWeapon;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMoveX => m_Wrapper.m_Gameplay_PlayerMoveX;
        public InputAction @PlayerMoveZ => m_Wrapper.m_Gameplay_PlayerMoveZ;
        public InputAction @CameraRotate => m_Wrapper.m_Gameplay_CameraRotate;
        public InputAction @PlayerRotY => m_Wrapper.m_Gameplay_PlayerRotY;
        public InputAction @PlayerAttack => m_Wrapper.m_Gameplay_PlayerAttack;
        public InputAction @PlayerPickUpWeapon => m_Wrapper.m_Gameplay_PlayerPickUpWeapon;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @PlayerMoveX.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMoveX;
                @PlayerMoveX.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMoveX;
                @PlayerMoveX.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMoveX;
                @PlayerMoveZ.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMoveZ;
                @PlayerMoveZ.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMoveZ;
                @PlayerMoveZ.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMoveZ;
                @CameraRotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraRotate;
                @PlayerRotY.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRotY;
                @PlayerRotY.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRotY;
                @PlayerRotY.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRotY;
                @PlayerAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerAttack;
                @PlayerAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerAttack;
                @PlayerAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerAttack;
                @PlayerPickUpWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerPickUpWeapon;
                @PlayerPickUpWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerPickUpWeapon;
                @PlayerPickUpWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerPickUpWeapon;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMoveX.started += instance.OnPlayerMoveX;
                @PlayerMoveX.performed += instance.OnPlayerMoveX;
                @PlayerMoveX.canceled += instance.OnPlayerMoveX;
                @PlayerMoveZ.started += instance.OnPlayerMoveZ;
                @PlayerMoveZ.performed += instance.OnPlayerMoveZ;
                @PlayerMoveZ.canceled += instance.OnPlayerMoveZ;
                @CameraRotate.started += instance.OnCameraRotate;
                @CameraRotate.performed += instance.OnCameraRotate;
                @CameraRotate.canceled += instance.OnCameraRotate;
                @PlayerRotY.started += instance.OnPlayerRotY;
                @PlayerRotY.performed += instance.OnPlayerRotY;
                @PlayerRotY.canceled += instance.OnPlayerRotY;
                @PlayerAttack.started += instance.OnPlayerAttack;
                @PlayerAttack.performed += instance.OnPlayerAttack;
                @PlayerAttack.canceled += instance.OnPlayerAttack;
                @PlayerPickUpWeapon.started += instance.OnPlayerPickUpWeapon;
                @PlayerPickUpWeapon.performed += instance.OnPlayerPickUpWeapon;
                @PlayerPickUpWeapon.canceled += instance.OnPlayerPickUpWeapon;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_MoveDown;
    private readonly InputAction m_UI_MoveAcross;
    private readonly InputAction m_UI_Select;
    private readonly InputAction m_UI_Back;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveDown => m_Wrapper.m_UI_MoveDown;
        public InputAction @MoveAcross => m_Wrapper.m_UI_MoveAcross;
        public InputAction @Select => m_Wrapper.m_UI_Select;
        public InputAction @Back => m_Wrapper.m_UI_Back;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @MoveDown.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveDown;
                @MoveAcross.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveAcross;
                @MoveAcross.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveAcross;
                @MoveAcross.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveAcross;
                @Select.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Back.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @MoveAcross.started += instance.OnMoveAcross;
                @MoveAcross.performed += instance.OnMoveAcross;
                @MoveAcross.canceled += instance.OnMoveAcross;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IGameplayActions
    {
        void OnPlayerMoveX(InputAction.CallbackContext context);
        void OnPlayerMoveZ(InputAction.CallbackContext context);
        void OnCameraRotate(InputAction.CallbackContext context);
        void OnPlayerRotY(InputAction.CallbackContext context);
        void OnPlayerAttack(InputAction.CallbackContext context);
        void OnPlayerPickUpWeapon(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMoveDown(InputAction.CallbackContext context);
        void OnMoveAcross(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
}
