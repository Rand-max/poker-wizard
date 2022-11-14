    using System.Collections.Generic;
    using UnityEngine;
    using Cinemachine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;
    using UnityEngine.EventSystems;

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

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }
        void Update(){
            if(Keyboard.current.kKey.wasPressedThisFrame){
                SceneManager.LoadScene("SampleScene");
            }
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode){
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);
            PlayerManager retiredplayerman=FindObjectOfType<EventSystem>().GetComponent<PlayerManager>();
            this.startingPoints=retiredplayerman.startingPoints;
            for(int i=0;i<players.Count;i++)
            {
                Debug.Log(retiredplayerman.playerheads[0]);
                playerheads[i]=retiredplayerman.playerheads[playerheadnumber[i]];
            }
            this.minimap=retiredplayerman.minimap;
            this.mirrorController=retiredplayerman.mirrorController;
            this.checkpointmanagers=retiredplayerman.checkpointmanagers;
            this.scoreManager=retiredplayerman.scoreManager;
            GetComponent<ScoreContainer>().scoreManager=scoreManager;
            foreach (var player in players)
            {
                Destroy(player.GetComponentInChildren<PlayerController>().Normal.GetChild(0).gameObject);
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
                    vm.gameObject.layer=playerLayers[player.playerIndex];
                }
                playerParent.GetComponentInChildren<Camera>().cullingMask|= (1 << layerToAdd);
                playerParent.GetComponentInChildren<ShootingController>().mousecolliderlayermask|= (1 << layerToAdd);
                playerParent.GetComponentInChildren<ShootingController>().enemyLayer=enemyLayers[player.playerIndex];
                playerParent.GetComponentInChildren<ShootingController>().FriendLayer=FriendLayers[player.playerIndex];
                playerParent.GetComponentInChildren<ShootingController>().mirrorController=mirrorController;
                playerParent.GetComponentInChildren<ShootingController>().animateplayer=playerchar;
                playerParent.GetComponentInChildren<ShootingController>().scoreManager=scoreManager;
                Debug.Log(playerParent.GetComponentInChildren<ShootingController>().animateplayer);
                //set the layer
                playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
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
    }

