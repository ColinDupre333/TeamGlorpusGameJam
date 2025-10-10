using UnityEngine;

public class Borders : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 moveAdjustement = Vector3.zero;
        if (viewportPosition.x < 0)
        {
            moveAdjustement.x += 1;
        }
        else if (viewportPosition.x > 1)
        { 
            moveAdjustement.x -= 1;
        }
        else if (viewportPosition.y < 0)
        {
            moveAdjustement.y += 1;
        }
        else if (viewportPosition.y > 1)
        {
            moveAdjustement.y -= 1;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjustement);
    }
}
