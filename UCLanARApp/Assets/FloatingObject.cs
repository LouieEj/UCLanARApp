using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    //Object Settings. Can be changed
    public float degreesPerSecond = 15.0f; //rotating speed per second
    public float amplitude = 0.5f; //hovering amplitude
    public float frequency = 1f; //frequency of float

    //Position of storing variables for X,Y,Z
    Vector3 posOffSet = new Vector3();
    Vector3 tempPos = new Vector3();


    // Start is called before the first frame update - INITIALIZATION FUNCTION
    void Start()
    {
        posOffSet = transform.position; //The starting position and rotation of object XYZ saved in vector
    }

    // Update is called once per frame. Updates every frame
    void Update()
    {
        //Spins object around the Y-Axis, makes it spin round
        //Delta time makes it match the time, multiplied by degrees per second on y axis, within the world (real life)
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        //Float gently up and down using the Sin() function
        tempPos = posOffSet;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude; //sett the y posotion to fixed multiplied by amplitude chosen

        transform.position = tempPos; //make the temppos equal for main transform
    }


}
