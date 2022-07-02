using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clowncontroller : MonoBehaviour
{
    public Animator anim;
    bool generated=false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        anim=GetComponent<Animator>();
        while (true)
        {
            yield return new WaitForSeconds(3);
            anim.SetInteger("movingindex",Random.Range(0,2));
            anim.SetTrigger("moving");
        }
    }
    // void Start(){
    //     anim=GetComponent<Animator>();
    // }
    // void Update(){
    //     if(anim.GetCurrentAnimatorStateInfo(0).IsName("Armature_0idle") && !generated)
    //     {
    //         anim.SetInteger("movingindex",Random.Range(0,2));
    //         anim.SetTrigger("moving");
    //         generated=true;

    //     }
        
    // }
    // public void animation_event()
    // {
    //     generated=false;
    // }
    // Update is called once per frame
}
