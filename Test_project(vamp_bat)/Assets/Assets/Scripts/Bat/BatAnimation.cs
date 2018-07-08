using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();

        //Установка анимации по умолчанию
        SetFlightAnimation();

	}

    public void SetFlightAnimation()
    {
        animator.SetBool("Fly", true);
        animator.SetBool("Attack", false);
    }

    public void SetAttackAnimation()
    {
        animator.SetBool("Fly", false);
        animator.SetBool("Attack", true);
    }
	
	// Update is called once per frame
	void Update () {	

	}
}
