using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUIMulti : MonoBehaviour
{
    /*
    icon
        0. fire
        1. accel
        2. shield
        3. ice
        4. chaos
        5. thunder
    */
    //spell icon
    public GameObject[] SpellIcon=new GameObject[4];
    public Texture[] si_tex;
    public Color[] si_color;
    Material[] SpellMat=new Material[4];

    //spell text
    public TextMeshProUGUI[] spellname=new TextMeshProUGUI[4];
    public VertexGradient[] st_color;

    //place text
    public TextMeshProUGUI[] placeNum=new TextMeshProUGUI[4];
    public TextMeshProUGUI[] placeEng=new TextMeshProUGUI[4];

    //ripple
    [SerializeField]
    public ParticleSystem[] ripeffect=null;

    void Start()
    {
        //player material declare
        for(int i=0;i<6;i++){
            SpellMat[i]=SpellIcon[i].GetComponent<Image>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
