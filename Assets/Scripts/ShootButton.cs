using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerManager.instance.isDead)
            return;
        if(PlayerManager.instance.playerInventory.currentGun.isAuto)
            PlayerManager.instance.playerInventory.currentGun.KeepFiring(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerManager.instance.isDead)
            return;
        if(PlayerManager.instance.playerInventory.currentGun.isAuto)
            PlayerManager.instance.playerInventory.currentGun.KeepFiring(true);
        else
            PlayerManager.instance.playerInventory.currentGun.FireOneShot();
    }
}