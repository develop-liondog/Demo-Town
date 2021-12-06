using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 街灯の制御
/// 時間によるライトのON,OFFなどを制御する
/// </summary>
public class StreetlampController : MonoBehaviour
{
	private Light spotLight = null;

    // Start is called before the first frame update
    void Start()
    {
		this.spotLight = GetComponentInChildren<Light>();
    }

	/// <summary>
	/// ライトのON,OFF
	/// </summary>
	/// <param name="b"></param>
	public void SetActiveLight( bool b )
	{
		this.spotLight.gameObject.active = b;
	}

}
