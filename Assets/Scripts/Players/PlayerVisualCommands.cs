using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualCommands : MonoBehaviour
{
    private Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Vector3 _ls = transform.localScale;
        if ((Mathf.Sign(_ls.x) != Mathf.Sign(_rb.velocity.x)) && _rb.velocity.x != 0)
            transform.localScale = new Vector3(-1 * _ls.x, _ls.y, _ls.z);
    }
}
