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
            public Virus virus;
            public Viewer viewer;
            public Movement movement;
        }


        public abstract void Excute(StateData data);
    }

    State state;
    State nextState;
    [SerializeField]
    State.StateData stateData;
    
    // Use this for initialization
    void Start () {
        nextState = null;
        state = new CitizenNormalState();
        stateData.ai = this;
        stateData.viewer.Target("InfectedActor");
    }

    // Update is called once per frame
    void Update () {
        if (nextState != null)
            state = nextState;
        state.Excute(stateData);
	}

    public void ChangeState(State state)
    {
        this.nextState = state;
    }
}
