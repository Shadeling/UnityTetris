using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMyGameActionListener<T>
{
    void OnEventRaised(T variable);
}
