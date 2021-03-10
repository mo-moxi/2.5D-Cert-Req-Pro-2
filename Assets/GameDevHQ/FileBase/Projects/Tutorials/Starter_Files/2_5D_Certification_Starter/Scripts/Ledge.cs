using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] 
    private Vector3 _handPos, _standPos, _handOffSet, _standOffSet;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                _handPos = transform.position + _handOffSet;
                player.GrabLedge(_handPos, this);
            }
        }
    }
    public Vector3 GetStandPos()
    {
        _standPos = transform.position + _standOffSet;
        Debug.Log("Std_Pos " + _standPos);
        return _standPos;
    }
}
