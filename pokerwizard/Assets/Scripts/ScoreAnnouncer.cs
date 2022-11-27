using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAnnouncer : MonoBehaviour
{
    public ScoreContainer scoreContainer;
    public List<TextMeshProUGUI> rank;
    public List<TextMeshProUGUI> hitpoint;
    public List<TextMeshProUGUI> gold;
    public List<int>totalscore;
    public List<TextMeshProUGUI> total;
    public List<TextMeshProUGUI> teamtotal;
    public GameObject TeamAcrown;
    public GameObject TeamBcrown;
    public LoadScene sceneloader;
    public bool Awin;
    public bool Bwin;
    [SerializeField]
    public ParticleSystem CelParA;
    [SerializeField]
    public ParticleSystem CelParB;
    // Start is called before the first frame update
    void Start()
    {
        Awin=false;
        Bwin=false;
        scoreContainer=FindObjectOfType<ScoreContainer>();
        for (int i = 0; i < 4; i++)
        {
            rank[i].text=scoreContainer.rank[i].ToString();
            hitpoint[i].text=scoreContainer.hitpoint[i].ToString();
            gold[i].text=scoreContainer.gold[i].ToString();
            totalscore[i]=scoreContainer.hitpoint[i]+scoreContainer.gold[i]*5;
            switch(scoreContainer.rank[i]){
                case 1:
                totalscore[i]+=10;
                break;
                case 2:
                totalscore[i]+=7;
                break;
                case 3:
                totalscore[i]+=5;
                break;
                case 4:
                totalscore[i]+=2;
                break;
            }
            total[i].text=totalscore[i].ToString();
        }
        Compare();
        teamtotal[0].text=(totalscore[0]+totalscore[1]).ToString();
        teamtotal[1].text=(totalscore[2]+totalscore[3]).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Compare(){
        if(totalscore[0]+totalscore[1]>totalscore[2]+totalscore[3]){
            TeamAcrown.SetActive(true);
            TeamBcrown.SetActive(false);
            Awin=true;
            CelParA.Play();
        }
        if(totalscore[0]+totalscore[1]<totalscore[2]+totalscore[3]){
            TeamBcrown.SetActive(true);
            TeamAcrown.SetActive(false);
            Bwin=true;
            CelParB.Play();
        }
        if(totalscore[0]+totalscore[1]==totalscore[2]+totalscore[3]){
            TeamBcrown.SetActive(true);
            TeamAcrown.SetActive(true);
            Awin=true;
            Bwin=true;
            CelParA.Play();
            CelParB.Play();
        }
    }
    public void nextround(){
        sceneloader.LoadtheScene("Multiple_UI");
    }
}
