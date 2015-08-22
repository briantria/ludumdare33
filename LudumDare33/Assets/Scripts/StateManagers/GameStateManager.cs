using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour 
{
	#region singleton
	private static GameStateManager m_instance = null;
	public static GameStateManager Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = FindObjectOfType<GameStateManager>();
				
				if (m_instance == null)
				{
					GameObject obj = new GameObject();
					m_instance = obj.AddComponent<GameStateManager>();
					obj.name = m_instance.name;
				}
			}
			
			return m_instance;
		}
	}
	#endregion
	
	protected void Awake ()
	{
		if (m_instance == null)
		{
			m_instance = this;
		}
		else
		{
			// restrict to one instance only
			Destroy(this.gameObject);
		}
	}
}

public enum GameState 
{
	Inactive, // game screen not active
	Idle, // game screen not focused
	Running
}