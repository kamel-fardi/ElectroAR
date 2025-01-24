using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;



    public partial class CircuitPlacement : ARBaseGestureInteractable
    {
        [SerializeField]
        [Tooltip("A GameObject to place when a ray cast from a user touch hits a plane.")]
    private List<GameObject> placementPrefab;
    
    private ARPlaneManager arPlaneManager;
    /*GameObject m_PlacementPrefab;


    public GameObject placementPrefab
    {
        get => m_PlacementPrefab;
        set => m_PlacementPrefab = value;
    }*/
    [SerializeField]
    private DataContainer dataContainer;

    [SerializeField]
        [Tooltip("The LayerMask that Unity uses during an additional ray cast when a user touch does not hit any AR trackable planes.")]
        LayerMask m_FallbackLayerMask;
    private GameObject placementObject;
    private GameObject placementObject1;


        public LayerMask fallbackLayerMask
        {
            get => m_FallbackLayerMask;
            set => m_FallbackLayerMask = value;
        }

        [SerializeField]
        ARObjectPlacementEvent m_ObjectPlaced = new ARObjectPlacementEvent();

        public ARObjectPlacementEvent objectPlaced
        {
            get => m_ObjectPlaced;
            set => m_ObjectPlaced = value;
        }

        readonly ARObjectPlacementEventArgs m_ObjectPlacementEventArgs = new ARObjectPlacementEventArgs();

        static readonly List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    
    protected virtual bool TryGetPlacementPose(TapGesture gesture, out Pose pose)
        {
            // Raycast against the location the player touched to search for planes.
            var hit = xrOrigin != null
                ? GestureTransformationUtility.Raycast(gesture.startPosition, s_Hits, xrOrigin, TrackableType.PlaneWithinPolygon, m_FallbackLayerMask)
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
                : arSessionOrigin != null && GestureTransformationUtility.Raycast(gesture.startPosition, s_Hits, arSessionOrigin, TrackableType.PlaneWithinPolygon, m_FallbackLayerMask);
#pragma warning restore 618
            if (hit)
            {
                pose = s_Hits[0].pose;

                // Use hit pose and camera pose to check if hit test is from the
                // back of the plane, if it is, no need to create the anchor.
                // ReSharper disable once LocalVariableHidesMember -- hide deprecated camera property
                var camera = xrOrigin != null
                    ? xrOrigin.Camera
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
                    : (arSessionOrigin != null ? arSessionOrigin.camera : Camera.main);
#pragma warning restore 618
                if (camera == null)
                    return false;

                return Vector3.Dot(camera.transform.position - pose.position, pose.rotation * Vector3.up) >= 0f;
            }

            pose = default;
            return false;
        }

        /// <summary>
        /// Instantiates the placement object and positions it at the desired pose.
        /// </summary>
        /// <param name="pose">The pose at which the placement object will be instantiated.</param>
        /// <returns>Returns the instantiated placement object at the input pose.</returns>
        /// <seealso cref="placementPrefab"/>
        protected virtual GameObject PlaceObject(Pose pose)
        {
            var  otherpos = new Vector3(pose.position.x-1,pose.position.y,pose.position.z);
            var placementObject = Instantiate(placementPrefab[0], pose.position, pose.rotation);
            var placementObject1 = Instantiate(placementPrefab[1], otherpos, pose.rotation);

            // Create anchor to track reference point and set it as the parent of placementObject.
            var anchor = new GameObject("PlacementAnchor").transform;
            anchor.position = pose.position;
            anchor.rotation = pose.rotation;
            placementObject.transform.parent = anchor;
            placementObject1.transform.parent = anchor;
            // Use Trackables object in scene to use as parent
            var trackablesParent = xrOrigin != null
                ? xrOrigin.TrackablesParent
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
                : (arSessionOrigin != null ? arSessionOrigin.trackablesParent : null);
#pragma warning restore 618
            if (trackablesParent != null)
                anchor.parent = trackablesParent;

            return placementObject1;
        }

        /// <summary>
        /// Unity calls this method automatically when the user places an object.
        /// </summary>
        /// <param name="args">Event data containing a reference to the instantiated placement object.</param>
        protected virtual void OnObjectPlaced(ARObjectPlacementEventArgs args)
        {
        // m_ObjectPlaced?.Invoke(args);
        //m_OnObjectPlaced?.Invoke(args.placementInteractable, args.placementObject);
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.enabled = false;
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

        /// <inheritdoc />
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            return gesture.targetObject == null;
        }

    /// <inheritdoc />
    protected override void OnEndManipulation(TapGesture gesture)
    {
        base.OnEndManipulation(gesture);

        if (gesture.isCanceled)
            return;

#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
        if (xrOrigin == null && arSessionOrigin == null)
            return;
#pragma warning restore 618
        if (placementObject == null)
        {
            if (TryGetPlacementPose(gesture, out var pose))
            {
                 placementObject = PlaceObject(pose);

                //m_ObjectPlacementEventArgs.placementInteractable = this;
                m_ObjectPlacementEventArgs.placementObject = placementObject;
                OnObjectPlaced(m_ObjectPlacementEventArgs);
                
            }
        }
    }
    }



