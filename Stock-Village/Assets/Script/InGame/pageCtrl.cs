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

    void Start()
    {
        popUp = false;
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
        popUp = true;
    }
    
    public void edBtnClick()
    {
        closeAll(); //��� â ������
        editPage.SetActive(true);
        popUp = true;
    }

    public void cashEdBtnClick()
    {
        closeAll(); //��� â ������
        cashEditPage.SetActive(true);
        popUp = true;
    }
    public void profileBtnClick()
    {
        //�����ִ� â�� ������ ��� â ������
        if (popUp == true)
        {
            closeAll();
            popUp = false; //��� �˾�â�� ��������
        }
        //��� â�� �����ִٸ� setPage open
        else
        {
            profilePage.SetActive(true);
            popUp = true; //�˾�â�� ��������
        }
    }

    public void backBtnClick()
    {
        closeAll(); //��� â ������
        setPage.SetActive(true);
        popUp = true;
    }

    void closeAll()
    {
        setPage.SetActive(false);
        optionPage.SetActive(false);
        editPage.SetActive(false);
        profilePage.SetActive(false);
        cashEditPage.SetActive(false);

        popUp = false;
    }
}
