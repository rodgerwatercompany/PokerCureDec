using UnityEngine;
using System.Collections;

namespace Rodger
{
    public abstract class TouchPlaneBase : MonoBehaviour
    {

        protected string hitColliderName;
        protected Camera m_camera;
        protected bool m_state_drag;
        protected RaycastHit m_hit;

        
        // Use this for initialization
        protected virtual void Start()
        {
            m_state_drag = false;
        }

        // Update is called once per frame
        protected virtual void Update()
        {            

            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            
            if (m_state_drag)
            {
                
                if (Physics.Raycast(ray, out m_hit, Mathf.Infinity))
                {
                    if (m_hit.collider.name == hitColliderName)
                    {
                        OnPanelDrag();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out m_hit, Mathf.Infinity))
                {

                    if (m_hit.collider.name == hitColliderName)
                    {
                        m_state_drag = true;
                        OnPressCollider(true);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                m_state_drag = false;
                OnPressCollider(false);
            }
        }

        protected abstract void OnPressCollider(bool state);
        protected abstract void OnPanelDrag();
    }
}