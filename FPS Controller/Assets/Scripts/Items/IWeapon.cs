using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Fire();
    public void Reload();
    public void Activate();
    public void Deactivate();
}
