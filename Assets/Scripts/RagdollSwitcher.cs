using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSwitcher : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Animator[] animators;

    [ContextMenu("Set Up")]
    private void SetUp()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animators = GetComponentsInChildren<Animator>();
    }

    [ContextMenu("Remove Ragdoll")]
    private void RemoveRagdoll()
    {
        foreach(var r in rigidbodies)
        {
            if(r.TryGetComponent<Joint>(out var joint))
            {
                DestroyImmediate(joint);
            }
            var collider = r.GetComponent<Collider>();
            DestroyImmediate(collider);
        }
        rigidbodies = null;
    }

    [ContextMenu("Turn Off Animator")]
    private void TurnOffAnimator()
    {
        foreach(Animator a in animators)
        {
            a.enabled = false; 
        }
    }
}
