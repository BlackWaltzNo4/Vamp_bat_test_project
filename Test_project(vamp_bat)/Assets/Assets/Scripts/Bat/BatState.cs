using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatState : MonoBehaviour {

    public bool inAttack;
    public bool movesToPlayer;
    public bool movesToTargetPoint;

    //Хранит Transform игрового объекта, назначенного целевой точкой для данной мыши при ее создании
    public Transform targetPoint;

    //Устанавливает состояние мыши "В атаке"
    public void SetStateInAttack()
    {
        inAttack = true;
        movesToPlayer = false;
        movesToTargetPoint = false;
    }

    //Устанавливает состояние мыши "Движется к игроку"
    public void SetStateMovesToPlayer()
    {
        inAttack = false;
        movesToPlayer = true;
        movesToTargetPoint = false;
    }

    //Устанавливает состояние мыши "Движется к целевой точке"
    public void SetStateMovesToTargetPoint()
    {
        inAttack = false;
        movesToPlayer = false;
        movesToTargetPoint = true;
    }
}
