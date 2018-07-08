using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour {

    public void SetRandomPosition()
    {
        float altitude;
        float minRange;
        float maxRange;
        float randomRange;

        //Устанавливаем случайную высоту
        altitude = Random.Range(15f, 35f);
        //Через уравнение Пифагора определяем дистанцию, на которую при заданной высоте
        //может отдалиться точка, чтобы расстояние между ней и центром было равно 100 м.
        maxRange = Mathf.Sqrt(10000 - Mathf.Pow(altitude, 2));
        //Минимальная дистанция равняется высоте, если она больше или равна 20 м.
        //и рассчитывается аналогично максимальной дистанции, если высота меньше 20 м.
        if (altitude >= 20) minRange = altitude;
        else minRange = Mathf.Sqrt(400 - Mathf.Pow(altitude, 2));
        //Выбор случайной дистанции, удовлетворяющей условию
        randomRange = Random.Range(minRange, maxRange);

        //Находим случайную точку на окружности случайного 
        //радиуса, удовлетворяющего условию
        Vector3 _rangePosition = GameObject.Find("Player").GetComponent<Transform>().position + 
                                 new Vector3(Random.Range(0f, 1f) - 0.5f, 0, Random.Range(0f, 1f) - 0.5f).normalized * randomRange;

        //Перемещаем игровой объект в найденную точку на заданную высоту
        this.GetComponent<Transform>().position = new Vector3(_rangePosition.x, altitude, _rangePosition.z);

        //Debug.Log("Distance: " + Vector3.Distance(GameObject.Find("Player").GetComponent<Transform>().position, this.GetComponent<Transform>().position) +
        //          ", altitude: " + this.GetComponent<Transform>().position.y +
        //          ", minRange: " + minRange +
        //          ", maxRange: " + maxRange +
        //          ", range: " + randomRange);
    }
}
