using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualCommands : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 _ls;
    private Transform Transform;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        TryGetComponent(out Transform);
    }

    
    void Update()
    {
        _ls = Transform.localScale;
        if ((Mathf.Sign(_ls.x) != Mathf.Sign(_rb.velocity.x)) && _rb.velocity.x != 0)
            Transform.localScale = new Vector3(-1 * _ls.x, _ls.y, _ls.z);
    }


}
