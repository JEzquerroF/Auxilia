using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSec : MonoBehaviour
{
    private void Start()
    {
        
        Destroy(gameObject, 1f);
    }
}
