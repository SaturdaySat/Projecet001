using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

    public float movingSpeed = 15.0f;
    public MovingPoint[] movingPoints;

    public int curIndex = 0;

	void Start () {
        movingPoints = transform.parent.GetComponentsInChildren<MovingPoint>(false);
	}
	
	void Update () {
		if(IsReachTarget(curIndex))
        {
            curIndex = (curIndex + 1) % movingPoints.Length;
        }
        MoveBlockToTarget(curIndex);
    }

    private bool IsReachTarget(int index)
    {
        if (index < 0 || index >= movingPoints.Length)
            return false;

        Vector3 targetPos = movingPoints[index].transform.position;
        float distance = Vector3.Distance(targetPos, transform.position);
        return distance <= 0.1f;
    }

    private void MoveBlockToTarget(int index)
    {
        if (index < 0 || index >= movingPoints.Length)
            return;

        Vector3 targetPos = movingPoints[index].transform.position;
        this.transform.position = Vector3.MoveTowards(transform.position, targetPos, movingSpeed * Time.deltaTime);
    }
}
