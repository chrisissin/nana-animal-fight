using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector2 mousePostion;
    private float offsetX, offsetY;

    public static bool mouseButtonReleased;


    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;
    private Vector3 startDist;

    private void OnMouseDown(){
        //Debug.Log("this is OnMouseDown");
        mouseButtonReleased= false;
        offsetX = Camera.main.ScreenToWorldPoint(mousePostion).x - transform.position.x;
        offsetY = Camera.main.ScreenToWorldPoint(mousePostion).y - transform.position.y;

        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        startDist = transform.position - rayPoint;
    }

    private void OnMouseDrag(){
        mousePostion = Camera.main.ScreenToWorldPoint(mousePostion);
        transform.position = new Vector2(mousePostion.x - offsetX, mousePostion.y - offsetY);
        //Debug.Log("this is OnMouseDrag"+transform.position.x);
    }

    private void OnMouseUp(){
        mouseButtonReleased = true;
        dragging = false;
    }

    private void OnTriggerStay2D(Collider2D collision){
        string thisGameobjectName;
        string collisionGameobjectName;
        
        thisGameobjectName = gameObject.name.Substring(0, name.IndexOf("_"));
        collisionGameobjectName = collision.gameObject.name.Substring(0, name.IndexOf("_"));

        if(mouseButtonReleased && thisGameobjectName == "wolve" && thisGameobjectName == collisionGameobjectName){
            Instantiate(Resources.Load("threewolves_Object"), transform.position, Quaternion.identity);
            mouseButtonReleased= false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else
        if(mouseButtonReleased && thisGameobjectName == "threewolves" && thisGameobjectName == collisionGameobjectName){
            Instantiate(Resources.Load("mega_Object"), transform.position, Quaternion.identity);
            mouseButtonReleased= false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Console.WriteLine("start !!");
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("update !!");
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint + startDist;
        }
    }
}
