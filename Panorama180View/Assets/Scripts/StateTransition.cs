#pragma warning disable 0414

/**
 * Panorama180ViewDemo.StateTransition
 * State transition test.
 * Operate VR180 using an external method.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panorama180ViewDemo {
    [RequireComponent(typeof(Camera))]
    public class StateTransition : MonoBehaviour
    {
        // Panorama180View class assigned to Camera.
        private Panorama180View.Panorama180View m_panorama180View = null;

        [SerializeField] Texture2D sourceImage = null;      // Source Image.
        [SerializeField] Texture2D destImage = null;        // Destnation Image.

        private int m_state = 0;        // State.

        void Start () {
            m_panorama180View = this.GetComponent<Panorama180View.Panorama180View>();
            m_panorama180View.SetSrcTexture(sourceImage);       // Source Image.
            m_panorama180View.SetDestTexture(destImage);        // Destnation Image.

            m_panorama180View.SetFadeInColor(new Color(0, 0, 0));   // FadeIn Color.
            m_panorama180View.SetTransitionInterval(3.0f);          // Transition interval.

            // FadeIn Mode.
            m_panorama180View.SetStateTransition(Panorama180View.Panorama180View.StateTransitionType.FadeIn);
        }

        void Update () {
            if (Time.time > 20.0f && Time.time < 30.0f) {
                if (m_state == 0) {
                    m_state = 1;

                    m_panorama180View.SetTransitionInterval(1.0f);      // Transition interval.

                    // Blend Mode.
                    m_panorama180View.SetStateTransition(Panorama180View.Panorama180View.StateTransitionType.Blend);
                }

            } else if (Time.time > 30.0f) {
                if (m_state == 1) {
                    m_state = 2;
                    m_panorama180View.SetSrcTexture(destImage);
                    m_panorama180View.SetDestTexture(sourceImage);

                    // Blend Mode.
                    m_panorama180View.SetStateTransition(Panorama180View.Panorama180View.StateTransitionType.Blend);
                }
            }
        }
    }

}
