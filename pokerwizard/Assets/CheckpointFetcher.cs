using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class CheckpointFetcher : MonoBehaviour
{
    public CheckpointController cpc;
    public bool FetchNow;
    // Start is called before the first frame update
    #if UNITY_EDITOR
    void Start()
    {
        cpc=GetComponent<CheckpointController>();
    }

    // Update is called once per frame
    
    void Update()
    {
        if(FetchNow){
            List<GameObject> checks=new List<GameObject>();
            foreach (var item in GetComponentsInChildren<cpController>())
            {
                checks.Add(item.gameObject);
            }
            cpc.Checkpoints=checks.ToArray();
        }
    }
    #endif
}
