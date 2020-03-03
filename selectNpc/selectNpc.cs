using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectNpc : MonoBehaviour
{

    public GameObject[] characterprefabs;
    public UIInput nameInput;
    private GameObject[] charactergameobject;
    private int selectIndex=0;
    private int length;
    // Start is called before the first frame update
    void Start()
    {
        length = characterprefabs.Length;
        charactergameobject = new GameObject[length];
        for(int i=0;i<length;i++)
        {
            charactergameobject[i] = GameObject.Instantiate(characterprefabs[i], this.transform.position, this.transform.rotation);
        }
        showcharater();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void showcharater()
    {
        charactergameobject[selectIndex].SetActive(true);
        for(int i=0;i<length;i++)
        {
            if(i!=selectIndex)
            {
                charactergameobject[i].SetActive(false);
            }
        }
    }

    public void onnextbuttonclick()
    {
        selectIndex++;
        selectIndex %= length;
        showcharater(); 
    }

    public void onprevbuttonclick()
    {
        selectIndex--;
        if (selectIndex == -1)
        {
            selectIndex = length - 1;
        }
        showcharater();
    }

    public void onokbuttonclick()
    {
        PlayerPrefs.SetInt("selectcharaindex", selectIndex);
        PlayerPrefs.SetString("name", nameInput.value);
        SceneManager.LoadScene(2);
    }
}
