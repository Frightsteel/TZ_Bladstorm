using System;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //If we want to use a custom animation of meteor falling, based on some fall time in future
    public event Action<Collider2D[]> OnMeteorFellEvent; 


    public void DropMeteor(Vector2 fallPoint, float radius, LayerMask layerMask)
    {
        gameObject.transform.position = fallPoint;//temp, unless we have anim

        gameObject.SetActive(true);//temp, unless we have anim

        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        OnMeteorFellEvent?.Invoke(targetColliders);
    }
}