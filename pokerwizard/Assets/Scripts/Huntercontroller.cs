using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Huntercontroller : MonoBehaviour
{
    public Animator anim;
    public VisualEffect accelbuf;

    private bool isaccellering;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //accel
        if(anim!=null){
            if(Input.GetButtonDown("Fire1")&&!isaccellering){
                anim.SetTrigger("Accel");

                if(accelbuf!=null){
                    accelbuf.Play();
                }
                isaccellering=true;
                StartCoroutine(ResetBool(isaccellering,0.5f));
            }
        }
    }

    IEnumerator ResetBool(bool boolToReset,float delay=0.1f){
        yield return new WaitForSeconds(delay);
        isaccellering=!isaccellering;
    }
}
