using System;
using UnityEngine;

public class Curseur_follower : MonoBehaviour
{
    [SerializeField] private float distanceFromCamera = 5f; // Distance from the camera
    [SerializeField] private float impactForce = 5f; // Force applied on collision
    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera
        mainCamera = Camera.main; 
    }

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        // Get mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        
        // Convert mouse position to world space at a fixed distance from the camera
        mousePosition.z = distanceFromCamera;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        
        // Move object to the calculated position
        transform.position = worldPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("we are colliding" + other.gameObject.name.ToString());
        Rigidbody bubbleRb = other.gameObject.GetComponent<Rigidbody>();
        if (bubbleRb != null)
        {
            // Calculate force direction
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;

            // Apply force to simulate deformation
            bubbleRb.AddForce(forceDirection * impactForce, ForceMode.Impulse);
        }
    }
    
}
