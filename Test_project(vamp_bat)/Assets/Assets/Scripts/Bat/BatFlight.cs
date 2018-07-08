using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlight : MonoBehaviour {

    [SerializeField]
    public float speed = 10f;
    public float maxRotationSpeed = 0.9f;
    public float currentRotationSpeed;

    private Transform destination;

    private float timer = 0;

    //Приводит скорость передвижения и максимальную скорость поворота мыши к значениям по умолчанию
    public void ResetSpeed()
    {
        speed = 10f;
        maxRotationSpeed = 0.9f;
    }

    //Устанавливает скорость передвижения и максимальную скорость поворота мыши
    public void SetSpeed(float _speed, float _rotationSpeed)
    {
        speed = _speed;
        maxRotationSpeed = _rotationSpeed;
    }

    //Устанавливает игрока как конечную цель движения мыши
    public void SetDestinationToPlayer()
    {
        this.destination = GameObject.Find("Target").GetComponent<Transform>();
        this.GetComponent<BatState>().SetStateMovesToPlayer();
        currentRotationSpeed = 0.0f;
    }

    //Устанавливает целевую точку как конечную цель движения мыши
    public void SetDestinationToTargetPoint()
    {
        this.destination = this.GetComponent<BatState>().targetPoint;
        this.GetComponent<BatState>().SetStateMovesToTargetPoint();
        currentRotationSpeed = 0.04f;
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (timer > 0.1f)
        {
            //Если мышь не находится в атаке, каждые .1 секунды увеличивает скорость ее поворота на 0.001f, пока она не достигнет максимального значения
            if (!this.GetComponent<BatState>().inAttack)
            {
                if (currentRotationSpeed < maxRotationSpeed) currentRotationSpeed += 0.001f;
            }
            //Если мышь находится в атаке, ее скорость поворота всегда равна заданному максимальному значению
            else
            {
                currentRotationSpeed = maxRotationSpeed;
            }

            timer = 0;
        }

        //Находит направление, в котором должна двигаться мышь, чтобы достигнуть конечной точки (destination)
        Vector3 relativePosition = destination.position - transform.position;

        //Сглаживает направление, в котором движется мышь, и необходимое направление
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(relativePosition), currentRotationSpeed);
        //Перемещает позицию мыши в пространстве сцены на расчетное значение
        transform.position += transform.forward * Time.deltaTime * speed;
    }

}
