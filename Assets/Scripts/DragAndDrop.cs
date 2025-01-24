using UnityEngine;

public class DragAndDrop3D : MonoBehaviour
{
    private Vector3 _offset;
    private float _zCoordinate;
    private Camera _camera;
    public float snapDistance = 0.5f;

    void Awake()
    {
        _camera = Camera.main; // Cache the main camera
    }

    void Update()
    {
        // Check for touch events
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
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

            if (touch.phase == TouchPhase.Moved)
            {
                if (_zCoordinate != 0) // Only move if we've successfully started a drag
                {
                    transform.position = GetTouchWorldPos(touch) + _offset;
                }
            }

            if (touch.phase == TouchPhase.Ended)
        {
            // Check if the distance is less than the snap distance
            float distanceToTarget = Vector3.Distance(gameObject.transform.position, new Vector3(0.0f,0.0f,2.25f));
            
            if (distanceToTarget < snapDistance)
            {
                // Snap the object to the target position
                gameObject.transform.position = new Vector3(0.0f,0.0f,2.25f);
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
/*
using UnityEngine;

public class DragAndDrop3D : MonoBehaviour
{
    [SerializeField]
    private GameObject Objects;

    private Vector3 _offset;
    private float _zCoordinate;
    private Camera _camera;
    //public Transform targetPosition; // Set this in the inspector to the desired target position
    public float snapDistance = 0.0001f;

    void Awake()
    {
        _camera = Camera.main; // Cache the main camera
    }

    void Update()
    {
        // Check for touch events
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Check if we're touching our object
                Ray ray = _camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit) && hit.transform == Objects.transform)
                {
                    _zCoordinate = _camera.WorldToScreenPoint(Objects.transform.position).z;
                    _offset = Objects.transform.position - GetTouchWorldPos(touch);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (_zCoordinate != 0) // Only move if we've successfully started a drag
                {
                    Objects.transform.position = GetTouchWorldPos(touch) + _offset;
                }
            }

            if (touch.phase == TouchPhase.Ended)
        {
            // Check if the distance is less than the snap distance
            float distanceToTarget = Vector3.Distance(Objects.transform.position, new Vector3(0.0f,0.0f,2.25f));
            
            if (distanceToTarget < snapDistance)
            {
                // Snap the object to the target position
                Objects.transform.position = new Vector3(0.0f,0.0f,2.25f);
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
*/