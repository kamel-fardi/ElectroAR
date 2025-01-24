using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class ARObjectPlacementEvent :UnityEvent<ARPlacementInteractableSingle, GameObject>
{
}