using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#region Commentaires / Explications
/* Petit script qui contrôle un "Curseur UI" pour remplacer le curseur de base
 * Placer le script sur un UI Image
 * Remplissez le tableau de Sprites (UI Cursor Images) si vous changez de curseur en cours de jeu (exemple : un oeil sur des objets en particulier, une cible sur une cible etc...)
 * Utilisation par script :
 * - GetUiCursorLock() : renvoi true si l'UI Curseur est lock au centre de l'écran
 * - GetUICursorVisible() : renvoi true si l'UI Curseur est visible
 * - SetUICursorLock(bool locked) : Lock et delock l'UI Curseur au centre de l'écran
 * - SetUICursorVisible(bool visible) : rend visible ou invisible l'UI Curseur à l'écran
 * - ChangeUICursor(int numeroSprite = -1) : permet de changer le sprite de l'UI Curseur en fonction de l'index du tableau. 
 * laisser vide pour réinitialiser l'UI Curseur par défaut
*/
#endregion

public class UICursor : MonoBehaviour
{

    #region Attributs
    [SerializeField] private bool uiCursorLocked = false;
    [SerializeField] private Sprite[] uiCursorImages;
    private bool uiCursorVisible = true;
    private Image uiCursorImage;
    private Sprite uiCursorSpritePrincipal;
    #endregion

    #region Unity Fonctions
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        uiCursorImage = GetComponent<Image>();
        uiCursorImage.raycastTarget = false;
        uiCursorSpritePrincipal = uiCursorImage.sprite;

    }

    // Update is called once per frame
    void Update()
    {

        if (!uiCursorLocked && uiCursorVisible)
            uiCursorImage.transform.position = Input.mousePosition;

    }

    #endregion

    #region Mes Fonctions
    public bool GetUiCursorLock()
    {
        return uiCursorLocked;
    }

    public bool GetUICursorVisible()
    {
        return uiCursorVisible;
    }

    public void SetUICursorLock(bool locked)
    {
        if (locked)
        {
            uiCursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }

        else
        {
            uiCursorLocked = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetUICursorVisible(bool visible)
    {
        if (!visible)
        {
            uiCursorVisible = false;
            uiCursorImage.enabled = false;
        }

        else
        {
            uiCursorVisible = true;
            uiCursorImage.enabled = true;
        }
    }

    public void ChangeUICursor(int numeroSprite = -1)
    {
        if (numeroSprite == -1)
            uiCursorImage.sprite = uiCursorSpritePrincipal;
        else

            uiCursorImage.sprite = uiCursorImages[numeroSprite];
    }
    #endregion
}
