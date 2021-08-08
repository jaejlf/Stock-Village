using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class pageCtrl : MonoBehaviour
{
    public GameObject setPage;
    public GameObject optionPage;
    public GameObject editPage;
    public GameObject profilePage;
    public GameObject cashEditPage;

    public bool popUp; //�˾�â�� �����ִ��� Ȯ��

    List<GameObject> PageList = new List<GameObject>();
    private void Start()
    {
        PageList.Add(setPage);
        PageList.Add(optionPage);
        PageList.Add(editPage);
        PageList.Add(profilePage);
        PageList.Add(cashEditPage);
    }
    public void setButtonClick()
    {
        int open = 0; //0 : close, 1 : open

        //�����ִ� â �ִ��� üũ
        foreach(var page in PageList)
        {
            if(page.activeSelf == true)
            {
                open = 1;
                break;
            }
        }
        //�����ִ� â�� ������ ��� â ������
        if(open == 1)
        {
            closeAll();
            popUp = false; //��� �˾�â�� ��������
        }
        //��� â�� �����ִٸ� setPage open
        else
        {
            setPage.SetActive(true);
            popUp = true; //�˾�â�� ��������
        }
    }

    public void opBtnClick()
    {
        closeAll(); //��� â ������
        optionPage.SetActive(true);
    }
    
    public void edBtnClick()
    {
        closeAll(); //��� â ������
        editPage.SetActive(true);
    }

    public void profileBtnClick()
    {
        closeAll(); //��� â ������
        profilePage.SetActive(true);
    }

    public void backBtnClick()
    {
        closeAll(); //��� â ������
        setPage.SetActive(true);
    }

    public void cashEd_backBtnClick()
    {
        closeAll(); //��� â ������
        profilePage.SetActive(true);
    }
    void closeAll()
    {
        setPage.SetActive(false);
        optionPage.SetActive(false);
        editPage.SetActive(false);
        profilePage.SetActive(false);
        cashEditPage.SetActive(false);
    }
}
