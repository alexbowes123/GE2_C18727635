using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class VTakeOffState: State
// {
   
//     public override void Enter()
//     {
//         Debug.Log("Baddie in takeoff!");
//         // Debug.Log("state is "+owner.GetComponent<StateMachine>().currentState);
//         owner.GetComponent<FollowPath>().enabled = true;
     
//     }
//     public override void Exit()
//     {   
//         //leave follow path behaviour after taking off

//         owner.GetComponent<FollowPath>().enabled = false;
//         Debug.Log("BYE TAKEOFF!");
       
//     }

//     public override void Think()
//     {
//         //Has their take off path reached the end

//         // change to wander behaviour
       
       

      
       
//     }
// }

// public class FlightAttackState : State
// {
//     public override void Enter()
//     {
//         Debug.Log("Baddie chasing");
//         owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
//         owner.GetComponent<Pursue>().enabled = true;
//     }

//     public override void Think()
//     {
//         Vector3 toEnemy = owner.GetComponent<Fighter>().enemy.transform.position - owner.transform.position; 
//         if (Vector3.Angle(owner.transform.forward, toEnemy) < 45 && toEnemy.magnitude < 60 && owner.GetComponent<Fighter>().ammo > 0)
//         {
          

//             GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
//             bullet.transform.parent = owner.transform;
//             bullet.GetComponent<Missile>().target = owner.GetComponent<Fighter>().enemy;
//             // bullet.transform.Rotate(0,90,90);
//             bullet.transform.Rotate(90,0,0);

//             Debug.Log("ammo is" +owner.GetComponent<Fighter>().ammo);


//             owner.GetComponent<Fighter>().ammo --;
//         }        
//         if (Vector3.Distance(
//             owner.GetComponent<Fighter>().enemy.transform.position,
//             owner.transform.position) < 10)
//         {
//             // owner.ChangeState(new FleeState());
//         }

//     }

//     public override void Exit()
//     {
//         owner.GetComponent<Pursue>().enabled = false;
//     }
// }

public class BadGuyController : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "change_path")
        {
            Debug.Log("Collided with cp");
            
            GetComponent<StateMachine>().ChangeState(new FlightAttackState());
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        

        //TAKE OFF 
        // GetComponent<FollowPath>().enabled = false; //remove and swtich on take off

        GetComponent<StateMachine>().ChangeState(new TakeOffState());
         
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
