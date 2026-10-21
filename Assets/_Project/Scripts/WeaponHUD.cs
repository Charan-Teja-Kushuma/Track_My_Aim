
using UnityEngine;

public class WeaponHUD : MonoBehaviour
{
    [SerializeField] private WeaponController weapon;

    private GUIStyle ammoStyle;

    private void Awake()
    {
        if (weapon == null)
            weapon = FindFirstObjectByType<WeaponController>();

        ammoStyle = new GUIStyle();
        ammoStyle.fontSize = 28;
        ammoStyle.fontStyle = FontStyle.Bold;
        ammoStyle.normal.textColor = Color.white;
        ammoStyle.alignment = TextAnchor.MiddleRight;
    }

    private void OnGUI()
    {
        if (weapon == null)
            return;

        // Ammo display
        GUI.Label(
            new Rect(Screen.width - 220, Screen.height - 70, 190, 45),
            $"AMMO  {weapon.CurrentAmmo}/{weapon.MagazineSize}",
            ammoStyle
        );

        GUI.color = Color.white;
        GUI.color = weapon.LastShotHitTarget ? Color.green : Color.white;
        // Crosshair dimensions and screen center
        float centerX = Screen.width * 0.5f;
        float centerY = Screen.height * 0.5f;
        float gap = 5f;
        float length = 12f;
        float thickness = 2f;

        
        
        // Left arm
        GUI.DrawTexture(
            new Rect(centerX - gap - length,
                     centerY - thickness / 2f,
                     length, thickness),
            Texture2D.whiteTexture
        );

        // Right arm
        GUI.DrawTexture(
            new Rect(centerX + gap,
                     centerY - thickness / 2f,
                     length, thickness),
            Texture2D.whiteTexture
        );

        // Top arm
        GUI.DrawTexture(
            new Rect(centerX - thickness / 2f,
                     centerY - gap - length,
                     thickness, length),
            Texture2D.whiteTexture
        );

        // Bottom arm
        GUI.DrawTexture(
            new Rect(centerX - thickness / 2f,
                     centerY + gap,
                     thickness, length),
            Texture2D.whiteTexture
        );

        GUI.color = Color.white;
    }
}
