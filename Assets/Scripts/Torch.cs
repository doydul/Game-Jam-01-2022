using UnityEngine;

[CreateAssetMenu(fileName = "Torch", menuName = "ProjectSpecific/Buildings/Torch", order = 0)]
public class Torch : BuildingType {

    public Sprite lightSprite;
    
    public override void Init(Building parent) {
        var sm = parent.gameObject.AddComponent<SpriteMask>();
        sm.sprite = lightSprite;
    }
}