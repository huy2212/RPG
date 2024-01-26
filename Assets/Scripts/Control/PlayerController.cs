using UnityEngine;
using RPG.Move;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Camera mainCamera;
        private Ray ray;
        private RaycastHit hit;

        void Awake()
        {
            mover = GetComponent<Mover>();
        }

        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            bool isHit = Physics.Raycast(ray, out hit);
            if (isHit)
            {
                mover.MoveTo(hit.point);
            }
        }
    }
}