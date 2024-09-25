using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public List<touchLocation> touches = new List<touchLocation>();

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
           
            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("touchBegan");

                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(t.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector3.forward);

                if (hit.collider != null && hit.collider.GetComponent<ITouchArea>() != null && !hit.collider.GetComponent<ITouchArea>().IsTouched)
                {
                    Debug.Log("Touched object name: " + hit.collider.gameObject.name);
                    //  hit.collider.gameObject.transform.position = getTouchPosition(t.position);
                    hit.collider.GetComponent<ITouchArea>().StartToDrag(getTouchPosition(t.position));
                    touches.Add(new touchLocation(t.fingerId, hit.collider.GetComponent<TouchArea>()));
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch Ended");
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                if (thisTouch != null)
                {
                    thisTouch.circle.GetComponent<ITouchArea>().StopToDrag(getTouchPosition(t.position));
                    touches.RemoveAt(touches.IndexOf(thisTouch));
                }

            }
            else if (t.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch Moving");
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                if (thisTouch != null)
                {
                    //   thisTouch.circle.transform.position = getTouchPosition(t.position);
                    thisTouch.circle.GetComponent<ITouchArea>().OnDrag(getTouchPosition(t.position));
                }
            }
            ++i;
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
}
