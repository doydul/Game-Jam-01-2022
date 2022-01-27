using UnityEngine;

[CreateAssetMenu(fileName = "Farm", menuName = "ProjectSpecific/Buildings/Farm", order = 0)]
public class Farm : BuildingType {

    public Sprite foodIcon;
    public float productionTime;
    public int hungerSaciation;

    public override void Init(Building parent) {
        parent.storage["timer"] = new Timer(productionTime);
        parent.storage["hasFood"] = false;
        parent.iconSprite.sprite = foodIcon;
        parent.iconSprite.gameObject.SetActive(false);
    }

    public override void OnUpdate(Building parent) {
        var timer = parent.storage["timer"] as Timer;
        var hasFood = (bool)parent.storage["hasFood"];
        if (!hasFood && timer.Check()) {
            parent.iconSprite.gameObject.SetActive(true);
            parent.storage["hasFood"] = true;
        }
    }

    public override void OnInteract(Building parent) {
        var hasFood = (bool)parent.storage["hasFood"];
        if (hasFood) {
            var timer = parent.storage["timer"] as Timer;
            timer.Start();
            parent.storage["hasFood"] = false;
            parent.iconSprite.gameObject.SetActive(false);
            parent.manager.AddHunger(hungerSaciation);
        }
    }
}