using System;
using UnityEngine;

namespace Utility
{
    public abstract class EventWrapper : ScriptableObject
    {
        public event Action Event;
        
        public void Raise() => this.Event?.Invoke();
        
        public void Clear() => this.Event = null;
    }
    
    public abstract class EventWrapper<T> : ScriptableObject
    {
        public event Action<T> Event;
        
        public void Raise(T value) => this.Event?.Invoke(value);
        
        public void Clear() => this.Event = null;
    }
    
    public abstract class EventWrapper<T, U> : ScriptableObject
    {
        public event Action<T, U> Event;
        
        public void Raise(T TValue, U UValue) => this.Event?.Invoke(TValue, UValue);
        
        public void Clear() => this.Event = null;
    }
}
