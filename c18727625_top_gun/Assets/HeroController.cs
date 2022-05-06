using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TakeOffState: State
{
    Vector3 attackPos;


   
    public override void Enter()
    {
        Debug.Log("Hi I'm in takeoff!");
        //set path to takeoff
        //set path following to on
     
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

class WanderState: State
{
   
    public override void Enter()
    {
        owner.GetComponent<NoiseWander>().enabled = true;
        Debug.Log("Hi I'm in wander!");
     
    }
    public override void Exit()
    {   
        //leave take off state
       
    }

    public override void Think()
    {
        //Has their take off path reached the end

        // change to wander behaviour
       
    }
}

public class HeroController : MonoBehaviour
{ 
    // Start is called before the first frame update

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "change_path")
        {
            
            GetComponent<StateMachine>().ChangeState(new WanderState());
        }

    }


    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new TakeOffState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
