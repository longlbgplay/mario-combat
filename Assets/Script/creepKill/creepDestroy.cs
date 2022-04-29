using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(creepDisappear());
    }
    IEnumerator creepDisappear()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
