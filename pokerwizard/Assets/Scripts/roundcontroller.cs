using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class roundcontroller : MonoBehaviour
{
    public int playercount;
    public GameObject lap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lap!=null)GetComponent<TextMeshProUGUI>().text=lap.GetComponent<CheckpointController>().lap.ToString();
    }
}
