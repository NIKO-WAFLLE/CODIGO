using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool isLeft = false;
    bool isRight = false;

    public Rigidbody2D rb;
    public float speedForce;

    public void clickLeft()
    {
        isLeft = true;
        Debug.Log("izquierda true");
    }

    public void releaseLeft()
    {
        isLeft = false;
        Debug.Log("izquierda false");
    }

    public void clickRight()
    {
        isRight = true;
        Debug.Log("derecha verdadero");
    }

    public void releaseRight()
    {
        isRight = false;
        Debug.Log("derecha false");
    }

    private void FixedUpdate()
    {
        if (isLeft)
        {
            rb.AddForce(new Vector2(-speedForce, 0) * Time.deltaTime);
            Debug.Log("izquierda");
        }

        if (isRight)
        {
            rb.AddForce(new Vector2(speedForce, 0) * Time.deltaTime);
            Debug.Log("derecha");
        }
    }


}
