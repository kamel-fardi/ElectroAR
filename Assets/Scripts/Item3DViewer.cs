using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private DummyInventory dummyInventory;

    private Transform itemPrefab;

   
    private void Awake() // Change this from Start() to Awake()
    {
        dummyInventory.OnItemSelected += DummyInventory_OnItemSelected;
    }


    private void DummyInventory_OnItemSelected(object sender, ItemSO itemSO)
    {
        Debug.Log("Instantiating prefab: " + itemSO.prefab);
        if (itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);
        }
        itemPrefab = Instantiate(itemSO.prefab);
        if (itemPrefab != null)
        {
            Debug.Log("Prefab instantiated successfully at position: " + itemPrefab.position);
        }
        else
        {
            Debug.LogError("Failed to instantiate prefab.");
        }
        itemPrefab.transform.SetParent(null);
        Debug.Log(itemSO.name);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (itemPrefab != null)
        {
            itemPrefab.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
        }
        else
        {
            Debug.LogError("itemPrefab is null during drag");
        }
    }
}
