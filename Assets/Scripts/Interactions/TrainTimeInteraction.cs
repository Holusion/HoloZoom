using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Train time", menuName="Interactions/Train time")]
public class TrainTimeInteraction : Interaction
{
    [System.Serializable]
    public struct TrainSchedule {
        public Vector3 time;
        public bool start;
    }
    public string gameObjectName;
    public string city1;
    public string city2;
    public List<TrainSchedule> times;
    private System.DateTime lastNow = System.DateTime.Now;

    public TrainTimeInteraction()
    {

    }

    public override bool CanInteract(Player player)
    {
        return true;
    }

    public override void UpdateInteraction(Player player)
    {
        System.DateTime now = System.DateTime.Now;

        if(times.Exists(schedule => schedule.time == new Vector3(now.Hour, now.Minute, now.Second)) && now != lastNow) {
            TrainSchedule ts = times.Find(schedule => schedule.time == new Vector3(now.Hour, now.Minute, now.Second));
            string city = ts.start ? city2 : city1;
            Debug.Log("Le train de " + ts.time.x + ":" + ts.time.y + " à destination de " + city + " arrive en gare");
            now = lastNow;
        }
    }
}