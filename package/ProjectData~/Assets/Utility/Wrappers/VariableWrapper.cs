using System;
using UnityEngine;

namespace Utility
{
    public abstract class VariableWrapper<T> : ScriptableObject
    {
        [SerializeField] private T m_Value;

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => this.m_Value;
            set
            {
                m_Value = value;
                this.OnValueChanged?.Invoke(m_Value);
            }
        }
    }
}