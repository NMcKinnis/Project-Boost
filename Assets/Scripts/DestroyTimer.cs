using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float timeTillDestruction = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginDestructionTimer());
    }

    private IEnumerator BeginDestructionTimer()
    {
        yield return new WaitForSeconds(timeTillDestruction);
        Destroy(gameObject);
    }
}
