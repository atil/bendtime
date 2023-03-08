using UnityEngine;
using System.Collections.Generic;

public enum TimelineState
{
    None,
    Forward,
    Pause,
    Rewind,
}

public static class Timeline
{
    public static float Current;
    private static float FirstMark = -1;
    private static Stack<Bend> Stack = new Stack<Bend>();
    public static bool IsMoving = false;

    private static TimelineView _view;

    static Timeline()
    {
        _view = GameObject.FindObjectOfType<TimelineView>();
    }

    public static void Tick(float dt)
    {
        if (IsMoving)
        {
            Forward(dt, false);
        }

    }

    public static void Toggle()
    {
        IsMoving = !IsMoving;
    }

    public static void Forward(float dt, bool forced)
    {

        float stepped = Mathf.Clamp(Current + dt, 0f, 1f);

        foreach (var bend in Stack)
        {
            if (stepped > bend.T1 && stepped < bend.T2)
            {
                stepped = bend.T2;
                break;
            }
        }

        Current = stepped;

        if (forced)
        {
            IsMoving = false;
        }
    }

    public static void Back(float dt)
    {

        float stepped = Mathf.Clamp(Current - dt, 0f, 1f);

        foreach (var bend in Stack)
        {
            if (stepped > bend.T1 && stepped < bend.T2)
            {
                stepped = bend.T1;
                break;
            }
        }

        Current = stepped;

        IsMoving = false;
    }

    public static void Mark()
    {
        if (FirstMark == -1)
        {
            FirstMark = Current;

            _view.Mark(FirstMark);
        }
        else 
        {
            float min = Mathf.Min(FirstMark, Current);
            float max = Mathf.Max(FirstMark, Current);

            var bend = new Bend(min, max);
            Stack.Push(bend);

            _view.Bend(bend);
            FirstMark = -1;

            IsMoving = false;
        }
    }

    public static void CancelMark()
    {
        FirstMark = -1;
        IsMoving = false;
    }

    public static void Unbend()
    {
        if (Stack.Count == 0)
        {
            return;
        }

        var bend = Stack.Pop();
        _view.Unbend(bend);

        FirstMark = -1;

        IsMoving = false;
    }
}