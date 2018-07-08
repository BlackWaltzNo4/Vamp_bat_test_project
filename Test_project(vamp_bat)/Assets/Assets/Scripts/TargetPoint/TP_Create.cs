using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Create : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.GetComponent<SetPosition>().SetRandomPosition();

	}

}
