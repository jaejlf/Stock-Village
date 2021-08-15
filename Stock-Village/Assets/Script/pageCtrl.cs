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

    void Start()
    {
        popUp = false;
        PageList.Add(setPage);
        PageList.Add(optionPage);
        PageList.Add(editPage);
        PageList.Add(profilePage);
        PageList.Add(cashEditPage);
    }
    public void setButtonClick()
    {
        //�����ִ� â�� ������ ��� â ������
        if(popUp == true)
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
