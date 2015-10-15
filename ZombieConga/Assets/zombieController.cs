using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zombieController : MonoBehaviour {

    public AudioClip enemyContactSound;
    public AudioClip catContactSound;
    private int lives = 3;  
    private bool isInvincible = false;
    private float timeSpentInvincible;
    public float moveSpeed = 1.0f;
    public float turnSpeed = 3.0f;
    private Vector3 moveDirection;
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;
    private List<Transform> congaLine = new List<Transform>();
    // Use this for initialization
    void Start () {
        moveDirection = Vector3.right;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 currentPosition = transform.position;
        if (Input.GetButton("Fire1"))
        {
            Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveDirection = moveToward - currentPosition;
            moveDirection.z = 0;
            moveDirection.Normalize();
        }
        Vector3 target = moveDirection * moveSpeed + currentPosition;
        transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);
        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                             Quaternion.Euler(0, 0, targetAngle),
                             turnSpeed * Time.deltaTime);
        EnforceBounds();
        if (isInvincible)
        {
            timeSpentInvincible += Time.deltaTime;
            if (timeSpentInvincible < 3f)
            {
                float remainder = timeSpentInvincible % .3f;
                GetComponent<Renderer>().enabled = remainder > .15f;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
                isInvincible = false;
            }
        }
    }
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cat"))
        {
            Transform followTarget = congaLine.Count == 0 ? transform : congaLine[congaLine.Count - 1];
            other.transform.parent.GetComponent<catController>().JoinConga(followTarget, moveSpeed, turnSpeed);
            GetComponent<AudioSource>().PlayOneShot(catContactSound);
            if (congaLine.Count >= 5)
            {
                Application.LoadLevel("WinScene");
            }
            congaLine.Add(other.transform);
        }
        else if (!isInvincible && other.CompareTag("enemy"))
        {
            isInvincible = true;
            timeSpentInvincible = 0;
            GetComponent<AudioSource>().PlayOneShot(enemyContactSound);
            for (int i = 0; i < 2 && congaLine.Count > 0; i++)
            {
                int lastIdx = congaLine.Count - 1;
                Transform cat = congaLine[lastIdx];
                congaLine.RemoveAt(lastIdx);
                cat.parent.GetComponent<catController>().ExitConga();
            }
            if (--lives <= 0)
            {
                Application.LoadLevel("LoseScreen");
            }
        }
    }

    private void EnforceBounds()
    {
        // 1
        Vector3 newPosition = transform.position;
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;

        // 2
        float xDist = mainCamera.aspect * mainCamera.orthographicSize;
        float yDist = mainCamera.orthographicSize;
        float xMax = cameraPosition.x + xDist;
        float xMin = cameraPosition.x - xDist;
        float yMax = cameraPosition.y + yDist;
        float yMin = cameraPosition.y - yDist;

        // 3
        if (newPosition.x < xMin || newPosition.x > xMax)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
            moveDirection.x = -moveDirection.x;
        }
        if (newPosition.y < yMin || newPosition.y > yMax)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
            moveDirection.y = -moveDirection.y;
        }
        // TODO vertical bounds

        // 4
        transform.position = newPosition;
    }
}
