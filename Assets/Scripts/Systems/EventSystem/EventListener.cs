using UnityEngine;
using UnityEngine.Events;

//creo nuestro propio tipo de game event para poder mandar mas parametros
[System.Serializable]
//public class CustomGameEvent : UnityEvent<object, object[]> { }
public class CustomGameEvent : UnityEvent<object, object> { }

public class EventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private CustomGameEvent _response;

    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        _gameEvent.UnregisterListener(this);
    }
    /*
    public void OnEventRaised(object sender, params object[] data)
    {
        _response.Invoke(sender, data);
    }*/
    public void OnEventRaised(object sender, object data)
    {
        _response.Invoke(sender, data);
    }
}
