using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y+3, transform.position.z);
    }
}
