using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }
    #region TouchBeganEvents
    public Action<Vector2> OnTouchBeganEvent;
    public void TouchBeganEvent(Vector2 position)
    {
        if (OnTouchBeganEvent != null)
        {
            OnTouchBeganEvent(position);            
        }        
    }
    #endregion
    #region TouchMovedEvents
    public Action<Vector2> OnTouchMovedEvent;
    public void TouchMovedEvent(Vector2 delta)
    {
        if (OnTouchMovedEvent != null)
        {
            OnTouchMovedEvent(delta);
        }
    }
    #endregion
    #region TouchEndedEvents
    public Action OnTouchEndedEvent;
    public void TouchEndedEvent()
    {
        if (OnTouchEndedEvent != null)
        {
            OnTouchEndedEvent();
        }
    }
    #endregion
    #region TouchStationaryEvents
    public Action<Vector2> OnTouchStationaryEvent;
    public void TouchStationaryEvent(Vector2 position)
    {
        if (OnTouchStationaryEvent != null)
        {
            OnTouchStationaryEvent(position);

        }
    }
    #endregion
    #region TouchCancelledEvents
    public Action OnTouchCancelledEvent;
    public void TouchCancelledEvent()
    {
        if (OnTouchCancelledEvent != null)
        {
            OnTouchCancelledEvent();
        }
    }
    #endregion
    #region OnSlideEvent
    public Action<Vector2> OnSlideEvent;
    public void SlideEvent(Vector2 delta)
    {
        if (OnSlideEvent != null)
        {
            OnSlideEvent(delta);
        }
    }
    #endregion
    
}
