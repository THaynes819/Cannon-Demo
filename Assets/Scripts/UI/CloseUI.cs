using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class CloseUI : MonoBehaviour
    {
        [SerializeField] GameObject uiContainer = null;
        [SerializeField] bool hasToggleKey = false;
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        // Start is called before the first frame update
        void Start()
        {
            uiContainer.SetActive (false);
        }

        // Update is called once per frame
        void Update()
        {
            if (hasToggleKey == false) return;

            if (Input.GetKeyDown (toggleKey))
            {
                Toggle ();
            }
        }

        public void Toggle ()
        {
            uiContainer.SetActive (!uiContainer.activeSelf);
        }

        public void Close()
        {
            if (uiContainer.activeSelf)
            {
                uiContainer.SetActive(false);
            }
            
        }
    }
}

