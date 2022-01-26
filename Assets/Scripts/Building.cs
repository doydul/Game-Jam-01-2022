using UnityEngine;

public class Building : MonoBehaviour {
    
    public SpriteRenderer sprite;
    public SpriteRenderer highlightSprite;
    public SpriteMask highlightMask;
    public Collider2D collider;
    public Color canPlaceColor;
    public Color cantPlaceColor;

    public bool canPlace { get; private set; }
    public string name { get; private set; }

    void Awake() {
        collider.isTrigger = true;
    }

    public void Init(float scale, Sprite sprite, string name) {
        transform.localScale = new Vector3(scale, scale, 1);
        this.sprite.sprite = sprite;
        this.highlightMask.sprite = sprite;
        this.name = name;
    }

    public void CanPlace() {
        highlightSprite.color = canPlaceColor;
        canPlace = true;
    }

    public void CantPlace() {
        highlightSprite.color = cantPlaceColor;
        canPlace = false;
    }

    public void Place() {
        highlightSprite.gameObject.SetActive(false);
        highlightMask.gameObject.SetActive(false);
        collider.isTrigger = false;
    }
}