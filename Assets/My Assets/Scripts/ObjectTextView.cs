using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class ObjectTextView : MonoBehaviour
{

    public string Text = "<b>Укажите</b> <color=#ffea00>имя</color> объекта";
    public int TextSize = 14;
    public Font TextFont;
    public Color TextColor = Color.white;
    public float TextHeight = 1.15f;
    public bool ShowShadow = true;
    public Color ShadowColor = new Color(0, 0, 0, 0.5f);
    public Vector2 ShadowOffset = new Vector2(1, 1);

    private string textShadow;

    void Awake()
    {
        enabled = false;
        TextShadowReady();
    }

    void TextShadowReady()
    {
        textShadow = Regex.Replace(Text, "<color[^>]+>|</color>", string.Empty);
    }

    void OnGUI()
    {
        GUI.depth = 9999;

        GUIStyle style = new GUIStyle();
        style.fontSize = TextSize;
        style.richText = true;
        if (TextFont) style.font = TextFont;
        style.normal.textColor = TextColor;
        style.alignment = TextAnchor.MiddleCenter;

        GUIStyle shadow = new GUIStyle();
        shadow.fontSize = TextSize;
        shadow.richText = true;
        if (TextFont) shadow.font = TextFont;
        shadow.normal.textColor = ShadowColor;
        shadow.alignment = TextAnchor.MiddleCenter;

        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + TextHeight, transform.position.z);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        screenPosition.y = Screen.height - screenPosition.y;

        if (ShowShadow) GUI.Label(new Rect(screenPosition.x + ShadowOffset.x, screenPosition.y + ShadowOffset.y, 0, 0), textShadow, shadow);
        GUI.Label(new Rect(screenPosition.x, screenPosition.y, 0, 0), Text, style);
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
}