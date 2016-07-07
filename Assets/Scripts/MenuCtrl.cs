using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuCtrl : MonoBehaviour {

	public void SceneName(string name){
		SceneManager.LoadScene (name);
	}
}
