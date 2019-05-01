using System.Collections;
using UnityEngine;

public class Wander : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent agent; //= GetComponent<UnityEngine.AI.NavMeshAgent>();
    public float wanderRadius;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent == null)
        {
            Debug.Log("agent is null, something went wrong");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)//boolean on whether it has a path to follow
        {

            if (agent.remainingDistance <= agent.stoppingDistance)//check to see if we should be stopped
            {
                Debug.Log(this + "reached destination: "+ agent.destination);

                if (!agent.hasPath)//if you dont have a path, or not moving
                {
                    Debug.Log(this + "No path currently" + agent.destination);
                }
                Vector3 newPos = ForwardRandomNavSphere(this.transform.position+transform.forward*(wanderRadius/3), wanderRadius, -1);
                agent.destination = newPos;
                Debug.Log(this + " changed destination to: " + agent.destination);
                Debug.DrawRay(agent.destination, Vector3.up*30, Color.blue, 1.0f);
            }
            return;
        }
        Debug.Log(this + "pending path" + Time.time);
        return;
    }

    /// <summary>
    /// find a random point on the layermask in front of the origin
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="dist"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>
    public static Vector3 ForwardRandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * Random.Range(2, dist);
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
