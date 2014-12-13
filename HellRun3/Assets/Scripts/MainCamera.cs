using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour 
{
    public Transform player;

    public Vector2 margin;
    public Vector2 smoothing;

    public BoxCollider2D limit;

    private Vector3 min, max;

    public bool IsFollowing { get; set; }

    public void Start()
    {
        IsFollowing = true;
        min = limit.bounds.min;
        max = limit.bounds.max;
    }

    public void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if(IsFollowing)
        {
            if (Mathf.Abs(x - player.position.x) > margin.x)
                x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
            else if (Mathf.Abs(y - player.position.y) > margin.y)
                y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
        }

        var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);


    }

    

	
}
