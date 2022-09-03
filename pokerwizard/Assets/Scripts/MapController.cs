using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> player;
    public GameObject minimap;
    public List<GameObject> playerhead;
    public float mprescaleamount;
    public Vector3 mpoffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos;
        Vector2 mapsize=minimap.GetComponent<RectTransform>().sizeDelta;
        Vector3 newpos;
        for(int i=0;i<4;i++){
            playerpos=player[i].transform.position;
            newpos.x=mapsize.y/2-playerpos.x/1000*mapsize.y;
            newpos.y=mapsize.x/2-playerpos.z/1000*mapsize.x;
            newpos.z=0;
            playerhead[i].GetComponent<RectTransform>().localPosition=newpos*mprescaleamount+mpoffset;
        }
    }
}
