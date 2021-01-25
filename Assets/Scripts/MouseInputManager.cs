using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class MouseInputManager : MonoBehaviour
{
    RaycastHit2D hit;
    
    public EventGameObject onClickAttackable;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, enemyLayers.value);
            if (!hit)
                return;


            if (hit.collider.GetComponent(typeof(IAttackable)) != null && hit.collider != null)
            {
                Debug.Log("HIT!!: " + hit.collider.gameObject.name);

                GameObject attackable = hit.collider.gameObject;
                onClickAttackable.Invoke(attackable);

            }
           
        }*/
        
    }
    [Serializable]
    public class EventGameObject : UnityEvent<GameObject> { }
}
