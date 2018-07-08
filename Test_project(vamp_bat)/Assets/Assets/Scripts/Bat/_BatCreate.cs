using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BatCreate : MonoBehaviour {

	public void Create ()
    {
        this.GetComponent<SetPosition>().SetRandomPosition();
        this.GetComponent<BatFlight>().SetDestinationToPlayer();
        this.GetComponent<BatFlight>().ResetSpeed();
    }

}
