using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineView : MonoBehaviour 
{
	public Image Cursor;
	public Image MainLine;
	public GameObject BentSectionPrefab;
	public Transform BentSectionParent;
    public Image Status;

    [Header("Status Images")]
    public Sprite FastForward;
    public Sprite Rewind;
    public Sprite Pause;
    public Sprite Play;

	private readonly Dictionary<Bend, GameObject> _bendToObject = new Dictionary<Bend, GameObject>();
	
	private GameObject _tempBend;
	private float _tempMark;

	private float MainLineSize
	{
		get { return MainLine.rectTransform.rect.width; }
	}

	private void Update()
	{
		var p = Cursor.rectTransform.anchoredPosition;
		p.x = Timeline.Current * MainLineSize;
		Cursor.rectTransform.anchoredPosition = p;

		if (_tempBend != null)
		{
			var min = Mathf.Min(_tempMark, Timeline.Current);
			var max = Mathf.Max(_tempMark, Timeline.Current);
			FitObjectBetween(_tempBend, min, max);
		}


	}

	public void Mark(float t)
	{
		_tempBend = Instantiate(BentSectionPrefab, BentSectionParent);
		_tempMark = t;
		FitObjectBetween(_tempBend, t, t + 0.0001f);
	}

	public void Bend(Bend bend)
	{
		var go = Instantiate(BentSectionPrefab, BentSectionParent);
		FitObjectBetween(go, bend.T1, bend.T2);
		_bendToObject.Add(bend, go);

		if (_tempBend != null)
		{
			Destroy(_tempBend);
		}

	}

	private void FitObjectBetween(GameObject go, float f1, float f2)
	{
		var rt = go.GetComponent<RectTransform>();
		
		var p = rt.anchoredPosition;
		p.x = f1 * MainLineSize;
		rt.anchoredPosition = p;

		var s = rt.sizeDelta;
		s.x = (f2 - f1) * MainLineSize;
		rt.sizeDelta = s;

	}

	public void Unbend(Bend bend)
	{
		Destroy(_bendToObject[bend]);
		_bendToObject.Remove(bend);

		if (_tempBend != null)
		{
			Destroy(_tempBend);
		}
	}

    public void Tick(TimelineState state)
    {

        if (state == TimelineState.Forward)
        {
            Status.sprite = FastForward;
        }
        else if (state == TimelineState.Rewind)
        {
            Status.sprite = Rewind;
        }
        else
        {
            Status.sprite = Pause;
        }
    }
}
