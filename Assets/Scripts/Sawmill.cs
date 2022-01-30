using UnityEngine;

[CreateAssetMenu(fileName = "Sawmill", menuName = "ProjectSpecific/Buildings/Sawmill", order = 0)]
public class Sawmill : BuildingType {

    public float productionTime;

    public override void Init(Building parent) {
        parent.storage["timer"] = new Timer(productionTime);
    }

    public override void OnUpdate(Building parent) {
        var timer = parent.storage["timer"] as Timer;
        if (timer.Check()) {
            parent.manager.AddWood(1);
        }
    }
}