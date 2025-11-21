using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionManager : MonoBehaviour
{
    [Header("My data")]
    public List<BookData> allBooks;

    [Header("UI References")]
    public Transform contentGrid;
    public GameObject prefabButtonItem;

    [Header("Book Details UI")]
    public GameObject bookDetailsPanel;
    public Image imageDetail;
    public TextMeshProUGUI titleDetail;
    public TextMeshProUGUI descriptionDetail;

    void Start()
    {
        //GenerationGrid();
        bookDetailsPanel.SetActive(false);
    }

    private void OnEnable() {
        GenerationGrid();
    }

    public void GenerationGrid()
    {
        /*
        foreach (Transform child in contentGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (BookData book in allBooks)
        {
            GameObject newItem = Instantiate(prefabButtonItem, contentGrid);
            CollectionItem itemScript = newItem.GetComponent<CollectionItem>();
            itemScript.Setup(book, this);
        }
        */

        foreach (Transform child in contentGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (BookData book in allBooks)
        {
            GameObject newItem = Instantiate(prefabButtonItem, contentGrid);
            CollectionItem itemScript = newItem.GetComponent<CollectionItem>();
            itemScript.Setup(book, this);
        }
    }

    public void ShowBookDetails(BookData book)
    {
        imageDetail.sprite = book.bookIcon;
        titleDetail.text = book.bookTitle;
        descriptionDetail.text = book.bookDescription;

        bookDetailsPanel.SetActive(true);
    }

    public void CloseBookDetails()
    {
        bookDetailsPanel.SetActive(false);
    }

    public void ClearAllProgress()
    {
        PlayerPrefs.DeleteAll();
        GenerationGrid();
        Debug.Log("Progreso borrado");
    }
}
