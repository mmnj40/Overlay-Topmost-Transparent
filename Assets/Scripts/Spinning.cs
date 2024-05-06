using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spinning : MonoBehaviour
{
    float m_Speed;
    float minX, maxX, minY, maxY;
    float bufferBound;
    Vector3 movementDirection;
    [SerializeField]
    GameObject gameObject;

    void Start()
    {
        m_Speed = 50.0f;
        bufferBound = 0.5f;
        transform.Rotate(xAngle: 60, 0, 60);
        Camera cam = Camera.main;
        float screenHalfWidth = cam.aspect * cam.orthographicSize;
        float screenHalfHeight = cam.orthographicSize;
        float objectHalfWidth = GetComponent<Renderer>().bounds.extents.x;
        float objectHalfHeight = GetComponent<Renderer>().bounds.extents.y;

        minX = -screenHalfWidth + objectHalfWidth + bufferBound;
        maxX = screenHalfWidth - objectHalfWidth - bufferBound;
        minY = -screenHalfHeight + objectHalfHeight + bufferBound;
        maxY = screenHalfHeight - objectHalfHeight - bufferBound;

        movementDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);


    }


    void FixedUpdate()
    {
        transform.position += movementDirection * m_Speed * Time.deltaTime;
        transform.Rotate(Vector3.up * m_Speed * Time.deltaTime, Space.World);


        if (transform.position.x < minX || transform.position.x > maxX)
        {
            Vector3 normal = Vector3.right;
            movementDirection = Vector3.Reflect(movementDirection, normal);
        }

        if (transform.position.y < minY || transform.position.y > maxY)
        {
            Vector3 normal = Vector3.down;
            movementDirection = Vector3.Reflect(movementDirection, normal);


        }
        if (m_Speed > 50)
        {
            m_Speed -= 10f;
        }
        if (IsClicked())
        {
            m_Speed += 1000;
        }
    }



    private bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayHit = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayCastHit;

            if (Physics.Raycast(rayHit, out rayCastHit))
            {
                if (rayCastHit.collider.gameObject == gameObject)
                {
                    return true;
                }
            }
        }


        return false;
    }
}
