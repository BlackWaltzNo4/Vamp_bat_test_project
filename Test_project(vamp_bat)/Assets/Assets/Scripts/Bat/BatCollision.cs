using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCollider")
        {
            this.GetComponent<BatAttack>().Attack();
            this.GetComponent<BatFlight>().SetDestinationToTargetPoint();
        }
        else if (other.name == this.GetComponent<BatState>().targetPoint.name)
        {
            other.GetComponent<SetPosition>().SetRandomPosition();
            this.GetComponent<BatFlight>().SetDestinationToPlayer();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Предотвращает столкновения мыши с краем игровой области
        if (collision.gameObject.tag == "Wall") Physics.IgnoreCollision(collision.collider, this.GetComponent<Collider>());
    }
}
