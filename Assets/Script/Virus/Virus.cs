using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    // -------------------------------------------------------------------
    // Virus内用ステイト -------------------------------------------------
    abstract class State
    {
        public Virus virus;

        public State(Virus virus)
        {
            this.virus = virus;
        }

        public abstract void Execute();
    }

    // -------------------------------------------------------------------
    // ステイトデータ設定用 ----------------------------------------------
    [System.Serializable]
    class StateData
    {
        public GameObject infectionArea;
        public float virusStayTime;
    }

    static string infectionTag = "InfectionArea";

    [SerializeField]
    StateData stateData;

    State state;
    State nextState;

    AudioSource audio;

    [SerializeField]
    AudioClip infectedSE;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        state = new UnVirusState(this);
        nextState = null;

        // モデルからMeshRendererコンポーネントを探す
        m_modelMesh = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 状態の変更
        if (nextState != null) state = nextState;

        // 状態ごとの処理
        state.Execute();

        // 見た目への変更
        UpdateModelCondition();
    }

    // 感染エリアに侵入した
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag != infectionTag) return;
        //if (IsInfected()) return;
        //VirusAbility virus = other.gameObject.GetComponentInParent<VirusAbility>(); // 仮
        //Infected(virus);
    }

    public bool IsInfected()
    {
        bool isInfected = false;
        if (nextState != null)
            isInfected = nextState.GetType() != typeof(UnVirusState);
        return isInfected || state.GetType() != typeof(UnVirusState);
    }

    //　感染
    public void Infected(VirusAbility virus)
    {
        VirusAbility selfVirus = GetComponent<VirusAbility>();
        if (virus != null)
            virus.Copy(selfVirus);
        gameObject.tag = "InfectedActor";
        if (virus == null) ChangeState(new InfectedState(this, stateData));
        else ChangeState(new StayState(this, stateData));
        Debug.Log(gameObject.name + " : Infected");
        GameManager.infectedNum += 1;
        audio.PlayOneShot(infectedSE);
    }

    // ウィルスを感染可能な状態にする
    void ActivateInfection()
    {
        ChangeState(new InfectedState(this, stateData));
    }

    void ChangeState(State state)
    {
        nextState = state;
    }

    // -------------------------------------------------------------------
    // 無感染 ------------------------------------------------------------
    class UnVirusState : State
    {
        public UnVirusState(Virus virus) : base(virus) { }
        public override void Execute() { }
    }

    // -------------------------------------------------------------------
    // 潜伏状態 ----------------------------------------------------------
    class StayState : State
    {
        float virusStayTime;
        float time;

        public StayState(Virus virus, StateData data) : base(virus)
        {
            virusStayTime = data.virusStayTime;
            time = 0.0f;
        }

        public override void Execute()
        {

            // 潜伏時間を数える
            time = Mathf.Min(time + Time.deltaTime, virusStayTime);

            // ウィルスを活性化させる
            if (virusStayTime <= time) virus.ActivateInfection();

        }
    }

    // -------------------------------------------------------------------
    // 感染可能状態 ------------------------------------------------------
    class InfectedState : State
    {
        GameObject infectionArea;
        bool canInfected;

        public InfectedState(Virus virus, StateData data) : base(virus)
        {
            infectionArea = data.infectionArea;
            canInfected = false;
        }

        public override void Execute()
        {
            if (canInfected == true) return;
            infectionArea = Instantiate(infectionArea, virus.gameObject.transform);
            canInfected = true;
        }

        ~InfectedState()
        {
            if (infectionArea != null)
                Destroy(infectionArea);
        }
    }



    // ウイルスの感染具合 ==========================================================
    [SerializeField]
    private Color m_maxInfectionColor = Color.magenta;            // 感染具合最大時のモデル色
    private int m_infectionCondition = 0;                         // ウイルスの感染具合(0～100%)
    private MeshRenderer[] m_modelMesh;                           // モデルのMeshRendererコンポーネント



    //----------------------------------------------------------------------
    //! @brief 感染具合による見た目の変更
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void UpdateModelCondition()
    {
        float condition = m_infectionCondition / 100.0f;

        // マテリアルの色を変える
        foreach (var m in m_modelMesh)
            m.material.color = Color.Lerp(Color.white, m_maxInfectionColor, condition);
    }

    public int InfectionCondition
    {
        get { return m_infectionCondition; }
        set { m_infectionCondition = value; }
    }
}
        
