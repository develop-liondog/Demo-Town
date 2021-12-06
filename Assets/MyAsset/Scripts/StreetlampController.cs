using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X���̐���
/// ���Ԃɂ�郉�C�g��ON,OFF�Ȃǂ𐧌䂷��
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
	/// ���C�g��ON,OFF
	/// </summary>
	/// <param name="b"></param>
	public void SetActiveLight( bool b )
	{
		this.spotLight.gameObject.active = b;
	}

}
