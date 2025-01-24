using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using System;
public class ToggleBuzzer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placementPrefab;

    public GameObject piece1;
    public GameObject piece2;
    public AudioClip touchSound; // Assign the audio clip in the Inspector

    private bool isPiece1Visible = true;
    private float touchCooldown = 0.5f; // Adjust the cooldown duration as needed
    private float lastTouchTime;
    private AudioSource audioSource;

    void Start()
    {
        // Ensure both pieces are initially in the correct state
        piece1.SetActive(isPiece1Visible);
        piece2.SetActive(!isPiece1Visible);

        // Ensure an AudioSource component is attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource
        audioSource.clip = touchSound;
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

                    // Play the assigned audio clip
                    if (audioSource != null && touchSound != null)
                    {
                        audioSource.PlayOneShot(touchSound);
                    }

                    lastTouchTime = Time.time; // Update the last touch time
                }
                }
            }
        }
    }
}
