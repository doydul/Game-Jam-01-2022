using UnityEngine;

[CreateAssetMenu(fileName = "Quarry", menuName = "ProjectSpecific/Buildings/Quarry", order = 0)]
public class Quarry : BuildingType {

    public float productionTime;

    public override void Init(Building parent) {
        parent.storage["timer"] = new Timer(productionTime);
    }

    public override void OnUpdate(Building parent) {
        var timer = parent.storage["timer"] as Timer;
        if (timer.Check()) {
            parent.manager.AddStone(1);
        }
    }
}