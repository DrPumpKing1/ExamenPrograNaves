using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivable
{
    public void ToggleActive(bool active);

    public bool GetActive();
}
