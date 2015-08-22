using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenStateManager : MonoBehaviour 
{
	#region singleton
	private static ScreenStateManager m_instance = null;
	public static ScreenStateManager Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = FindObjectOfType<ScreenStateManager>();
				
				if (m_instance == null)
				{
					GameObject obj = new GameObject();
					m_instance = obj.AddComponent<ScreenStateManager>();
					obj.name = m_instance.name;
				}
			}
			
			return m_instance;
		}
	}
	#endregion

	#region serialized private variables
	[SerializeField] private GameObject m_objTitleScreen;
	[SerializeField] private GameObject m_objLevelSelectScreen;
	[SerializeField] private GameObject m_objGameScreen;
	[SerializeField] private GameObject m_objSettingsScreen;
	[SerializeField] private GameObject m_objResultsScreen;
	#endregion

	private GameObject m_objActiveScreen;
	private Dictionary<ScreenState, GameObject> m_dictScreens = new Dictionary<ScreenState, GameObject>();

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

		LoadScreens ();
	}

	private void LoadScreens ()
	{
		m_dictScreens.Add (ScreenState.OnGameScreen,        m_objGameScreen);
		m_dictScreens.Add (ScreenState.OnTitleScreen,       m_objTitleScreen);
		m_dictScreens.Add (ScreenState.OnResultsScreen,     m_objResultsScreen);
		m_dictScreens.Add (ScreenState.OnSettingsScreen,    m_objSettingsScreen);
		m_dictScreens.Add (ScreenState.OnLevelSelectScreen, m_objLevelSelectScreen);

		foreach (KeyValuePair<ScreenState, GameObject> objScreen in m_dictScreens)
		{
			objScreen.Value.SetActive (false);
		}

		m_objActiveScreen = m_dictScreens[ScreenState.OnTitleScreen];
		m_objActiveScreen.SetActive (true);
	}

	public void ChangeScreen (ScreenState p_screenState)
	{
		m_objActiveScreen.SetActive (false);
		m_objActiveScreen = m_dictScreens[p_screenState];
		m_objActiveScreen.SetActive (true);
	}
}

public enum ScreenState
{
	OnTitleScreen,
	OnLevelSelectScreen,
	OnGameScreen,
	OnSettingsScreen,
	OnResultsScreen
}