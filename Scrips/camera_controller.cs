using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{

    //public GameObject player;
    public Transform player;

    private Vector3 offset;

    // Use this for initialization
    void Start ()
    {
        offset = transform.position - player.transform.position;

        //transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
   
}
