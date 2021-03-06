using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
	// Third-Person Chararacter & Transform Vector-3
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;

	// Movement 
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private bool m_VerticalMovement = false;

	// Boolean Allow flags
	public bool allow_jump = false;
	public bool allow_movement = true;
	public bool allow_sprint = true;

	// inverts mouse Y, set by in game options menu
	bool invertMouseY = false;

	// World Controller & related Dimension-Change variables
	private World world_controller;
	bool dimension;
	int two_shot;

	void Awake ()
	{
		world_controller = FindObjectOfType<World> ();
		world_controller.shotChangeEvent += ShotChange;
		world_controller.shiftEvent += Shift;

		world_controller.animationStartEvent += HandleAnimationStart;
		world_controller.animationEndEvent += HandleAnimationEnd;
	}

	void Shift (bool dim, float time)
	{
		dimension = dim;
		if (!dimension)
		{
			if (two_shot % 2 == 1)
			{
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
			}
			else
			{
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
			}
		} 
		else
		{
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		GetComponent<Rigidbody>().freezeRotation = true;
	}

	void ShotChange (int tw_shot)
	{
		two_shot = tw_shot;

		// Calvin: we might run into problems here regarding player movement during the 
		// shift since time is 0, but as of yet it doesnt seem to be a problem.
		if (!dimension)
		{
			Shift(dimension, 0);
		}
	}

	void HandleAnimationStart()
	{
		allow_movement = false;
	}

	void HandleAnimationEnd()
	{
		allow_movement = true;
	}

	private void Start()
    {
        // Get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // We use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // Get the third person character (this should never be null due to require component)
        m_Character = GetComponent<ThirdPersonCharacter>();
    }


    private void Update()
    {
		/********* We think we can delete dis ********************************************************************************************
		dimension = GameObject.Find("WorldController").GetComponent<World>().dimension;
		var m_rigidbody = GetComponent<Rigidbody> ();
		if(dimension == false)
		{
			m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
		}
		else
		{
			m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		}
		************************************************************************************************************/
		if (allow_jump)
		{
			if (!m_Jump)
			{
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		// Read inputs
		float v;
		float h = CrossPlatformInputManager.GetAxis("Horizontal");

		if (dimension == true)
		{
			v = CrossPlatformInputManager.GetAxis ("Vertical");

		}
		else
		{
			v = 0;
		}

		//Debug.Log (v + " " + h + " " + (h == 0.0f) + " " + (v == 0.0f));
		// CUSTOM - disables horizontal movement, for 2D
		// if (m_VerticalMovement) v = CrossPlatformInputManager.GetAxis("Vertical");
		// else v = 0;
        bool crouch = Input.GetKey(KeyCode.C);

        // Calculate move direction to pass to character
        if (m_Cam != null)
        {
            // Calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;

        }
        else
        {
            // We use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
		// Walk speed multiplier
		if (Input.GetKey(KeyCode.LeftControl) && allow_sprint) m_Move *= 0.5f;
#endif

		// Regular gameplay allow movement
		if (allow_movement) 
		{
			// Pass all parameters to the character control script
			m_Character.Move(m_Move, crouch, m_Jump);
		}
		m_Jump = false;
    }
}

