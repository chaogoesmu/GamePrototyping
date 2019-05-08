using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float growth = 0;
    public float maxSize = 100f;
    public float growthRate = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        growth += Time.deltaTime * growthRate;
        if (growth >= maxSize)
        {

            Vector3 randPoint = MinionAI.ForwardRandomNavSphere(this.transform.position, 20,-1);
            //spawn a tree
            Debug.DrawRay(randPoint, Vector3.up * 30, Color.green, 5.0f);
            growth = 0f;
        }
    }

}
