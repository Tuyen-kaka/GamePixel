using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 vector3Velocity = Vector3.zero;
    public float tren, duoi, trai, phai;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            var playerX = player.transform.position.x;
            var camX = transform.position.x;
            var playerY = player.transform.position.y;
            var camY = transform.position.y;

            if (playerX > trai && playerX < phai)
            {
                camX = playerX;
            }

            if (playerX < trai)
            {
                camX = trai;
            }

            if (playerX > phai)
            {
                camX = phai;
            }

            if (playerY < tren && playerY > duoi)
            {
                camY = playerY;
            }

            if (camY < duoi)
            {
                camY = duoi;
            }

            //transform.position = new vector3(camx, 0, -10);
            transform.position = Vector3.SmoothDamp(
                    transform.position,
                    new Vector3(camX, camY, -10),
                    ref vector3Velocity,
                    0.125f
                );
        }
    }
}
