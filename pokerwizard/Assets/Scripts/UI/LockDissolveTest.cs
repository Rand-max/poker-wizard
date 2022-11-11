using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockDissolveTest : MonoBehaviour
{
    //key
    public PlayerManager playerManager;
    public List<GameObject>keys;
    public List<GameObject>players;
    public List<int>playerhead;
    //lock
    public List<GameObject> locks;
    public ReadyCallOut red;
    
    [SerializeField]
    private List<Material> Mat;

    private List<float> fadeval=new List<float>(4){1.5f,1.5f,1.5f,1.5f};
    private List<float> countdown=new List<float>(4){1f,1f,1f,1f};
    List<bool> counting=new List<bool>(4){false,false,false,false};
    List<bool> isDissolving=new List<bool>(4){false,false,false,false};
    private List<bool> isEnabled=new List<bool>(4){true,true,true,true};

    //text
    public List<TextMeshProUGUI> playernum;

    public TextMeshProUGUI Connected1;
    public TextMeshProUGUI Connected2;
    public TextMeshProUGUI Connected3;
    public TextMeshProUGUI Connected4;
    public TextMeshProUGUI p1_text;
    public TextMeshProUGUI p2_text;
    public TextMeshProUGUI p3_text;
    public TextMeshProUGUI p4_text;

    //circle
    private List<bool> circleAppear=new List<bool>(4){false,false,false,false};
    public List<Image> cirimg;

    [SerializeField]
    private List<Color> circolor;

    public void ButtonClicked(CursorKey cursor,GameObject locker){
        //lock
        FindObjectOfType<AudioManager>().Play("unlock");
        //controller
        /*Connected1.text="Connected !";
        Connected2.text="Connected !";
        Connected3.text="Connected !";
        Connected4.text="Connected !";
        p1_text.text="1p";
        p2_text.text="2p";
        p3_text.text="3p";
        p4_text.text="4p";*/
        //key
        int cursornum=0;
        for(int i=0;i<4;i++){
            if(cursor.gameObject==keys[i]){
                isEnabled[i]=!isEnabled[i];
                keys[i].SetActive(isEnabled[i]);
                cursornum=i;
            }
        }
        for(int i=0;i<4;i++){
            if(locker==locks[i].transform.parent.gameObject){
                fadeval[i]=1.5f;
                countdown[i]=1f;
                playernum[i].text=cursor.cursorname;
                isDissolving[i]=true;
                circleAppear[i]=true;
                playerManager.characters[cursornum]=players[i];
                playerManager.playerheadnumber[cursornum]=i;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        Connected1.text="Connected !";
        Connected2.text="Connected !";
        Connected3.text="Connected !";
        Connected4.text="Connected !";
        p1_text.text="1p";
        p2_text.text="2p";
        p3_text.text="3p";
        p4_text.text="4p";
        for (int i = 0; i < 4; i++)
        {
            isEnabled[i]=true;
            Mat[i]=locks[i].GetComponent<Image>().material;
            Mat[i].SetFloat("_FadeValue",1f);
            circleAppear[i]=false;
            circolor[i]= new Color (1, 1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<4;i++){
            if(isDissolving[i]){
                fadeval[i]-=Time.deltaTime;
                if(fadeval[i]<0f){
                    fadeval[i]=0f;
                    isDissolving[i]=false;
                }
                Mat[i].SetFloat("_FadeValue",fadeval[i]);
            }
            if(circleAppear[i]){
                countdown[i] -=Time.deltaTime;
                if(countdown[i]<0f){
                    countdown[i]=0f;
                    circleAppear[i]=false;
                    cirimg[i].color=circolor[i];
                }
                circolor[i]= new Color (1, 1, 1, 1);
            }
        }
        bool getReady=true;
        for(int i=0;i<4;i++){
            if(isEnabled[i]){
                getReady=false;
            }
        }
        if(getReady){
            red.ButtonClicked();
        }
    }
    
}
