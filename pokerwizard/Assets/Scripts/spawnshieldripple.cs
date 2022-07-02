using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class spawnshieldripple : MonoBehaviour
{
    public GameObject shieldRipple;

    private VisualEffect shieldripplevfx; 
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="bullet"){
            var ripples=Instantiate(shieldRipple,transform) as GameObject;
            shieldripplevfx=ripples.GetComponent<VisualEffect>();
            shieldripplevfx.SetVector3("sphere_center",col.contacts[0].point);
            Destroy(ripples,2);
        }
    }
}
