using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float animationSpeed;

    // Animation
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float _gripTarget;
    private float _triggerTarget;
    private float _gripCurrent;
    private float _triggerCurrent;
    private string _animatorGripParam = "Grip";
    private string _animatorTriggerParam = "Trigger";

    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Vector3 positionOffset;
    private Transform _followTarget;
    private Rigidbody _rigidBody;
    

    void Start()
    {
        // Animation
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        // Physics Movement
        _followTarget = followObject.transform;
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidBody.mass = 20f;
        _rigidBody.position = _followTarget.position;
        _rigidBody.rotation = _followTarget.rotation;

    }

    void Update()
    {
        AnimateHand();
        PhysicsMovement();
    }

    internal void SetTrigger(float value)
    {
        _gripTarget = value;

    }

    internal void SetGrip(float value)
    {
        _triggerTarget = value;
    }

    void AnimateHand()
    {
        if(_gripCurrent != _gripTarget)
        {
            _gripCurrent = Mathf.MoveTowards(_gripCurrent, _gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(_animatorGripParam, _gripCurrent);
        }

        if (_triggerCurrent != _triggerTarget)
        {
            _triggerCurrent = Mathf.MoveTowards(_triggerCurrent, _triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(_animatorTriggerParam, _triggerCurrent);
        }
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    }

    void PhysicsMovement()
    {
        // Update position
        var positionWithOffset = _followTarget.position + positionOffset;
        var distance = Vector3.Distance(positionWithOffset, transform.position); // difference between current and target positions
        _rigidBody.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Update rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var quat = rotationWithOffset * Quaternion.Inverse(_rigidBody.rotation); // difference between current and target angles
        quat.ToAngleAxis(out float angle, out Vector3 axis);
        _rigidBody.angularVelocity = axis * angle * Mathf.Deg2Rad * rotateSpeed; // angular velocity to reach target rotation
    }
}
