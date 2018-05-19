using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAI : MonoBehaviour {

    // ステイトベース
    public abstract class State
    {
        // ステイトへ渡すデータ
        [System.Serializable]
        public class StateData
        {
            // AI
            public CitizenAI ai;
            // ウィルス
            public Virus virus;
            // 視野
            public Viewer viewer;
            // 移動
            public Movement movement;
            // 捕獲しているオブジェクト
            public GameObject catchObj;
            //// 移動ノード
            //public AIRouteNode.Node[] nodes;
        }

        // 状態のUpdate
        public abstract void Excute(StateData data);

        // 状態のあたり判定
        public virtual void OnTriggerEnter(Collider other, StateData data) { }
    }

    // ステイトデータ
    [SerializeField]
    State.StateData stateData;
    // ステイト
    State state;
    State nextState;
    
    // 移動ノード
    [SerializeField]
    GameObject[] routeNode;

    // Use this for initialization
    void Start () {
        nextState = null;
        state = new CitizenNormalState();
        stateData.ai = this;
        stateData.viewer.Target("InfectedActor");

        //AIRouteNode.GetNodeData(routeNode, out stateData.nodes);
    }

    // Update is called once per frame
    void Update () {
        if (nextState != null)
            state = nextState;
        state.Excute(stateData);
        
	}

    private void OnTriggerEnter(Collider other)
    {
        state.OnTriggerEnter(other, stateData);
    }

    // 状態の変更
    public void ChangeState(State state)
    {
        this.nextState = state;

    }

    // ステイト確認
    public bool CheckState<Type>()
    {
        return state.GetType() == typeof(Type);
    }
}
