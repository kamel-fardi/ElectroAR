using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class faceCamera : MonoBehaviour
{
 Transform cam;
 Vector3 targetAngel = Vector3.zero;
Vector3 targetScale1 = Vector3.zero;
 void Start()
 {
     cam = Camera.main.transform;
 }
 void Update()
 {
    transform.LookAt(cam);
     targetAngel = transform.localEulerAngles;
     targetAngel.y = 0;
     targetAngel.x = 0;
     targetAngel.z = transform.localEulerAngles.z+90;
    targetScale1.x = 0.001f;
    targetScale1.y = 0.001f;
    targetScale1.z = 1;
    transform.localScale = targetScale1;
     transform.localEulerAngles = targetAngel;
     
 }
}
