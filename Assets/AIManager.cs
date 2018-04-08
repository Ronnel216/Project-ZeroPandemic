using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    // AIの感染者達
    struct InfectedActor
    {
        public GameObject actor;
        public Vector3 target;
    }

    List<InfectedActor> infectedActors;

    public int degugnum;

    // Use this for initialization
    void Start()
    {
        infectedActors = new List<InfectedActor>();
    }

    // Update is called once per frame
    void Update()
    {
        degugnum = infectedActors.Count;
    }

    // アクターの追加
    public void Register(GameObject actor)
    {
        if (infectedActors.Find(x => x.actor == actor).actor) return;
        var temp = new InfectedActor();
        temp.actor = actor;
        temp.target = new Vector3();
        infectedActors.Add(temp);
    }

    // アクターの登録の解消
    public void Remove(GameObject actor)
    {
        var removeData = infectedActors.FindIndex(x => x.actor == actor);
        if (removeData >= 0)
            infectedActors.RemoveAt(removeData);
    }

    // 感染者の目標座標を取得する
    public Vector3 GetInfectedActorTarget(GameObject actor)
    {
        InfectedActor temp = infectedActors.Find(x => x.actor == actor);
        if (temp.actor)
        {
            return temp.target;
        }

        Debug.Break();
        return new Vector3();
    }
}
