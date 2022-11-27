    using System.Collections.Generic;
    using UnityEngine;
    using Cinemachine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class PlayerManager : MonoBehaviour
    {
        public List<GameObject>characters;
        public List<int>playerheadnumber;
        public List<GameObject>Cursors;
        public List<GameObject>playerheads;
        public GameObject minimap;
        public GamePlayUIMulti mirrorController;
        public ScoreManager scoreManager;
        private List<PlayerInput> players = new List<PlayerInput>();
        public List<PlayerInput> Players=>players;
        [SerializeField]
        private List<Transform> startingPoints;
        [SerializeField]
        private List<LayerMask> playerLayers;
        [SerializeField]
        private List<LayerMask> enemyLayers;
        [SerializeField]
        private List<LayerMask> FriendLayers;
        private PlayerInputManager playerInputManager;
        public List<GameObject>checkpointmanagers;
        public static PlayerManager instance;

        //ready
        public GameObject readyUI;
        public GameObject goIcon;
        public bool allReady;
        public bool isDissolving;
        public float countdown;
        private Material mat;
        private float fadeval;
        LoadScene loader;

        private void Awake()
        {
            
            if(instance==null&&!GetComponent<EventSystem>()){
                instance=this;
            }
            else if(!GetComponent<EventSystem>())
            {
                Destroy(gameObject);
                return;
            }
            if(!GetComponent<EventSystem>()){
                DontDestroyOnLoad(this.gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
                playerInputManager = FindObjectOfType<PlayerInputManager>();
            }
        }
        void Update(){
            //go dissolve and play
            if(Keyboard.current.lKey.wasPressedThisFrame){
                allReady=true;
                readyUI.SetActive(true);
            }
            if(Keyboard.current.bKey.wasPressedThisFrame){
                readyUI.SetActive(false);
            }
            if(Keyboard.current.pKey.wasPressedThisFrame&&allReady){
                isDissolving=true;
            }
            if(isDissolving){
                fadeval-=Time.deltaTime;
                if(fadeval<0f){
                    fadeval=0f;
                    isDissolving=false;
                }
                mat.SetFloat("_FadeValue",fadeval);
            }
            //sample scene
            if(Keyboard.current.kKey.wasPressedThisFrame){
                FindObjectOfType<AudioManager>().Play("ready_game");
                loader=FindObjectOfType<LoadScene>();
                if(loader){
                    loader.LoadtheScene("SampleScene");
                }
                else{
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode){
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);
            scoreManager=FindObjectOfType<ScoreManager>();
            PlayerManager retiredplayerman=FindObjectOfType<EventSystem>().GetComponent<PlayerManager>();
            if(retiredplayerman){
                this.startingPoints=retiredplayerman.startingPoints;
                for(int i=0;i<players.Count;i++)
                {
                    playerheads[i]=retiredplayerman.playerheads[playerheadnumber[i]];
                }
                this.Cursors=retiredplayerman.Cursors;
                this.minimap=retiredplayerman.minimap;
                this.mirrorController=retiredplayerman.mirrorController;
                this.checkpointmanagers=retiredplayerman.checkpointmanagers;
                this.scoreManager=retiredplayerman.scoreManager;
                this.readyUI=retiredplayerman.readyUI;
                this.goIcon=retiredplayerman.goIcon;
            }
            countdown=4f;
            isDissolving=false;
            allReady=false;
            if(readyUI)readyUI.SetActive(false);
            if(goIcon)mat=goIcon.GetComponent<Image>().material;
            mat.SetFloat("_FadeValue",1f);
            GetComponent<ScoreContainer>().scoreManager=scoreManager;
            if(scoreManager){
                scoreManager.scoreContainer=this.GetComponent<ScoreContainer>();
            }
            foreach (var player in players)
            {
                ShootingController sc=player.transform.parent.GetComponentInChildren<ShootingController>();
                if(sc){
                    Debug.Log(sc);
                    sc.sa=FindObjectOfType<ScoreAnnouncer>();
                }
                ScrollDown sd=FindObjectOfType<ScrollDown>();
                if(sd){
                    player.transform.parent.GetComponentInChildren<ShootingController>().SD=sd;
                }
                if(player.playerIndex>=0&&Cursors.Count>=0&&Cursors.Count>player.playerIndex&&Cursors[player.playerIndex]!=null){
                    player.GetComponentInChildren<PlayerController>().playerCursor=Cursors[player.playerIndex];
                }
                if(player.GetComponentInChildren<PlayerController>().Normal.childCount>0){
                    Destroy(player.GetComponentInChildren<PlayerController>().Normal.GetChild(0).gameObject);
                }
                player.GetComponent<PlayerController>().transform.parent.GetComponentInChildren<CinemachineVirtualCamera>().OnTargetObjectWarped(player.GetComponent<PlayerController>().Normal,startingPoints[player.playerIndex].position-player.GetComponent<PlayerController>().rb.transform.position);
                player.GetComponent<PlayerController>().rb.transform.position=startingPoints[player.playerIndex].position;
                player.GetComponent<PlayerController>().transform.forward=Vector3.back;
                GameObject playerchar=Instantiate(characters[player.playerIndex]);
                playerchar.transform.SetParent(player.GetComponentInChildren<PlayerController>().Normal,false);
                playerchar.tag="Player";
                player.transform.parent.GetComponentInChildren<ShootingController>().bullet=new GameObject("Bullet");
                player.transform.parent.GetComponentInChildren<ShootingController>().bulleteffect=new GameObject("Effect");
                player.transform.parent.GetComponentInChildren<ShootingController>().bulletendeffect=new GameObject("Boom");
                player.GetComponentInChildren<PlayerController>().playerlayer=playerLayers[player.playerIndex];
                player.GetComponentInChildren<PlayerController>().playerNumber=player.playerIndex;
                player.GetComponentInChildren<PlayerController>().TeamNumber=Mathf.FloorToInt(player.playerIndex/2f);
                player.GetComponentInChildren<PlayerController>().playerModel=playerchar.transform;
                //need to use the parent due to the structure of the prefab
                Transform playerParent = player.transform.parent;
                minimap.GetComponent<MapController>().player.Add(player.GetComponent<PlayerController>().Normal.gameObject);
                minimap.GetComponent<MapController>().playerhead.Add(playerheads[player.playerIndex]);
                checkpointmanagers[player.playerIndex].GetComponent<CheckpointController>().player=player.GetComponent<PlayerController>().Normal.gameObject;
                //playerParent.position = startingPoints[player.playerIndex].position;

                //convert layer mask (bit) to an integer 
                
                int layerToAdd = (int)Mathf.Log(playerLayers[player.playerIndex].value, 2);
                playerchar.layer=layerToAdd;
                foreach (var vm in playerParent.GetComponentsInChildren<CinemachineVirtualCamera>(true))
                {
                    vm.gameObject.layer=layerToAdd;
                }
                playerParent.GetComponentInChildren<ShootingController>().mousecolliderlayermask|= (1 << layerToAdd);
                playerParent.GetComponentInChildren<ShootingController>().enemyLayer=enemyLayers[player.playerIndex];
                playerParent.GetComponentInChildren<ShootingController>().FriendLayer=FriendLayers[player.playerIndex];
                playerParent.GetComponentInChildren<ShootingController>().mirrorController=mirrorController;
                playerParent.GetComponentInChildren<ShootingController>().animateplayer=playerchar;
                playerParent.GetComponentInChildren<ShootingController>().scoreManager=scoreManager;
                playerParent.GetComponentInChildren<ShootingController>().aim=mirrorController.aim[player.playerIndex];
                Debug.Log(playerParent.GetComponentInChildren<ShootingController>().animateplayer);
                //set the layer
                //playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
                //add the layer
                playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
                //set the action in the custom cinemachine Input Handler
                //playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");
                if(player.playerIndex==1){
                    Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                    playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                    playerParent.GetComponentInChildren<ShootingController>().friend=players[0].gameObject;
                    players[0].transform.parent.GetComponentInChildren<ShootingController>().friend=player.gameObject;
                }
                if(player.playerIndex==2){
                    playerParent = players[1].transform.parent;
                    Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                    playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                    playerParent = players[2].transform.parent;
                    cmrect=new Rect(0.5f,0.5f,0.5f,0.5f);
                    playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                    players[0].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[2].gameObject);
                    players[1].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[2].gameObject);
                    players[2].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[0].gameObject);
                    players[2].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[1].gameObject);
                }
                if(player.playerIndex==3){
                    playerParent = players[1].transform.parent;
                    Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                    playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                    playerParent = players[2].transform.parent;
                    cmrect=new Rect(0.5f,0.5f,0.5f,0.5f);
                    playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                    players[2].transform.parent.GetComponentInChildren<ShootingController>().friend=players[3].gameObject;
                    players[3].transform.parent.GetComponentInChildren<ShootingController>().friend=players[2].gameObject;
                    players[0].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[3].gameObject);
                    players[1].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[3].gameObject);
                    players[3].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[0].gameObject);
                    players[3].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[1].gameObject);
                }
            }
        }

        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += AddPlayer;
        }

        private void OnDisable()
        {
            playerInputManager.onPlayerJoined -= AddPlayer;
        }

        public void AddPlayer(PlayerInput player)
        {
            players.Add(player);
            player.GetComponentInChildren<PlayerController>().playerCursor=Cursors[player.playerIndex];
            /*
            Debug.Log(player.GetComponentInChildren<PlayerController>().Normal.GetChild(0).gameObject);
            Destroy(player.GetComponentInChildren<PlayerController>().Normal.GetChild(0).gameObject);
            GameObject playerchar=Instantiate(characters[player.playerIndex]);
            playerchar.transform.SetParent(player.GetComponentInChildren<PlayerController>().Normal,false);
            playerchar.tag="Player";
            
            player.GetComponentInChildren<PlayerController>().playerlayer=playerLayers[player.playerIndex];
            player.GetComponentInChildren<PlayerController>().playerNumber=player.playerIndex;
            player.GetComponentInChildren<PlayerController>().playerModel=playerchar.transform;
            //need to use the parent due to the structure of the prefab
            Transform playerParent = player.transform.parent;
            minimap.GetComponent<MapController>().player.Add(player.GetComponent<PlayerController>().Normal.gameObject);
            minimap.GetComponent<MapController>().playerhead.Add(playerheads[player.playerIndex]);
            checkpointmanagers[player.playerIndex].GetComponent<CheckpointController>().player=player.GetComponent<PlayerController>().Normal.gameObject;
            //playerParent.position = startingPoints[player.playerIndex].position;

            //convert layer mask (bit) to an integer 
            
            int layerToAdd = (int)Mathf.Log(playerLayers[player.playerIndex].value, 2);
            playerchar.layer=layerToAdd;
            playerParent.GetComponentInChildren<ShootingController>().mousecolliderlayermask|= (1 << layerToAdd);
            playerParent.GetComponentInChildren<ShootingController>().enemyLayer=enemyLayers[player.playerIndex];
            playerParent.GetComponentInChildren<ShootingController>().FriendLayer=FriendLayers[player.playerIndex];
            playerParent.GetComponentInChildren<ShootingController>().mirrorController=mirrorController;
            playerParent.GetComponentInChildren<ShootingController>().animateplayer=playerchar;
            Debug.Log(playerParent.GetComponentInChildren<ShootingController>().animateplayer);
            //set the layer
            playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
            //add the layer
            playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
            //set the action in the custom cinemachine Input Handler
            //playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");
            if(player.playerIndex==2){
                Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                playerParent.GetComponentInChildren<ShootingController>().friend=players[0].gameObject;
                players[0].transform.parent.GetComponentInChildren<ShootingController>().friend=player.gameObject;
            }
            if(player.playerIndex==3){
                playerParent = players[1].transform.parent;
                Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                playerParent = players[2].transform.parent;
                cmrect=new Rect(0.5f,0.5f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                players[0].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[2].gameObject);
                players[1].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[2].gameObject);
                players[2].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[0].gameObject);
                players[2].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[1].gameObject);
            }
            if(player.playerIndex==4){
                playerParent = players[1].transform.parent;
                Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                playerParent = players[2].transform.parent;
                cmrect=new Rect(0.5f,0.5f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
                players[2].transform.parent.GetComponentInChildren<ShootingController>().friend=players[3].gameObject;
                players[3].transform.parent.GetComponentInChildren<ShootingController>().friend=players[2].gameObject;
                players[0].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[3].gameObject);
                players[1].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[3].gameObject);
                players[3].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[0].gameObject);
                players[3].transform.parent.GetComponentInChildren<ShootingController>().enemy.Add(players[1].gameObject);
            }
            */
        }
        public void RemovePlayer(PlayerInput player)
        {
            players.Remove(player);
        }
    }

