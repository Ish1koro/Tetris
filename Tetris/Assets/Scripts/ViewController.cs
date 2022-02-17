using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private Map _map = default;

    private void Awake()
    {
        _map = GetComponent<Map>();
    }

    public void Move()
    {
        
    }
}
