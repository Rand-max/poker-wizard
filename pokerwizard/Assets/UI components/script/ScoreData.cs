using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreData : MonoBehaviour
{
    //flag texture
    public RawImage[] fasten_img=new RawImage[4];
    public Texture[] fasten_tex=new Texture[4];

    public RawImage[] flag_img=new RawImage[4];
    public Texture[] flag_tex=new Texture[4];

    private int curentItem=0;


    //drop flag
    //get data
    //data appear animation


    public void ChangeFlag(){
        // for(int i=0;i<4;i++){
        //     flag_img[i].texture=flag_tex[i+1];
        //     fasten_img[i].texture=fasten_tex[i+1];
        // }
        flag_img[0].texture=flag_tex[1];
        fasten_img[0].texture=fasten_tex[1];

        flag_img[1].texture=flag_tex[2];
        fasten_img[1].texture=fasten_tex[2];

        flag_img[2].texture=flag_tex[3];
        fasten_img[2].texture=fasten_tex[3];

        flag_img[3].texture=flag_tex[0];
        fasten_img[3].texture=fasten_tex[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++){
            flag_img[i].texture=flag_tex[i];
            fasten_img[i].texture=fasten_tex[i];
        }
        //flag_img.texture=flag_tex[currentItem];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
