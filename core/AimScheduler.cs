
using UnityEngine;

public class AimScheduler : MonoBehaviour
{
    bool aiming = false;
    bool reloading = false;
    bool punching = false;
   public void Aiming(bool aim)
    {
        aiming = aim;
    }
    public void Punching(bool punch)
    {
        punching = punch;
    }
    public void Reloading(bool reload)
    {
        reloading = reload;
    }
    public bool ToReload()
    {
        return reloading;
    }
    public bool ToAim()
    {
        return aiming;
    }
    public bool ToPunch()
    {
        return punching;
    }
}
