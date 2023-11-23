using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{
    [SerializeField]
    private int maxValue = 100;

    private int _value;
    public int Value
    {
        get { return _value; }
        private set
        {
            _value = Mathf.Clamp(value, 0, maxValue);
            // Démarrez la coroutine pour une transition en douceur
            StartCoroutine(SmoothTransition());
        }
    }

    [SerializeField]
    private Image topBarImage;

    [SerializeField]
    private Image bottomBarImage;

    private float _fullWidth;
    private float TargetWidth => Value * _fullWidth / maxValue;

    private void Awake()
    {
        _fullWidth = topBarImage.rectTransform.sizeDelta.x;
        // Initialisez la valeur à la valeur maximale au démarrage
        Value = maxValue;
    }

    private void Update()
    {
        // Si vous souhaitez changer la valeur en cliquant sur les boutons gauche et droit de la souris
       /* if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Change(20);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Change(-20);
        }
       */
    }

    private void AdjustBarWidth()
    {
        // Mettez à jour la largeur des barres immédiatement
        topBarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TargetWidth);
        bottomBarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _fullWidth - TargetWidth);
    }

    private IEnumerator SmoothTransition()
    {
        // La durée de la transition (en secondes)
        float transitionDuration = 0.5f;

        // La largeur initiale de la barre
        float initialWidth = topBarImage.rectTransform.rect.width;

        // Temps écoulé pendant la transition
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Calculez la nouvelle largeur interpolée
            float newWidth = Mathf.Lerp(initialWidth, TargetWidth, elapsedTime / transitionDuration);

            // Appliquez la nouvelle largeur à la barre
            topBarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

            // Mettez à jour le temps écoulé
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Assurez-vous que la largeur finale est correcte
        topBarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, TargetWidth);
    }

    public void Change(int amount)
    {
        // Changez la valeur en fonction du montant spécifié
        Value += amount;
    }
}
