using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    public void SetRecipeText(RecipeSO recipeSO)
    {
        recipeText.text = recipeSO.recipeName;
        foreach (Transform child in iconContainer)
            Destroy(child.gameObject);
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
