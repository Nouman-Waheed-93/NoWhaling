using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

namespace NMenus
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    public class SpriteSwapToggle : MonoBehaviour
    {

        public Sprite onSprite, offSprite;
        public ToggleEvent onToggle;

        Image myImg;

        bool isOn = true;



        // Use this for initialization
        void Start()
        {
            myImg = GetComponent<Image>();
        }

        public void SwitchToggle()
        {
            isOn = !isOn;
            myImg.sprite = isOn ? onSprite : offSprite;
            onToggle.Invoke(isOn);
            CrossPlatformInputManager.SetButtonDown("Dir");
            StartCoroutine("ResetDirButon");
        }

        IEnumerator ResetDirButon()
        {
            yield return null;
            CrossPlatformInputManager.SetButtonUp("Dir");
        }

    }
}
