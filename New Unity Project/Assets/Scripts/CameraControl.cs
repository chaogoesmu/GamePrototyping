using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float ScrollSpeed = 15;
    public float ScrollEdge = 0.0f;

    public int HorizontalScroll = 1;
    public int VerticalScroll = 1;
    public int DiagonalScroll = 1;

    float PanSpeed = 10;

    Vector2 ZoomRange = new Vector2(-5, 5);
    public float CurrentZoom = 0;
    public float ZoomZpeed = 1;
    public float ZoomRotation = 1;

    private Vector3 InitPos;
    private Vector3 InitRotation;



    void Start()
    {
        //Instantiate(Arrow, Vector3.zero, Quaternion.identity);

        InitPos = transform.position;
        InitRotation = transform.eulerAngles;

    }

    void Update()
    {

        //PAN
        if (Input.GetKey("mouse 2"))
        {
            //(Input.mousePosition.x - Screen.width * 0.5)/(Screen.width * 0.5)
            transform.Translate((Vector3.right * Time.deltaTime * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f)), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);

        }
        else
        {
            if (Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width * (1 - ScrollEdge)*/)
            {
                transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
            }
            else if (Input.GetKey("a") /*|| Input.mousePosition.x <= Screen.width * ScrollEdge*/)
            {
                transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);
            }

            if (Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height * (1 - ScrollEdge)*/)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
            }
            else if (Input.GetKey("s") /*|| Input.mousePosition.y <= Screen.height * ScrollEdge*/)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * -ScrollSpeed, Space.World);
            }
        }

        //ZOOM IN/OUT

        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;

        CurrentZoom = Mathf.Clamp(CurrentZoom, ZoomRange.x, ZoomRange.y);

        //transform.position.y -= (transform.position.y - (InitPos.y + CurrentZoom)) * 0.1;
        //transform.eulerAngles.x -= (transform.eulerAngles.x - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1;
        int layerthing;
        layerthing = 1 << 8 | 1 << 9;

        //check if the left mouse has been pressed down this frame
        //empty RaycastHit object which raycast puts the hit details into
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerthing))
                if (hit.collider != null)
                {
                    print("hit: " + hit.collider.ToString());
                }
        }
    }
}
