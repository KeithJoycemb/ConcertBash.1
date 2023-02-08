using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public Renderer nonPhysicalHand;
    public float showNonPhysicalHandDistance = 5f;
    private Collider[] handCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handCollider = GetComponentsInChildren<Collider>();
    }
    public void EnableHandCollider()
    {
        foreach(var item in handCollider)
        {
            item.enabled = true;
        }
    }
    public void DisableHandCollider()
    {
        foreach (var item in handCollider)
        {
            item.enabled = false;
        }
    }
    public void EnableHandColliderDelay(float delay)
    {
        Invoke("EnablHandCollider", delay);
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance>showNonPhysicalHandDistance)
        {
            nonPhysicalHand.enabled = true;
        }
        else
        {
            nonPhysicalHand.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
