using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool repeatSchedule = false;
    public List<TrainSchedule> times;
    private System.DateTime lastNow = System.DateTime.Now;
    private long previous = 0L;
    private int lastTrajet = 0;
    private bool inDirection = true;

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

        if(repeatSchedule) {
            if(now.Minute == lastNow.Minute + 1) {
                string src = inDirection ? src1 : src2;
                string dst = inDirection ? src2 : src1;
                LaunchTrain(player, src, dst, now, 1);
                inDirection = !inDirection;
            }
        } else {
            if(times.Exists(schedule => schedule.time == nowTime) && lastNow != now) {
                TrainSchedule ts = times.Find(schedule => schedule.time == nowTime);
                string dest = ts.start ? src2 : src1;
                string src = ts.start ? src1 : src2;

                LaunchTrain(player, src, dest, now, 1);
            }
        }

    }

    private void LaunchTrain(Player player, string src, string dest, System.DateTime nowTime, int trajet) {
        string message = "Le train de " + nowTime.Hour + ":" + nowTime.Minute + " à destination de " + dest + " arrive en gare";
        GameObject gameObject = GameObject.Find(gameObjectName);
        if(gameObject) {
            gameObject.transform.Find("GareInfo").Find("Canvas").Find("Text").GetComponent<Text>().text = message;
            player.CmdAnimate(gameObject, src, "", false);
        }
        lastNow = nowTime;
    }
}