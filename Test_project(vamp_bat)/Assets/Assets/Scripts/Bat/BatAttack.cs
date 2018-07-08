using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour {

    private float attackCounter;

    public void Attack()
    {
        //В случае успешной атаки присваивает мыши состояние "Движется к целевой точке"
        this.GetComponent<BatState>().SetStateMovesToTargetPoint();
        this.GetComponent<BatAnimation>().SetFlightAnimation();
        this.GetComponent<BatFlight>().SetDestinationToTargetPoint();
        this.GetComponent<BatFlight>().ResetSpeed();
        this.attackCounter = 0;

        //Заливает экран красным цветом
        GameObject.Find("Camera").GetComponent<DamageVisuializer>().intensity = 1;
    }

    // Update is called once per frame
    void Update () {

        //Если расстояние между мышью и игроком меньше 10 м. и мышь не находилась
        //в атаке, присваивает мыши состояние "в атаке" и запускает анимацию атаки
        if (Vector3.Distance(transform.position, GameObject.Find("PlayerCollider").GetComponent<Transform>().position) < 10 &&
            this.GetComponent<BatState>().movesToPlayer &&
            !this.GetComponent<BatState>().inAttack)
        {
            this.GetComponent<BatFlight>().SetSpeed(10f, 0.01f);
            this.GetComponent<BatState>().SetStateInAttack();
            this.GetComponent<BatAnimation>().SetAttackAnimation();
        }

        //Таймер, отсчитывающий время нахождения мыши в атаке
        if (this.GetComponent<BatState>().inAttack)
        {
            attackCounter += Time.deltaTime;
        }

        //Если мышь не смогла атаковать игрока за 1.48 сек. (время, за которое проигрывается 
        //анимация атаки), присваивает мыши состояние "Движется к целевой точке"
        if (attackCounter >= 1.48)
        {
            this.GetComponent<BatState>().SetStateMovesToTargetPoint();
            this.GetComponent<BatAnimation>().SetFlightAnimation();
            this.GetComponent<BatFlight>().SetDestinationToTargetPoint();
            this.GetComponent<BatFlight>().ResetSpeed();
            this.attackCounter = 0;
        }


    }
}
