using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAI : MonoBehaviour {

    public abstract class State
    {
        [System.Serializable]
        public class StateData
        {
            public CitizenAI ai;
            public GameObject catchObj;
            public AIRouteNode.Node[] nodes;
            public Virus virus;
            public Viewer viewer;
            public Movement movement;
        }

        public abstract void Excute(StateData data);

        public virtual void OnTriggerEnter(Collider other, StateData data) { }
    }

    State state;
    State nextState;
    [SerializeField]
    State.StateData stateData;

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
