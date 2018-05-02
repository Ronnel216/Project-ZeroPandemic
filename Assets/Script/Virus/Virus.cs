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

    enum CitizenType
    {
        NORMAL,
        STRONG,
    };

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

    [SerializeField]
    private float virusTime;

    State state;
    State nextState;

    AudioSource audio;

    [SerializeField]
    AudioClip infectedSE;

    [SerializeField]
    GameObject birthEffect;

    [SerializeField]
    private GameObject comboManager;

    [SerializeField]
    private CitizenType citizenType;

    private ComboScript combo;

    [SerializeField]
    private float virusstealTime;

    private bool virusFlag;

    // Use this for initialization
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        state = new UnVirusState(this);
        nextState = null;
        originalVirus = null;
        virusFlag = false;
        // モデルからMeshRendererコンポーネントを探す
        m_modelMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        // 色を取得
        int size = m_modelMesh.materials.Length;
        m_defaultColor = new Color[size];
        for (int i = 0; i < size; i++)
            m_defaultColor[i] = m_modelMesh.materials[i].color;

        comboManager = GameObject.Find("ComboManager");
        combo = comboManager.GetComponent<ComboScript>();
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
        if(gameObject.tag!="Player")
            combo.PlusCombo();

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

        if (gameObject.tag != "Player")
            StartCoroutine(StealVirus(gameObject));
        else
        // 感染者であることを示す
        gameObject.tag = "InfectedActor";

        


        // 感染源なら潜伏時間をスキップする

        if (citizenType == CitizenType.NORMAL)
        {
            if (infectedActor == null) ChangeState(new InfectedState(this, stateData));
            else ChangeState(new StayState(this, stateData)); 
            GameManager.infectedNum += 1;
        }

        if(citizenType == CitizenType.STRONG)
        {
            ChangeState(new WaitState(this, stateData));
        }

        audio.PlayOneShot(infectedSE);
        birthEffect = Instantiate(birthEffect, gameObject.transform);

        //Debug.Log(gameObject.name + " : Infected");

        StartCoroutine(combo.ComboCoroutine());

    }

    // ウィルスを感染可能な状態にする
    void ActivateInfection()
    {
        ChangeState(new InfectedState(this, stateData));
    }

    void ActivateStay()
    {
        ChangeState(new StayState(this, stateData));
    }

    void UnVirus()
    {
        GameManager.infectedNum -= 1;
        ChangeState(new UnVirusState(this));
    }

    void Recovery()
    {
        if (IsInfected() == false) return;
        GameManager.infectedNum -= 1;
        ChangeState(new UnVirusState(this));
    }

    void KillSelf()
    {
        GameManager.infectedNum -= 1;
        GameManager.killedNum += 1;
        Destroy(gameObject);
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

    // 待機状態 ----------------------------------------------------------
    class WaitState : State
    {
        float time;
        float waitTime;
        float virusTime;

        public WaitState(Virus virus, StateData data) : base(virus)
        {
            time = 0.0f;
            waitTime = 0;
            virusTime = virus.virusTime;
        }

        public override void Execute()
        {
            if (virus.GetvirusFlag())
            {
                waitTime += Time.deltaTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
                virus.UnVirus();
            }

            if (waitTime > virusTime)
            {
                GameManager.infectedNum += 1;
                virus.ActivateStay();
            }
        }
    }

    IEnumerator StealVirus(GameObject infectedActor)
    {
        yield return new WaitForSeconds(virusstealTime);
        infectedActor.tag = "NoneAbilityActor";
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
        GameObject birthEffect;

        public InfectedState(Virus virus, StateData data) : base(virus)
        {
            infectionArea = data.infectionArea;
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
            //else virus.KillSelf();   // 仮
            

            // 現在の割合を代入
            virus.InfectionCondition = invasiveness / invasivenessLimit;
            // 感染状態の自然治癒
            RecoveryVirus();

        }

        void RecoveryVirus()
        {
            // コードを無効化
            if (true) return;

            // 病原体はウィルスは弱まらない
            if (virus.originalVirus == null) return;

            // 感染から回復する（お亡くなりになられる）
            if (invasiveness <= 0.0f)
            {
                virus.KillSelf();
                //virus.Recovery();
                //if (infectionArea != null)
                //    Destroy(infectionArea);
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
    private SkinnedMeshRenderer m_modelMesh;                      // モデルのSkinnedMeshRendererrコンポーネント
    private Color[] m_defaultColor;                             　// 初期マテリアル色


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
        int size = m_modelMesh.materials.Length;
        for (int i = 0; i < size; i++)
        {
            Color color = Color.Lerp(m_defaultColor[i], m_maxInfectionColor, m_infectionCondition);
            m_modelMesh.materials[i].color = color;
        }
    }

    public float InfectionCondition
    {
        get { return m_infectionCondition; }
        set { m_infectionCondition = value; }
    }

    public void SetVirusFlag(bool flag)
    {
        virusFlag = flag;
    }

    public bool GetvirusFlag()
    {
        return virusFlag;
    }

}

