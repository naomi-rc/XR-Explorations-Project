using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider teleportationProvider;    

    private InputAction _thumbstick;
    private bool _isActive;

    void Start()
    {
        var activateMode = playerControls.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activateMode.performed += OnTeleportActivate;
        activateMode.Enable();

        var cancelMode = playerControls.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancelMode.performed += OnTeleportCancel;
        cancelMode.Enable();

        _thumbstick = playerControls.FindActionMap("XRI LeftHand").FindAction("Move");
        _thumbstick.Enable();

        rayInteractor.enabled = false;
    }

    void Update()
    {
        if (!_isActive)
            return;
        if (_thumbstick.triggered) //player is still pushing thumbstick
            return; 
        if(!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
        };

        teleportationProvider.QueueTeleportRequest(request);
        rayInteractor.enabled = false;
        _isActive = false;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = false;
    }
}
