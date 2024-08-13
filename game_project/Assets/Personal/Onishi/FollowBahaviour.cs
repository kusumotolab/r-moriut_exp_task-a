using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBahaviour : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private GameObject followObject;
    
    // Start is called before the first frame update
    void Start()
    {
        followObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (followObject != null)
        {
            gameObject.transform.position = followObject.transform.position + offset;
        }
    }
}
