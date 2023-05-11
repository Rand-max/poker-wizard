using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> checkpointmanager;
    public List<int> leaderboard;
    public List<float> rank;
    public List<float> sortedrank;
    public List<int>ShootPoint;
    public List<TextMeshProUGUI>shootpointui;
    public List<int>gold;
    public List<bool>finished;
    public LoadScene loadScene;
    public ScoreContainer scoreContainer;
    public bool goldspawned=false;
    public GameObject[] finishAni;

    //add points adding ani
    [SerializeField]
    GameObject TeamAPointChangePrefab;
    [SerializeField]
    Transform TeamAPointParent;
    [SerializeField]
    RectTransform endpoint;

    // Start is called before the first frame update
    void Start()
    {
        finishAni[0].SetActive(false);
        finishAni[1].SetActive(false);
        finishAni[2].SetActive(false);
        finishAni[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if(!finished[i]){
                rank[i]=checkpointmanager[i].GetComponent<CheckpointController>().lap*100+getrank(checkpointmanager[i].GetComponent<CheckpointController>())-checkpointmanager[i].GetComponent<CheckpointController>().distance/1000f;
            }
        }
        sortedrank=new List<float>(rank);
        sortedrank.Sort();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if(rank[i]==sortedrank[j]){
                    leaderboard[i]=4-j;
                }
            }
        }
        if(finished[0]&&finished[1]&&finished[2]&&finished[3]){
            scoreContainer.SaveScore();
            loadScene.LoadtheScene("ScoreBoard");
        }
        if(!goldspawned&&sortedrank[3]>200f){
            GetComponent<GoldEggSpawner>().spawn(2);
            goldspawned=true;
        }
        if(!GetComponent<GoldEggSpawner>().timerIsRunning&&sortedrank[3]>300f){
            GetComponent<GoldEggSpawner>().TimerStart();
        }
    }
    public void AddScore(int playernum,int score){
        ShootPoint[playernum]+=score;
        shootpointui[0].text=(ShootPoint[0]+ShootPoint[1]+gold[0]*5+gold[1]*5).ToString();
        shootpointui[1].text=(ShootPoint[2]+ShootPoint[3]+gold[2]*5+gold[3]*5).ToString();
    }
    public void AddEgg(int playernum,int egg){
        gold[playernum]+=egg;
        shootpointui[0].text=(ShootPoint[0]+ShootPoint[1]+gold[0]*5+gold[1]*5).ToString();
        shootpointui[1].text=(ShootPoint[2]+ShootPoint[3]+gold[2]*5+gold[3]*5).ToString();
    }
    public void finish(int index){
        if(rank[index]>200f&&checkpointmanager[index].GetComponent<CheckpointController>().passedlastcheckpoint){
            finished[index]=true;
            finishAni[index].SetActive(true);
            FindObjectOfType<AudioManager>().Play("finish");
            rank[index]+=400*(4-leaderboard[index]);
        }
    }
    public void finishAll(){
        for (int i = 0; i < 4; i++)
        {
            if(!finished[i]){
                finished[i]=true;
                finishAni[i].SetActive(true);
                FindObjectOfType<AudioManager>().Play("finish");
                rank[i]+=400*(4-leaderboard[i]);
            }
        }
    }
    //show point changing
    private void ShowPointChange(int change){
        var inst =Instantiate(TeamAPointChangePrefab,Vector3.zero,Quaternion.identity);
        inst.transform.SetParent(TeamAPointParent,false);
        RectTransform rect=inst.GetComponent<RectTransform>();
        LeanTween.moveY(rect,endpoint.anchoredPosition.y,1.5f).setOnComplete(()=>{
            Destroy(inst);
        });
    }
    public int getrank(CheckpointController cpc){
        if(cpc.activeindex==0&&cpc.passedlastcheckpoint){
            return cpc.Checkpoints.Length;
        }
        return cpc.activeindex;
    }
}
