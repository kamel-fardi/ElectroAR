using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject {
    public int id;
    public Sprite sprite;
    public Transform prefab;
    [TextArea(3, 10)]
    public string description; 
}
