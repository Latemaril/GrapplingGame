using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sinus = Mathf.Sin(Time.time);
        float d = Mathf.Abs(sinus);
        float t = Mathf.Lerp(1, 2, d);
        transform.localScale = new Vector3(t, t, t);
    }
}
