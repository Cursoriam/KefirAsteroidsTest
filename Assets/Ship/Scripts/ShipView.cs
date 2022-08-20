using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EntityManager.GetInstance().CreateEntity<ShipEntity>("ship");
    }

    public void ChangePosition(Coordinates2D position)
    {
        this.GetComponent<UnityEngine.Transform>().position = new Vector3(position.x, position.y);
    }

    public void Rotate(int angle)
    {
        
        this.GetComponent<UnityEngine.Transform>().rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }
}
