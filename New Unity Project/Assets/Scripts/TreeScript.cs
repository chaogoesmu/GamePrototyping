using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float growth = 0;
    public float energy = 0;
    public float maxEnergy = 100f;
    public float growthScaling = .1f;
    public float energyRate = .1f;
    public float maxSize = 100f;
    public float growthRate = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energy+= Time.deltaTime * energyRate;//tree gathers energy, when it's full on energy it grows
        if (energy >= maxEnergy)
        {
            energy = 0;
            growth += growthRate;
            if (growth >= maxSize)//if a tree is fully grown, it spawns
            {
                Vector3 randPoint = MinionAI.ForwardRandomNavSphere(this.transform.position, 20, -1);

                Debug.DrawRay(randPoint, Vector3.up * 30, Color.green, 5.0f);
                //TODO:spawn a tree
                //TODO: create a tree with an origin point at the bottom, may need to attach it to a blank gameobject
            }
        }
    }

}
