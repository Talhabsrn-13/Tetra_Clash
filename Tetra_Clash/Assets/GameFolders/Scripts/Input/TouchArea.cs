using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchArea : MonoBehaviour, ITouchArea
{
   
    private bool _isTouched = false;
    public Transform Transform => transform;
    PlayerController player;
    public bool IsTouched => _isTouched;
    GameObject currentShape;
    Vector3 lastPosition;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>(); 
    }

    public void StartToDrag(Vector2 touchPosition)
    {
        _isTouched = true;

        player.GamePlay.OnPointerDown(touchPosition);
        player.GamePlay.OnBeginDrag(touchPosition);
 
        //RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector3.forward);
        //if (hit.collider != null && hit.collider.GetComponent<ShapeInfo>() != null)
        //{
        //    currentShape = hit.collider.gameObject;
        //    player.GamePlay.currentShape = currentShape.GetComponent<ShapeInfo>();
        //}

        //Vector3 pos = touchPosition;
        //currentShape.transform.localScale = Vector3.one * player.GameBoardGenerator.BlockSize;
        //currentShape.transform.localPosition = new Vector3(pos.x, pos.y, 0);
    }



    public void OnDrag(Vector2 touchPosition)
    {
            Vector3 pos = Camera.main.ScreenToWorldPoint(touchPosition);
            player.GamePlay.OnDrag(touchPosition);
    }
    public void StopToDrag(Vector2 LastPosition)
    {
        _isTouched = false;
        player.GamePlay.OnPointerUp();
        lastPosition = LastPosition;     
    }

    public Vector3 LastPosition => lastPosition;
}
