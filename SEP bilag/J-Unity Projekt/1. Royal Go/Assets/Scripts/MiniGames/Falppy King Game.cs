//dette scirpt håntere logikken for "fuglen" i flappy king

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FlappyKingGame : MonoBehaviour
{
    [SerializeField] private float _velocity = 100.0f;
    [SerializeField] private float _rotationSpeed = 0.2f;

    private Rigidbody2D _rb;

    public GameObject gameUI;

    public RectTransform Player;
    [SerializeField] private RectTransform ground;
    [SerializeField] private RectTransform spawn;
    [SerializeField] private PipeMover pipes;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit hit;

        //Er det virkelig nødvendigt med et raycast til at tjekke om touch screen bliver brugt, har ikke lige en flottere løsning men det virker lidt overkill o.o
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Mouse Clicked");
            _rb.linearVelocity = Vector2.up * _velocity;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space Key Pressed");
            _rb.linearVelocity = Vector2.up * _velocity;
        }

        if (RectOverlaps(Player, ground))
        {
            Debug.Log("Game Over");
            Player.position = spawn.position;
            gameUI.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * _rotationSpeed);
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(a, b.position) ||
               RectTransformUtility.RectangleContainsScreenPoint(b, a.position);
    }
}

//skrevet af Nikolaj Bræmer
//Valideret af: Victor