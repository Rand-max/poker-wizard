    using System.Collections.Generic;
    using UnityEngine;
    using Cinemachine;
    using UnityEngine.InputSystem;

    public class PlayerManager : MonoBehaviour
    {
        public List<GameObject>characters;
        public GameObject minimap;
        private List<PlayerInput> players = new List<PlayerInput>();
        [SerializeField]
        private List<Transform> startingPoints;
        [SerializeField]
        private List<LayerMask> playerLayers;
        private PlayerInputManager playerInputManager;
        public List<GameObject>checkpointmanagers;

        private void Awake()
        {
            playerInputManager = FindObjectOfType<PlayerInputManager>();
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
            Debug.Log(player.GetComponentInChildren<PlayerController>().Normal.GetChild(0).gameObject);
            Destroy(player.GetComponentInChildren<PlayerController>().Normal.GetChild(0).gameObject);
            GameObject playerchar=Instantiate(characters[players.Count - 1]);
            playerchar.transform.SetParent(player.GetComponentInChildren<PlayerController>().Normal,false);
            player.GetComponentInChildren<PlayerController>().playerlayer=playerLayers[players.Count - 1];
            //need to use the parent due to the structure of the prefab
            Transform playerParent = player.transform.parent;
            minimap.GetComponent<MapController>().player.Add(player.GetComponent<PlayerController>().Normal.gameObject);
            checkpointmanagers[players.Count-1].GetComponent<CheckpointController>().player=player.GetComponent<PlayerController>().Normal.gameObject;
            //playerParent.position = startingPoints[players.Count - 1].position;

            //convert layer mask (bit) to an integer 
            
            int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

            //set the layer
            playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
            //add the layer
            playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
            //set the action in the custom cinemachine Input Handler
            //playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");
            if(players.Count==2){
                Rect cmrect=new Rect(0f,0f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
            }
            if(players.Count==3){
                Rect cmrect=new Rect(0.5f,0.5f,0.5f,0.5f);
                playerParent.GetComponentInChildren<Camera>().rect=cmrect;
            }
        }
    }

