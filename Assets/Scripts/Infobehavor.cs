using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    const float speed = 5.0f;

    [SerializeField]
    Transform selectionInof;

    Vector3 desiredscale = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        selectionInof.localScale = Vector3.Lerp(selectionInof.localScale, desiredscale, Time.deltaTime * speed);

    }
    public void OpenInfo()
    {
        desiredscale = Vector3.one;
    }
    public void CloseInfo()
    {
        desiredscale = Vector3.zero;
    }
}
