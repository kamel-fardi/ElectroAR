using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure to include this if you're using TextMeshPro
using System;
using UnityEngine.SceneManagement;

public class DummyInventory : MonoBehaviour
{
    public event EventHandler<ItemSO> OnItemSelected;

    [SerializeField] private List<ItemSO> itemList;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject itemDetailsUI;
    [SerializeField] private GameObject backbutton; 
    [SerializeField] private Text itemDescriptionText;
    [SerializeField]public DataContainer dataContainer;
    private Dictionary<ItemSO, Transform> itemSOTransformDic;

    private void Awake()
    {
        itemTemplate = transform.Find("ScrollArea/Scroll/Container/Button");
        itemTemplate.gameObject.SetActive(false);

        itemSOTransformDic = new Dictionary<ItemSO, Transform>();

        foreach (ItemSO itemSO in itemList)
        {
            Transform itemTransform = Instantiate(itemTemplate, itemTemplate.parent);
            itemTransform.gameObject.SetActive(true);

            // Set the image texture
            SetItemImage(itemTransform, itemSO);

            // Add the item and its transform to the dictionary
            itemSOTransformDic[itemSO] = itemTransform;

            // Add click listener
            AddClickListener(itemTransform, itemSO);
        }
    }

    private void SetItemImage(Transform itemTransform, ItemSO itemSO)
    {
        if (itemSO?.sprite == null)
        {
            Debug.LogError("sprite in itemSO is null or itemSO is null");
            return;
        }

        RawImage imageComponent = itemTransform.Find("Image").GetComponent<RawImage>();
        if (imageComponent == null)
        {
            Debug.LogError("RawImage component not found in Image GameObject");
            return;
        }

        imageComponent.texture = itemSO.sprite.texture;
    }

    private void AddClickListener(Transform itemTransform, ItemSO itemSO)
    {
        Button button = itemTransform.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found in itemTransform");
            return;
        }

        button.onClick.AddListener(() => SelectItem(itemSO));
    }

    private void SelectItem(ItemSO selectedItemSO)
    {
        // Hide the inventory UI and show the item details UI
        ShowItemDetails();
        dataContainer.data = selectedItemSO.id;

        // Invoke the selected item event
        OnItemSelected?.Invoke(this, selectedItemSO);

        // Update the description text
        UpdateDescription(selectedItemSO);
        
       
    }
   
    private void UpdateDescription(ItemSO selectedItemSO)
    {
        if (itemDescriptionText == null)
        {
            Debug.LogError("Item description TextMeshProUGUI is not set in the inspector");
            return;
        }

        if (selectedItemSO == null)
        {
            Debug.LogError("Selected ItemSO is null");
            return;
        }

        // Set the description text
        itemDescriptionText.text = selectedItemSO.description;
    }

    public void ShowItemDetails()
    {
        inventoryUI.SetActive(false);
        itemDetailsUI.SetActive(true);
        //hide the bottom buttons
        backbutton.SetActive(false);
        
        
    }

    public void HideItemDetails()
    {
        inventoryUI.SetActive(true);
        itemDetailsUI.SetActive(false);
        backbutton.SetActive(true);

    }
    
     public void navigateTo()
     {
        if(dataContainer.data == 4){
            SceneManager.LoadScene("LampCiruitAR");
        }
        if(dataContainer.data == 5){
            SceneManager.LoadScene("LampCiruitAR");
        }
        if(dataContainer.data == 6){
            SceneManager.LoadScene("LampCiruitAR");
        }
       
    }
}
