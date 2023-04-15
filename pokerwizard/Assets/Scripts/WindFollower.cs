using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class WindFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    float distance;
    // Update is called once per frame
    void Update()
    {
        distance+=speed*Time.deltaTime;
        transform.position=pathCreator.path.GetPointAtDistance(distance);
        transform.rotation=pathCreator.path.GetRotationAtDistance(distance);
    }
}
