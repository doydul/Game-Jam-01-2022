using UnityEngine;

[CreateAssetMenu(fileName = "BeeHive", menuName = "ProjectSpecific/Buildings/BeeHive", order = 0)]
public class BeeHive : BuildingType {

    public Sprite honeyIcon;
    public float productionTime;
    public int hungerSaciation;
    public int healthIncrease;

    public override void Init(Building parent) {
        parent.storage["timer"] = new Timer(productionTime);
        parent.storage["hasFood"] = false;
        parent.iconSprite.sprite = honeyIcon;
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
            parent.manager.AddHealth(healthIncrease);
        }
    }
}