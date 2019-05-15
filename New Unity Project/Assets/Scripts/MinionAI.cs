using System.Collections;
using UnityEngine;

public class MinionAI : MonoBehaviour
{


    private UnityEngine.AI.NavMeshAgent agent; //= GetComponent<UnityEngine.AI.NavMeshAgent>();
    public float wanderRadius;
    public enum Actions { undecided, idle, wander, busy }
    public Actions currentAction;
    private int ActionsLength = 4;
    public float idleTimeMax = 5f;
    public float ifleTimeMin = 15f;
    public int health;
    public int maxHealth;
    public int energy;
    public int maxEnergy;
    public float speed;
    public float energyUsage;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            currentAction = Actions.wander;
            Debug.Log("current action: "+currentAction + " " + currentAction);
            energyUsage = (maxHealth * speed)/1000;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAction)
        {
            case Actions.wander:
                wandering();
                break;
            case Actions.idle:
                StartCoroutine(idle(Random.Range(idleTimeMax, ifleTimeMin)));
                break;
            case Actions.busy:
                break;
            case Actions.undecided:
                Debug.Log("deciding action");
                currentAction = Actions.busy;
                while (currentAction == Actions.busy)
                {
                    //var val = Enum.GetNames(Actions);
                    currentAction = (Actions)Mathf.Floor(Random.value*ActionsLength);
                    Debug.Log("new action: " + currentAction);
                }
                
                break;
            default:
                Debug.Log("no action taken: "+ currentAction);
                break;
        }
    }

    private IEnumerator idle(float time)
    {
        Debug.Log("idling for: " + time);
        currentAction = Actions.busy;
        yield return new WaitForSeconds((int)time);

        // Code to execute after the delay
        //idle animation
        currentAction = Actions.undecided;
    }


    private void wandering()
    {
        if (agent.isStopped)
        {
            Vector3 newPos = ForwardRandomNavSphere(this.transform.position + transform.forward * (wanderRadius / 3), wanderRadius, -1);
            agent.destination = newPos;
            Debug.Log(this + " changed destination to: " + agent.destination);
            Debug.DrawRay(agent.destination, Vector3.up * 30, Color.blue, 5.0f);
            agent.isStopped = false;
            currentAction = Actions.wander;
        }
        if (!agent.pathPending)//boolean on whether it has a path to follow
        {
            //use energy here
            energy = energyUsage * Time.deltatime;

            if (agent.remainingDistance <= agent.stoppingDistance)//check to see if we should be stopped
            {
                Debug.Log(this + "reached destination: " + agent.destination);

                if (!agent.hasPath)//if you dont have a path, or not moving
                {
                    Debug.Log(this + "No path currently" + agent.destination);
                }
                agent.isStopped = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero; //agent was drifting occasionally after attempting to stop.
                currentAction = Actions.undecided;
            }
            return;
        }
        Debug.Log(this + "pending path" + Time.time);
        return;
    }

    /// <summary>
    /// find a random point on the layermask in front of the origin, may want to move this into a helper class
    /// note, placement forward of a transform should be done before passing in the origin
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="area"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>
    public static Vector3 ForwardRandomNavSphere(Vector3 origin, float area, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * Random.Range(2, area);
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, area, layermask);
        return navHit.position;
    }
}
