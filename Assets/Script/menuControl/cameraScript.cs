using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private Transform player;
    private float minX = 2.68f, maxX = 193;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        if(player != null && player.transform.position.y > -10)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = player.position.x;
            //cameraPosition.y = player.position.y;
            if (cameraPosition.x < minX) cameraPosition.x = 2.68f;
            if (cameraPosition.x > maxX) cameraPosition.x = maxX;
            //if (cameraPosition.y > -6 && cameraPosition.y < -19) cameraPosition.y = player.position.y;
            transform.position = cameraPosition;
        }
        else if(player != null && player.transform.position.y < -10)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = player.position.x;
            cameraPosition.y = player.position.y;
            if (cameraPosition.x < minX) cameraPosition.x = 136.87f;
            if (cameraPosition.y < -10) cameraPosition.y = -12;
            if (cameraPosition.y > -6 && cameraPosition.y < -19) cameraPosition.y = player.position.y;
            transform.position = cameraPosition;
        }
    }
}
