using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlacementArea : MonoBehaviour
{
    public Vector3 placementPosition; // The target position for the object to snap to.
    public float snapTime = 0.5f; // Duration of the snap effect.

    private void OnTriggerEnter(Collider other)
    {
        DragAndDrop3D draggable = other.GetComponent<DragAndDrop3D>();
        if (draggable != null)
        {
            StartCoroutine(SnapToPlace(draggable.gameObject));
            draggable.enabled = false; // Optionally disable further dragging.
        }
    }

    private IEnumerator SnapToPlace(GameObject objectToPlace)
    {
        float elapsedTime = 0;

        // Get the initial position of the object.
        Vector3 startingPos = objectToPlace.transform.position;

        while (elapsedTime < snapTime)
        {
            // Move the object to the target position over snapTime seconds.
            objectToPlace.transform.position = Vector3.Lerp(startingPos, placementPosition, (elapsedTime / snapTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object is exactly at the placement position.
        objectToPlace.transform.position = placementPosition;
    }
}
