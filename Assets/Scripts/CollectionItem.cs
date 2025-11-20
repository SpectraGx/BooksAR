using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItem : MonoBehaviour
{
    public Image imageIcon;
    public GameObject lockedOverlay;
    public Button myButton;

    private BookData bookData;
    private CollectionManager collectionManager;

    public void Setup(BookData data, CollectionManager manager)
    {
        bookData = data;
        collectionManager = manager;

        bool desbloqueado = PlayerPrefs.GetInt(bookData.bookID, 0) == 1;

        imageIcon.sprite = bookData.bookIcon;

        if (desbloqueado)
        {
            lockedOverlay.SetActive(false);
            myButton.interactable = true;
            imageIcon.color = Color.white;
        }
        else
        {
            lockedOverlay.SetActive(true);
            myButton.interactable = false;
            imageIcon.color = Color.gray;
        }

        myButton.onClick.AddListener(() => manager.ShowBookDetails(bookData));
    }
}
