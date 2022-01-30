using UnityEngine;
using System.Collections.Generic;

public class BuildingMenu : MonoBehaviour {
    
    public GameObject iconPrototype;
    public GameObject layout;
    public List<CraftableItem> craftableItems;

    public bool isOpen { get; private set; }
    public int childCount => layout.transform.childCount - 1;

    int currentIndex;

    void Awake() {
        iconPrototype.SetActive(false);
        Close();
        craftableItems.Reverse();
        foreach (var item in craftableItems) {
            SpawnIcon(item);
        }
    }

    void Update() {
        if (!isOpen) return;
        if (Input.GetKeyDown(KeyCode.A)) {
            currentIndex++;
            if (currentIndex >= childCount) {
                currentIndex = 0;
            }
            HighlightItem(currentIndex);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            currentIndex--;
            if (currentIndex < 0) {
                currentIndex = childCount - 1;
            }
            HighlightItem(currentIndex);
        }
    }

    void SpawnIcon(CraftableItem item) {
        var newIcon = Instantiate(iconPrototype);
        newIcon.transform.SetParent(iconPrototype.transform.parent);
        newIcon.GetComponent<CraftingIcon>().SetItem(item);
        newIcon.SetActive(true);
    }

    void HighlightItem(int index) {
        foreach(Transform child in layout.transform) {
            if (child.gameObject.activeSelf) {
                child.GetComponent<CraftingIcon>().Unhighlight();
            }
        }
        layout.transform.GetChild(index + 1).GetComponent<CraftingIcon>().Highlight();
    }

    public void Open() {
        isOpen = true;
        layout.SetActive(true);
        currentIndex = layout.transform.childCount - 2;
        HighlightItem(currentIndex);
    }

    public void Close() {
        isOpen = false;
        layout.SetActive(false);
    }

    public CraftableItem GetSelection() {
        return layout.transform.GetChild(currentIndex + 1).GetComponent<CraftingIcon>().item;
    }
}