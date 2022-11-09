using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WarpDetect : MonoBehaviour
{
    public VisualEffect warpSpeedVfx;
    public MeshRenderer tunnelLight;
    public bool warpActive;
    public float rate=0.02f;
    public float delay=3f;
    // Start is called before the first frame update
    void Start()
    {
        warpSpeedVfx.Stop();
        warpSpeedVfx.SetFloat("WarpAmount",0);
        tunnelLight.material.SetFloat("_Active",0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            warpActive=true;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            warpActive=false;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
    }
    //warp par
    IEnumerator ActivateParticles(){
        if(warpActive){
            warpSpeedVfx.Play();
            float amount=warpSpeedVfx.GetFloat("WarpAmount");
            while(amount<1 & warpActive){
                amount=+rate;
                warpSpeedVfx.SetFloat("WarpAmount",amount);
                yield return new WaitForSeconds(0.1f);
            }
        }else{
            float amount=warpSpeedVfx.GetFloat("WarpAmount");
            while(amount>0 & !warpActive){
                amount=-rate;
                warpSpeedVfx.SetFloat("WarpAmount",amount);
                yield return new WaitForSeconds(0.1f);
                if(amount<=0+rate){
                    amount=0;
                    warpSpeedVfx.SetFloat("WarpAmount",amount);
                    warpSpeedVfx.Stop();
                }
            }
        }
    }
    //tunnel
    IEnumerator ActivateShader(){
        if(warpActive){
            yield return new WaitForSeconds (delay);
            float amount=tunnelLight.material.GetFloat("_Active");
            while(amount<1 & warpActive){
                amount=+rate;
                tunnelLight.material.SetFloat("_Active",amount);
                yield return new WaitForSeconds(0.1f);
            }
        }else{
            float amount=tunnelLight.material.GetFloat("_Active");
            while(amount>0 & !warpActive){
                amount=-rate;
                warpSpeedVfx.SetFloat("WarpAmount",amount);
                yield return new WaitForSeconds(0.1f);
                if(amount<=0+rate){
                    amount=0;
                    tunnelLight.material.SetFloat("_Active",amount);
                }
            }
        }
    }
}
