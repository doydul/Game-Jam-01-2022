using UnityEngine;

[CreateAssetMenu(fileName = "HerbGarden", menuName = "ProjectSpecific/Buildings/HerbGarden", order = 0)]
public class HerbGarden : BuildingType {

    public Sprite herbIcon;
    public float productionTime;
    public int healthIncrease;

    public override void Init(Building parent) {
        parent.storage["timer"] = new Timer(productionTime);
        parent.storage["hasHerbs"] = false;
        parent.iconSprite.sprite = herbIcon;
        parent.iconSprite.gameObject.SetActive(false);
    }

    public override void OnUpdate(Building parent) {
        var timer = parent.storage["timer"] as Timer;
        var hasHerbs = (bool)parent.storage["hasHerbs"];
        if (!hasHerbs && timer.Check()) {
            parent.iconSprite.gameObject.SetActive(true);
            parent.storage["hasHerbs"] = true;
        }
    }

    public override void OnInteract(Building parent) {
        var hasHerbs = (bool)parent.storage["hasHerbs"];
        if (hasHerbs) {
            var timer = parent.storage["timer"] as Timer;
            timer.Start();
            parent.storage["hasHerbs"] = false;
            parent.iconSprite.gameObject.SetActive(false);
            parent.manager.AddHealth(healthIncrease);
        }
    }
}