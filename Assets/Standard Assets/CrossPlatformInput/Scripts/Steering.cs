using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{

    public class Steering : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public enum AxisOption
        {
            // Options for which axes to use
            Both, // Use both
            OnlyHorizontal, // Only horizontal
            OnlyVertical // Only vertical
        }

        public int MovementRange = 100;
        public Transform indicator;
        public RectTransform directionInd;
        public Transform defaultPosition;
        public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
        public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
        public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

        Vector3 m_StartPos;
        bool m_UseX; // Toggle for using the x axis
        bool m_UseY; // Toggle for using the Y axis
        CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

        void OnEnable()
        {
            CreateVirtualAxes();
        }

        void Start()
        {
            m_StartPos = transform.position;
        }

        void UpdateVirtualAxes(Vector3 value)
        {
            var delta = m_StartPos - value;
            delta.y = -delta.y;
            delta /= MovementRange;
            if (m_UseX)
            {
                m_HorizontalVirtualAxis.Update(-delta.x);
            }

            if (m_UseY)
            {
                m_VerticalVirtualAxis.Update(delta.y);
            }
            float Angle = Mathf.Rad2Deg * Mathf.Atan2(delta.y, -delta.x);

            if (delta.magnitude > 0.1f)
            {
                indicator.rotation = Quaternion.Euler(0, 0, Angle);
            }
        }

        void CreateVirtualAxes()
        {
            // set axes to use
            m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
            m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

            // create new axes based on axes to use
            if (m_UseX)
            {
                if (CrossPlatformInputManager.AxisExists(horizontalAxisName))
                {
                    CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);
                }
                m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
            }
            if (m_UseY)
            {
                if (CrossPlatformInputManager.AxisExists(verticalAxisName))
                {
                    CrossPlatformInputManager.UnRegisterVirtualAxis(verticalAxisName);
                }
                m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
            }
        }


        public void OnDrag(PointerEventData data)
        {
            Vector3 newPos = Vector3.zero;

            if (m_UseX)
            {
                int delta = (int)(data.position.x - m_StartPos.x);
                delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
                newPos.x = delta;
            }

            if (m_UseY)
            {
                int delta = (int)(data.position.y - m_StartPos.y);
                delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
                newPos.y = delta;
            }
            Vector3 direction = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
            Vector3 dirScale = Vector3.one;
            dirScale.x = Mathf.Clamp(Vector3.Distance(m_StartPos, data.position), 0, 50);
            dirScale.y = directionInd.sizeDelta.y;
            directionInd.sizeDelta = dirScale;
            UpdateVirtualAxes(direction); 
        }
        

        public void OnPointerUp(PointerEventData data)
        {
            UpdateVirtualAxes(m_StartPos);
            indicator.position = defaultPosition.position;
            directionInd.sizeDelta = Vector3.one;
        }


        public void OnPointerDown(PointerEventData data) {
            m_StartPos = data.position;
            indicator.position = m_StartPos;
        }

        void OnDisable()
        {
            Debug.Log("Removso");
            // remove the joysticks from the cross platform input
            if (m_UseX)
            {
                m_HorizontalVirtualAxis.Remove();
            }
            if (m_UseY)
            {
                m_VerticalVirtualAxis.Remove();
            }
        }
    }
}
