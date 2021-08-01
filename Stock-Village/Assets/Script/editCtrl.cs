using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class editCtrl : MonoBehaviour
{
    public GameObject optionPage;
    public GameObject editPage;
    public GameObject setPage;
    public GameObject cashEditPage;

    public portfolio myPortfolio;

    //editPage �ʵ�
    public InputField symbol;
    public InputField avgCost;
    public InputField shares;
    public InputField date;

    public void editBtnClick()
    {
        optionPage.SetActive(false);
        setPage.SetActive(false);

        //â�� ����������
        if (editPage.activeSelf == true)
        {
            editPage.SetActive(false);
        }
        else
        {
            editPage.SetActive(true);
        }
    }
    public void buyBtnClick()
    {
        if (checkEditData() == true)
        {
            Debug.Log("BUY button Click !");
            myPortfolio.BUY(symbol.text, double.Parse(avgCost.text), Int32.Parse(shares.text), date.text, 0);
            inputsClear();
        }
        else
        {
            Debug.Log("��� �ʵ忡 ���� �Է��ϼ���.");
        }

    }
    public void sellBtnClick()
    {
        if (checkEditData() == true)
        {
            Debug.Log("SELL button Click !");
            myPortfolio.SELL(symbol.text, double.Parse(avgCost.text), Int32.Parse(shares.text), date.text, 1);
            inputsClear();
        }
        else
        {
            Debug.Log("��� �ʵ忡 ���� �Է��ϼ���.");
        }
    }
    void inputsClear()
    {
        symbol.text = "";
        date.text = "";
        shares.text = "";
        avgCost.text = "";
    }
    bool checkEditData()
    {
        string[] editData = { symbol.text, date.text, shares.text, avgCost.text };
        for (int i = 0; i < editData.Length; i++)
        {
            if (editData[i] == "")
            {
                return false;
            }
        }
        return true;
    }
}
