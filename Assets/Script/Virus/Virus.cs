﻿using System;
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
        public float invasivenessLimit;
        public float virusStayTime;
        public float virusDistance;
    }

    static string infectionTag = "InfectionArea";

    // 病原体
    Virus originalVirus;

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
        originalVirus = null;

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

    public Virus GetOriginal()
    {
        return originalVirus; 
    }

    public bool IsInfected()
    {
        bool isInfected = false;
        if (nextState != null)
            isInfected = nextState.GetType() != typeof(UnVirusState);
        return isInfected || state.GetType() != typeof(UnVirusState);
    }

    //　感染
    public void Infected(GameObject infectedActor)
    {
        if (infectedActor != null)
        {
            // 病原体の能力をコピーする
            VirusAbility ability = infectedActor.GetComponent<VirusAbility>();
            VirusAbility selfAbility = GetComponent<VirusAbility>();
            ability.Copy(selfAbility);
            Virus original = infectedActor.GetComponent<Virus>();
            originalVirus = original;
        }
        else
            originalVirus = this;

        // 感染者であることを示す
        gameObject.tag = "InfectedActor";

        // 感染源なら潜伏時間をスキップする
        if (infectedActor == null) ChangeState(new InfectedState(this, stateData));
        else ChangeState(new StayState(this, stateData));

        GameManager.infectedNum += 1;
        audio.PlayOneShot(infectedSE);

        //Debug.Log(gameObject.name + " : Infected");

    }

    // ウィルスを感染可能な状態にする
    void ActivateInfection()
    {
        ChangeState(new InfectedState(this, stateData));
    }

    void Recovery()
    {
        ChangeState(new UnVirusState(this));
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
        // 感染範囲
        GameObject infectionArea;
        // 感染可能
        bool cratingInfectedArea;
        // 感染が弱まりだす距離
        float virusDistance;
        // 感染度 (s)
        float invasivenessLimit;
        float invasiveness;

        public InfectedState(Virus virus, StateData data) : base(virus)
        {
            infectionArea = data.infectionArea;
            cratingInfectedArea = false;
            virusDistance = data.virusDistance;
            invasivenessLimit = data.invasivenessLimit;
            invasiveness = invasivenessLimit;

            // 感染エリアを構築済み
            if (cratingInfectedArea == true) return;

            // 感染エリアの生成
            infectionArea = Instantiate(infectionArea, virus.gameObject.transform);
            infectionArea.transform.position+= new Vector3(0, 0.1f, 0);
            cratingInfectedArea = true;

        }

        public override void Execute()
        {
            // これ以降　感染源には必要のない処理
            if (virus.originalVirus == virus) return;

            // 病原体付近なら感染を強める
            if (virus.originalVirus == null) Debug.Break();

            var actor = virus.originalVirus.gameObject;
            float distance = (actor.transform.position - virus.gameObject.transform.position).magnitude;

            if (distance <= virusDistance)
            {
                invasiveness = invasivenessLimit;
            }

            // 現在の割合を代入
            virus.InfectionCondition = invasiveness / invasivenessLimit;
            // 感染状態の自然治癒
            RecoveryVirus();

        }

        void RecoveryVirus()
        {
            // 病原体はウィルスは弱まらない
            if (virus.originalVirus == null) return;

            // 感染から回復する
            if (invasiveness <= 0.0f)
            {
                virus.Recovery();
                if (infectionArea != null)
                    Destroy(infectionArea);
            }

            // 時間の更新
            invasiveness = Math.Max(0.0f, invasiveness - Time.deltaTime);            
        }

        //~InfectedState()
        //{

        //}
    }



    // ウイルスの感染具合 ==========================================================
    [SerializeField]
    private Color m_maxInfectionColor = Color.magenta;            // 感染具合最大時のモデル色
    private float m_infectionCondition = 0;                       // ウイルスの感染具合(0.0f～1.0f)
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
        // マテリアルの色を変える
        foreach (var m in m_modelMesh)
            m.material.color = Color.Lerp(Color.white, m_maxInfectionColor, m_infectionCondition);
    }

    public float InfectionCondition
    {
        get { return m_infectionCondition; }
        set { m_infectionCondition = value; }
    }
}
        
