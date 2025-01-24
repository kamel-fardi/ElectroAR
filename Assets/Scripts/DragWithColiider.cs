using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWithColiider : MonoBehaviour
{
    private Vector3 _offset;
    private float _zCoordinate;
    private Camera _camera;
    public GameObject replacementObject; // Set this in the inspector to the object to enable
    private Vector3 initialPosition;
    private bool isPlaced = false;

    void Awake()
    {
        _camera = Camera.main; // Cache the main camera
        initialPosition = transform.position; // Cache the initial position
    }

    void Update()
    {
        // Check for touch events
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (!isPlaced && touch.phase == TouchPhase.Began)
            {
                // Check if we're touching our object
                Ray ray = _camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    _zCoordinate = _camera.WorldToScreenPoint(gameObject.transform.position).z;
                    _offset = gameObject.transform.position - GetTouchWorldPos(touch);
                }
            }

            if (!isPlaced && touch.phase == TouchPhase.Moved)
            {
                if (_zCoordinate != 0) // Only move if we've successfully started a drag
                {
                    transform.position = GetTouchWorldPos(touch) + _offset;
                }
            }

            if (!isPlaced && touch.phase == TouchPhase.Ended)
            {
                // Check if the dragged object is near the placement object's collider
                Collider placementCollider = replacementObject.GetComponent<Collider>();
                if (placementCollider.bounds.Intersects(GetComponent<Collider>().bounds))
                {
                    // Disable the dragged object
                    gameObject.SetActive(false);

                    // Enable the replacement object
                    replacementObject.SetActive(true);

                    isPlaced = true; // Set the flag to indicate that the object is placed
                }
                

                // Reset zCoordinate to stop the drag
                _zCoordinate = 0;
            }
        }
    }

    private Vector3 GetTouchWorldPos(Touch touch)
    {
        Vector3 touchPoint = touch.position;
        touchPoint.z = _zCoordinate;
        Vector3 worldPoint = _camera.ScreenToWorldPoint(touchPoint);

        // Preserve the current y position of the object, effectively only allowing movement on the x and z axes.
        worldPoint.y = transform.position.y;

        return worldPoint;
    }
}
