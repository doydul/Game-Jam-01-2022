using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour {
    
    public SpriteRenderer sprite;
    public SpriteRenderer highlightSprite;
    public SpriteRenderer iconSprite;
    public SpriteMask highlightMask;
    public Collider2D collider;
    public Color canPlaceColor;
    public Color cantPlaceColor;

    public Dictionary<string, object> storage { get; private set; }
    public GameManager manager { get; private set; }
    public bool canPlace { get; private set; }
    public string name { get; private set; }
    public BuildingType behaviour { get; private set; }

    void Awake() {
        collider.isTrigger = true;
        storage = new Dictionary<string, object>();
        manager = FindObjectOfType<GameManager>();
    }

    void Update() {
        if (this.behaviour != null) this.behaviour.OnUpdate(this);
    }

    public void Init(float scale, Sprite sprite, string name, BuildingType behaviour) {
        transform.localScale = new Vector3(scale, scale, 1);
        this.sprite.sprite = sprite;
        this.highlightMask.sprite = sprite;
        this.name = name;
        this.behaviour = behaviour;
        if (this.behaviour != null) this.behaviour.Init(this);
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

    public void Interact() {
        behaviour.OnInteract(this);
    }
}