using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    BoxCollider2D myBodyCollider;

    Vector2 moveDelta;
    Vector2 startingLocalScale;
    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider = GetComponent<BoxCollider2D>();
        startingLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        FlipSprite();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector2(x, y);

        transform.Translate(moveDelta * Time.deltaTime * movementSpeed);
    }

    private void FlipSprite()
    {
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
