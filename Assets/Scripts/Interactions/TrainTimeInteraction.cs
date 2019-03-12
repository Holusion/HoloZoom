using System.Collections;
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

        if(repeatSchedule) {
            if(nowTime.y % 2 == 0 && lastNow.y != nowTime.y) {
                LaunchTrain(player, src2, src1, nowTime);
            } else if(nowTime.y % 3 == 0 && lastNow.y != nowTime.y) {
                LaunchTrain(player, src1, src2, nowTime);
            }
        } else {
            if(times.Exists(schedule => schedule.time == nowTime) && lastNow != nowTime) {
                TrainSchedule ts = times.Find(schedule => schedule.time == nowTime);
                string dest = ts.start ? src2 : src1;
                string src = ts.start ? src1 : src2;

                LaunchTrain(player, src, dest, nowTime);
            }
        }

    }

    private void LaunchTrain(Player player, string src, string dest, Vector3 nowTime) {
        string message = "Le train de " + nowTime.x + ":" + nowTime.y + " à destination de " + dest + " arrive en gare";
        GameObject gameObject = GameObject.Find(gameObjectName);
        if(gameObject) {
            gameObject.transform.Find("GareInfo").Find("Canvas").Find("Text").GetComponent<Text>().text = message;

            player.CmdAnimate(gameObject, src, "", false);
        }
        lastNow = nowTime;
    }
}