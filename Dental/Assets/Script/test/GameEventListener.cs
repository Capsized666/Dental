using UnityEngine;
using UnityEngine.Events;

public class GameEventListener:MonoBehaviour
{
    [SerializeField]
    private GameEventer _mainevent;
    [SerializeField]
    private UnityEvent Actions;

    private void OnEnable()
    {
        _mainevent.AddPerson(this);
    }
    private void OnDisable()
    {
        _mainevent.DelPerson(this);
    }
    public void EventRised() {
        Actions.Invoke();
    }
}