
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacmenetIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject placedPrefab;
    [SerializeField]
    GameObject placementIndicator;

    [SerializeField]
    InputAction touchInput;

    GameObject spawnedObject;
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        touchInput.performed += _ => { PlaceObject(); };
        placementIndicator.SetActive(false);
    }
    private void OnEnable()
    {
        touchInput.Enable();
    }
    private void OnDisable()
    {
        touchInput.Disable(); 
    }
    private void Update()
    {
        if(aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            placementIndicator.transform.SetPositionAndRotation(hitPose.position,hitPose.rotation);
            if (!placementIndicator.activeInHierarchy)
            {
                placementIndicator.SetActive(true);
            }

             
        }
    }
    void PlaceObject()
    {
        if(!placementIndicator.activeInHierarchy)
        {
            return;
        }
        if(spawnedObject == null)
        {
            spawnedObject = Instantiate(placedPrefab,placementIndicator.transform.position,placementIndicator.transform.rotation);

        }
        else
        {
            spawnedObject.transform.SetPositionAndRotation(placementIndicator.transform.position,placementIndicator.transform.rotation);
        }
    }

}
