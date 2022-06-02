//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/ControlsMapping.inputactions
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

public partial class @ControlsMapping : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlsMapping()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlsMapping"",
    ""maps"": [
        {
            ""name"": ""caminando"",
            ""id"": ""003e714f-ea69-48e0-8560-097bfa0bfc7b"",
            ""actions"": [
                {
                    ""name"": ""Caminar"",
                    ""type"": ""Value"",
                    ""id"": ""0c716312-44cf-438c-805e-5369478081ba"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Agarrar"",
                    ""type"": ""Button"",
                    ""id"": ""678be993-a8bc-4010-bc81-2567a837c37a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mirar"",
                    ""type"": ""Value"",
                    ""id"": ""e8fb8100-f6f5-481b-a895-8368154bf32e"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotar"",
                    ""type"": ""Button"",
                    ""id"": ""4b9b1f4c-7cf1-4c66-8168-8f052cccae47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9936790c-fcaf-4f64-98ac-611f74f8a986"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Agarrar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38920ff2-8968-462e-855a-c786bb739eb1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Agarrar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a5004bf-b621-4efc-95e6-80aa08e3cd74"",
                    ""path"": ""<HID::ShanWan PC/PS3/Android>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Agarrar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""ab5b9e95-e7c0-47b7-9624-6be5e63a1206"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Caminar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""695ca831-b48e-4bcf-b201-10b2bb27069e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7aa2c35d-9cad-4d27-9068-46a32bebd807"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""716986db-9158-4a90-b472-864c137f5808"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aeb9bab9-5da8-44bc-bfc8-1ae2ba084282"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b2f98a47-cbad-414e-af36-8a584ccef6a5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca614798-7e3e-4a3b-8235-cce30f8cf8cc"",
                    ""path"": ""<HID::ShanWan PC/PS3/Android>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Caminar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73c1c9b8-c9af-494c-a8a0-52baa2511f1b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""esquema"",
                    ""action"": ""Mirar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0330e8df-c06d-4645-85a6-bcc34473db7c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1437b22a-804d-401f-a6f6-6767d859f3ec"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""esquema"",
            ""bindingGroup"": ""esquema"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<HID::ShanWan PC/PS3/Android>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // caminando
        m_caminando = asset.FindActionMap("caminando", throwIfNotFound: true);
        m_caminando_Caminar = m_caminando.FindAction("Caminar", throwIfNotFound: true);
        m_caminando_Agarrar = m_caminando.FindAction("Agarrar", throwIfNotFound: true);
        m_caminando_Mirar = m_caminando.FindAction("Mirar", throwIfNotFound: true);
        m_caminando_Rotar = m_caminando.FindAction("Rotar", throwIfNotFound: true);
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

    // caminando
    private readonly InputActionMap m_caminando;
    private ICaminandoActions m_CaminandoActionsCallbackInterface;
    private readonly InputAction m_caminando_Caminar;
    private readonly InputAction m_caminando_Agarrar;
    private readonly InputAction m_caminando_Mirar;
    private readonly InputAction m_caminando_Rotar;
    public struct CaminandoActions
    {
        private @ControlsMapping m_Wrapper;
        public CaminandoActions(@ControlsMapping wrapper) { m_Wrapper = wrapper; }
        public InputAction @Caminar => m_Wrapper.m_caminando_Caminar;
        public InputAction @Agarrar => m_Wrapper.m_caminando_Agarrar;
        public InputAction @Mirar => m_Wrapper.m_caminando_Mirar;
        public InputAction @Rotar => m_Wrapper.m_caminando_Rotar;
        public InputActionMap Get() { return m_Wrapper.m_caminando; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CaminandoActions set) { return set.Get(); }
        public void SetCallbacks(ICaminandoActions instance)
        {
            if (m_Wrapper.m_CaminandoActionsCallbackInterface != null)
            {
                @Caminar.started -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnCaminar;
                @Caminar.performed -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnCaminar;
                @Caminar.canceled -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnCaminar;
                @Agarrar.started -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnAgarrar;
                @Agarrar.performed -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnAgarrar;
                @Agarrar.canceled -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnAgarrar;
                @Mirar.started -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnMirar;
                @Mirar.performed -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnMirar;
                @Mirar.canceled -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnMirar;
                @Rotar.started -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnRotar;
                @Rotar.performed -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnRotar;
                @Rotar.canceled -= m_Wrapper.m_CaminandoActionsCallbackInterface.OnRotar;
            }
            m_Wrapper.m_CaminandoActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Caminar.started += instance.OnCaminar;
                @Caminar.performed += instance.OnCaminar;
                @Caminar.canceled += instance.OnCaminar;
                @Agarrar.started += instance.OnAgarrar;
                @Agarrar.performed += instance.OnAgarrar;
                @Agarrar.canceled += instance.OnAgarrar;
                @Mirar.started += instance.OnMirar;
                @Mirar.performed += instance.OnMirar;
                @Mirar.canceled += instance.OnMirar;
                @Rotar.started += instance.OnRotar;
                @Rotar.performed += instance.OnRotar;
                @Rotar.canceled += instance.OnRotar;
            }
        }
    }
    public CaminandoActions @caminando => new CaminandoActions(this);
    private int m_esquemaSchemeIndex = -1;
    public InputControlScheme esquemaScheme
    {
        get
        {
            if (m_esquemaSchemeIndex == -1) m_esquemaSchemeIndex = asset.FindControlSchemeIndex("esquema");
            return asset.controlSchemes[m_esquemaSchemeIndex];
        }
    }
    public interface ICaminandoActions
    {
        void OnCaminar(InputAction.CallbackContext context);
        void OnAgarrar(InputAction.CallbackContext context);
        void OnMirar(InputAction.CallbackContext context);
        void OnRotar(InputAction.CallbackContext context);
    }
}
