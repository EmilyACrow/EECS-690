using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Compass : MonoBehaviour
{
	//[SerializeField] private RawImage compassImage;
	[SerializeField] private Transform player;
	[SerializeField] private TextMeshProUGUI compassDirectionText;

	public void Update()
	{
		//Get a handle on the Image's uvRect
		//compassImage.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1, 1);

		// Get a copy of your forward vector
		Vector3 forward = player.transform.forward;

		// Zero out the y component of your forward vector to only get the direction in the X,Z plane
		forward.y = 0;

		//Clamp our angles to only 5 degree increments
		float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
		headingAngle = 5 * (Mathf.RoundToInt(headingAngle / 5.0f));

		//Convert float to int for switch
		int displayAngle;
		displayAngle = Mathf.RoundToInt(headingAngle);

		//Set the text of compass Degree Text to the clamped value, but change it to the letter if it is a True direction
		switch (displayAngle)
		{
		case 0:
			compassDirectionText.SetText("N");
			break;
		case 360:
			compassDirectionText.SetText("N");
			break;
		case 45:
			compassDirectionText.SetText("NE");
			break;
		case 90:
			compassDirectionText.SetText("E");
			break;
		case 130:
			compassDirectionText.SetText("SE");
			break;
		case 180:
			compassDirectionText.SetText("S");
			break;
		case 225:
			compassDirectionText.SetText("SW");
			break;
		case 270:
			compassDirectionText.SetText("W");
			break;
		default:
			compassDirectionText.SetText(headingAngle.ToString ());
			break;
		}
	}
}