using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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

    public List<TextMeshProUGUI> ConnectedText;
    public List<TextMeshProUGUI> player_text;
    public bool everyoneReady=false;

    //circle
    private List<bool> circleAppear=new List<bool>(4){false,false,false,false};
    public List<Image> cirimg;

    [SerializeField]
    private List<Color> circolor;

    public void ButtonClicked(CursorKey cursor,GameObject locker,Texture playertexture,Texture playerfasten){
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
                playerManager.Players[cursornum].GetComponent<PlayerController>().playerTexture=playertexture;
                playerManager.Players[cursornum].GetComponent<PlayerController>().playerfastenTexture=playerfasten;
                playerManager.playerheadnumber[cursornum]=i;
                playerManager.playerReady[cursornum]=true;
            }
        }
    }
    void Awake(){
        SceneManager.sceneLoaded+=OnSceneLoaded;
        Mat[0]=locks[0].GetComponent<Image>().material;
        Mat[0].SetFloat("_FadeValue",1f);
        Mat[1]=locks[1].GetComponent<Image>().material;
        Mat[1].SetFloat("_FadeValue",1f);
        Mat[2]=locks[2].GetComponent<Image>().material;
        Mat[2].SetFloat("_FadeValue",1f);
        Mat[3]=locks[3].GetComponent<Image>().material;
        Mat[3].SetFloat("_FadeValue",1f);
    }
    // Start is called before the first frame update
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("loaded");
        for (int i = 0; i < 4; i++)
        {
            isEnabled[i]=true;
            Mat[i]=locks[i].GetComponent<Image>().material;
            Mat[i].SetFloat("_FadeValue",1f);
            circleAppear[i]=false;
            circolor[i]= new Color (1, 1, 1, 0);
            ConnectedText[i].text="";
            player_text[i].text="";
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerManager==null){
            foreach (var plm in FindObjectsOfType<PlayerManager>())
            {
                if(!plm.isOld){
                    playerManager=plm;
                }
            }
            if(playerManager==null){
                Debug.Log("No pm");
                return;
            }
        }
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
        /*
        bool getReady=true;
        for(int i=0;i<4;i++){
            if(isEnabled[i]){
                getReady=false;
            }
        }
        if(getReady){
            red.ButtonClicked();
        }*/
        // if(Mat[1].GetFloat("_FadeValue")==0){
        //     everyoneReady=true;
        // }
    }
    public void playerConnected(int ptext){
        FindObjectOfType<AudioManager>().Play("pick_key");
        ConnectedText[ptext].text="Connected !";
        player_text[ptext].text=(ptext+1)+"p";
    }
}
