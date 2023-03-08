using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour 
{
	public Transform StartPoint;
	public Transform EndPoint;

	void Start()
	{
	}

	void Update()
	{
		transform.position = Vector3.Lerp(StartPoint.position, EndPoint.position, Timeline.Current);
	}
}
