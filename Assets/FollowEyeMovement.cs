using UnityEngine;

public class FollowHeadMovement : MonoBehaviour
{
    public Transform headTransform; // Reference to the CenterEyeAnchor or TrackingSpace

    void LateUpdate()
    {
        // Ensure that the reference is set
        if (headTransform != null)
        {
            // Update the position and rotation of your GameObject to follow the head
            transform.position = headTransform.position;
            transform.rotation = headTransform.rotation;
        }
        else
        {
            Debug.LogError("Head transform reference is not set!");
        }
    }
}
