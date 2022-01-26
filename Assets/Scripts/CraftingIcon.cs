using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class CraftingIcon : MonoBehaviour {

    public Color defaultBorderColor;
    public Color highlightBorderColor;
    public Image iconImage;
    public GameObject descriptionContainer;
    public TMP_Text descriptionText;

    Image borderImage;

    public CraftableItem item { get; private set; }

    void Awake() {
        borderImage = GetComponent<Image>();
        Unhighlight();
    }
    
    public void Highlight() {
        borderImage.color = highlightBorderColor;
        descriptionContainer.SetActive(true);
    }

    public void Unhighlight() {
        borderImage.color = defaultBorderColor;
        descriptionContainer.SetActive(false);
    }

    public void SetItem(CraftableItem item) {
        iconImage.sprite = item.sprite;
        descriptionText.text = item.description;
        this.item = item;
    }
}