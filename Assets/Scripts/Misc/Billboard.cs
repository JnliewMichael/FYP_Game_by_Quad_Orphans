using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private void Start()
    {

    }
    private void LateUpdate()
    {
        transform.LookAt(playerTransform.position);
    }
}
