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
    public string src1;
    public string src2;
    public List<TrainSchedule> times;
    private Vector3 lastNow = new Vector3(-1,-1,-1);
    private long previous = 0L;

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
        Vector3 nowTime = new Vector3(now.Hour, now.Minute, now.Second);

        if(times.Exists(schedule => schedule.time == nowTime) && lastNow != nowTime) {
            TrainSchedule ts = times.Find(schedule => schedule.time == nowTime);
            string dest = ts.start ? src2 : src1;
            string src = ts.start ? src1 : src2;
            Debug.Log("Le train de " + ts.time.x + ":" + ts.time.y + " à destination de " + dest + " arrive en gare");

            player.CmdAnimate(GameObject.Find(gameObjectName), src);
            
            lastNow = nowTime;
        }
    }
}