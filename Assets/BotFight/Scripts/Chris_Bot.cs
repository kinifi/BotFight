using UnityEngine;
using System.Collections;

public class Chris_Bot : BotController {

	private BotController m_bot;
	private bool canStartBot = false;

	// Use this for initialization
	void Start () {
	
		m_bot = GetComponent<BotController>();

	}
	
	// Update is called once per frame
	void Update () {

		if(m_bot.canMove && canStartBot == false)
		{
			canStartBot = true;
			startMoving();
		}

	}

	private void startMoving()
	{
		m_bot.Action_RunRight();
		Invoke("stopPlayer", 0.3f);
	}

	private void stopPlayer()
	{
		m_bot.Action_StopMovement();
		m_bot.Action_Jump();
		Debug.Log("Stop Player");
	}
}
