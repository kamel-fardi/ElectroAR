using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using System;

public class TogglePieces : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placementPrefab;
    [SerializeField]
    private GameObject light;
    public GameObject piece1;
    public GameObject piece2;

    private bool isPiece1Visible = true;
    private float touchCooldown = 0.5f; // Adjust the cooldown duration as needed
    private float lastTouchTime;

    void Start()
    {
        
        // Ensure both pieces are initially in the correct state
        piece1.SetActive(isPiece1Visible);
        piece2.SetActive(!isPiece1Visible);
        light.SetActive(!isPiece1Visible);
    }
    private bool arepresent(){
        foreach (var item in placementPrefab)
        {
            if(item.activeSelf==false)
            return false;
        }
        return true;
    }
    
    void Update()
    {
        // Check for touch events
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is on this object and cooldown is over
            if (Time.time - lastTouchTime > touchCooldown)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    if(arepresent()){
                    // Toggle the visibility of the pieces when touched
                    isPiece1Visible = !isPiece1Visible;
                    piece1.SetActive(isPiece1Visible);
                    piece2.SetActive(!isPiece1Visible);
                    light.SetActive(!isPiece1Visible);
                    //ToggleTorchWithAR(!isPiece1Visible);

                    lastTouchTime = Time.time;
                    } // Update the last touch time
                }
            }
        }
    }
}

