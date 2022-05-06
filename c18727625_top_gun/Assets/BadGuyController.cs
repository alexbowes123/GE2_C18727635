using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VTakeOffState: State
{
   
    public override void Enter()
    {
        Debug.Log("Baddie in takeoff!");
        // Debug.Log("state is "+owner.GetComponent<StateMachine>().currentState);
        owner.GetComponent<FollowPath>().enabled = true;
     
    }
    public override void Exit()
    {   
        //leave follow path behaviour after taking off

        owner.GetComponent<FollowPath>().enabled = false;
        Debug.Log("BYE TAKEOFF!");
       
    }

    public override void Think()
    {
        //Has their take off path reached the end

        // change to wander behaviour
       
       

      
       
    }
}

public class AttackState : State
{
    public override void Enter()
    {
        Debug.Log("Baddie chasing");
        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        // Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
        // if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 30)
        // {
        //     // GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
        //     // owner.GetComponent<Fighter>().ammo --;
        // }        
        // if (Vector3.Distance(
        //     owner.GetComponent<Fighter>().enemy.transform.position,
        //     owner.transform.position) < 10)
        // {
        //     // owner.ChangeState(new FleeState());
        // }

    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }
}

public class BadGuyController : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "change_path")
        {
            Debug.Log("Collided with cp");
            
            GetComponent<StateMachine>().ChangeState(new AttackState());
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        

        //TAKE OFF 
        GetComponent<StateMachine>().ChangeState(new VTakeOffState());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
