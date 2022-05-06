using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState : State
{
    public override void Enter()
    {
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
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new AttackState());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
