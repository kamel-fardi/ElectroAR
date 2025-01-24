
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARPlacementInteractableSingle : ARBaseGestureInteractable
{
    [SerializeField]
    private List<GameObject> placementPrefab;
    [SerializeField]
    private ARObjectPlacementEvent onObjectPlaced;
    [SerializeField]
    private ARRaycastManager arRaycastManager;
    [SerializeField]
    private DataContainer dataContainer;
    private GameObject placementObject;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    //List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private static GameObject trackblesObject;
   
    
    protected override bool CanStartManipulationForGesture(TapGesture gesture){
        if(gesture.targetObject == null){
            return true;
        }
        return false;
    }

    
    protected override void OnEndManipulation(TapGesture gesture){
        if(gesture.isCanceled){
            return;
        }
        if(gesture.targetObject != null){
            return;
        }
        if (arRaycastManager.Raycast(gesture.startPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = hits[0];
            if (Vector3.Dot(Camera.main.transform.position - hit.pose.position, hit.pose.rotation * Vector3.up) < 0)
            {
                return;
            }
            if (placementObject == null) {
                placementObject = Instantiate(placementPrefab[dataContainer.data],hit.pose.position,hit.pose.rotation);
                var anchorObject = new GameObject("PlacmentAnchor");
                anchorObject.transform.position = hit.pose.position;
                //anchorObject.transform.rotation = hit.pose.rotation;
                if(trackblesObject == null)
                {
                    trackblesObject = GameObject.Find("Trackables");
                }
                if (trackblesObject != null) 
                {
                    anchorObject.transform.parent = trackblesObject.transform;
                }
                onObjectPlaced?.Invoke(this,placementObject);
            }
        }
    }
}