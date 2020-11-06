using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected float movementSpeed = 1f;

    protected BoxCollider2D myBodyCollider;

    protected Vector3 moveDelta;
    protected Vector2 startingLocalScale;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        myBodyCollider = GetComponent<BoxCollider2D>();
        startingLocalScale = transform.localScale;
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed);

        transform.Translate(moveDelta * Time.deltaTime * movementSpeed);

        if (moveDelta.x > 0)
        {
            transform.localScale = startingLocalScale;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector2(startingLocalScale.x * -1, startingLocalScale.y);
        }
    }
}
