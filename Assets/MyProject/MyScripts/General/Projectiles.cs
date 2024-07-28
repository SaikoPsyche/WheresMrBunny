using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Transform player;

    /*readonly bool isFacingRight = PlayerMovement.isFacingRight;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (isFacingRight)
            transform.position += moveSpeed * Time.deltaTime * Vector3.right;
        else
            transform.position += moveSpeed * Time.deltaTime * Vector3.left;

        transform.position = player.position;
        gameObject.SetActive(false);


        //Debug.Log(transform.position);
    }*/
}
