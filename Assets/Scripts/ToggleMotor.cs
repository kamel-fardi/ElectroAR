using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using System;
public class ToggleMotor : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placementPrefab;

    public GameObject piece1;
    public GameObject piece2;

    public GameObject spinningObject; // Assign the spinning object in the Inspector
    private bool isPiece1Visible = true;
    private float touchCooldown = 0.5f; // Adjust the cooldown duration as needed
    private float lastTouchTime;
    private Coroutine spinningCoroutine;

    void Start()
    {
        // Ensure both pieces are initially in the correct state
        piece1.SetActive(isPiece1Visible);
        piece2.SetActive(!isPiece1Visible);
    }
    public void VibrateDevice()
 {
     Handheld.Vibrate();
     
 }
    private bool ArePresent()
    {
        foreach (var item in placementPrefab)
        {
            if (item.activeSelf == false)
                return false;
        }
        return true;
    }

    IEnumerator SpinObject()
    {
        while (true)
        {
            spinningObject.transform.Rotate(Vector3.right * Time.deltaTime * 100.0f); // Adjust the rotation speed as needed
            yield return null;
        }
    }

    void StopSpinning()
    {
        if (spinningCoroutine != null)
        {
            StopCoroutine(spinningCoroutine);
        }
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
                    if (ArePresent())
                    {
                        // Toggle the visibility of the pieces when touched
                        isPiece1Visible = !isPiece1Visible;
                        piece1.SetActive(isPiece1Visible);
                        piece2.SetActive(!isPiece1Visible);

                        // Start or stop spinning based on the visibility state
                        if (isPiece1Visible)
                        {
                            StopSpinning();
                        }
                        else
                        {
                            VibrateDevice();
                            spinningCoroutine = StartCoroutine(SpinObject());
                        }

                        lastTouchTime = Time.time; // Update the last touch time
                    }
                }
            }
        }
    }
}
