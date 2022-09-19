using UnityEngine;

namespace Cinemachine.Examples
{
    /*
     Use WASD to move the character, and the mouse to rotate.

The 3rdPerson vcam is set up follow an invisible child of the player, which rotates itself in response to mouse movement.

The 3rdPersonAim extension locks the camera reticle to the first object that a raycast from the camera position intersects, and this is stable even in the presence of some camera noise.

The secondary (red dot) reticle shows what the player would hit if a shot were fired.  This may be different from the outer reticle if a nearby obstacle obstructs the player from seeing the object under the camera reticle. When the outer and inner recticles are not on the same position, the outer reticle moves apart to show that the aiming is inaccurate.

The dual reticle behaviour can be disabled by setting the Aim Target reticle field to null in the 3rdPersonAim extension.

Finally, the (optional - enable it to see what it does) ThirdPersonFollowDistanceModifier component modifies the camera distance as a function of vertical camera angle, mimicking the 3-orbit setup of the FreeLook by using a simple curve to modify the distance.
     */
    [AddComponentMenu("")] // Don't display in add component menu
public class ExampleHelpWindow : MonoBehaviour
{
    public string m_Title;
    [TextArea(minLines: 10, maxLines: 50)]
    public string m_Description;

    private bool mShowingHelpWindow = true;
    private const float kPadding = 40f;

    private void OnGUI()
    {
        if (mShowingHelpWindow)
        {
            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(m_Description));
            Vector2 halfSize = size * 0.5f;

            float maxWidth = Mathf.Min(Screen.width - kPadding, size.x);
            float left = Screen.width * 0.5f - maxWidth * 0.5f;
            float top = Screen.height * 0.4f - halfSize.y;

            Rect windowRect = new Rect(left, top, maxWidth, size.y);
            GUILayout.Window(400, windowRect, (id) => DrawWindow(id, maxWidth), m_Title);
        }
    }

    private void DrawWindow(int id, float maxWidth)
    {
        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label(m_Description);
        GUILayout.EndVertical();
        if (GUILayout.Button("Got it!"))
        {
            mShowingHelpWindow = false;
        }
    }
}

}