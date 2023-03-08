using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour 
{
    private const float Sensitivity = 0.1f;
    private float _wheelInput;
    private static TimelineView _view;

    void Start()
    {
        _view = GameObject.FindObjectOfType<TimelineView>();
    }

    void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Timeline.CancelMark();
		}

	    if (Input.GetMouseButtonDown(0))
	    {
			Timeline.Mark();
        }
        if (Input.GetMouseButtonDown(1))
	    {
			Timeline.Unbend();
        }

	    _wheelInput += System.Math.Sign(Input.GetAxis("Mouse ScrollWheel")); // Not Mathf.Sign()
	    _wheelInput = Mathf.Clamp(_wheelInput, -1, 1);
	    if (Input.GetKeyDown(KeyCode.F))
	    {
	        _wheelInput = 0;
	    }
        if (_wheelInput > 0)
	    {
			Timeline.Forward(Time.deltaTime * Sensitivity, true);
	        _view.Tick(TimelineState.Forward);
        }
        else if (_wheelInput < 0)
	    {
	        Timeline.Back(Time.deltaTime * Sensitivity);
	        _view.Tick(TimelineState.Rewind);
	    }
	    else
	    {
	        _view.Tick(TimelineState.Pause);
        }

        Timeline.Tick(Time.deltaTime * Sensitivity);
	}
}
