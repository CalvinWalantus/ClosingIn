using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private bool m_VerticalMovement = false;
		bool dimension = GameObject.Find ("WorldController").GetComponent<World> ().dimension;

        
        private void Start()
        {
			
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
			dimension = GameObject.Find ("WorldController").GetComponent<World> ().dimension;
			var m_rigidbody = GetComponent<Rigidbody> ();
			if (dimension == false) {
				m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			} else {
				m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			}
           if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
			float v;
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
			if (dimension == true) {
				v = CrossPlatformInputManager.GetAxis ("Vertical");
			} else {
				v = 0;
			}

			//CUSTOM - disables horizontal movement, for 2D
			//if (m_VerticalMovement) v = CrossPlatformInputManager.GetAxis("Vertical");
			//else v = 0;


            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;

                if (!dimension) {
                	//m_Move += new Vector3(0, 0, -1);	// TODO: fix walking angle in 3d
                }
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }

		//CUSTOM - set vertical movement possible true or false. Pass false for 2D.
		public void SetDimension (bool dim) {
			dimension = dim;
			m_VerticalMovement = !dim;
		}
	
    }

}
