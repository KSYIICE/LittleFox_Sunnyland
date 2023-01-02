using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public void Get()
    {
        FindObjectOfType<PlayerController>().CherryGet();
        Destroy(gameObject);
    }
}
