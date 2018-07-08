using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _FlockCreate : MonoBehaviour {

    public int batsAmount;
    public Transform batPrefab;
    public Transform targetPointPrefab;

    private float[] spawnTime;

	void Start () {

        for (int i = 0; i < batsAmount; i++)
        {
            StartCoroutine(Spawn(i, Random.Range(1.0f, 10.0f)));
        }

    }

    IEnumerator Spawn(int i, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Transform bat = Instantiate(batPrefab) as Transform;
        Transform targetPoint = Instantiate(targetPointPrefab) as Transform;

        bat.position = new Vector3();
        bat.parent = this.transform;
        bat.name = "BatController#" + i;

        targetPoint.position = new Vector3();
        targetPoint.parent = bat.transform;
        targetPoint.name = "TargetPoint#" + i;

        bat.Find("Bat").GetComponent<BatState>().targetPoint = targetPoint;
        bat.Find("Bat").GetComponent<_BatCreate>().Create();
    }
}
