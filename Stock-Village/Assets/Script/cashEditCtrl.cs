using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class cashEditCtrl : MonoBehaviour
{
    public portfolio myPortfolio;

    //cashEditPage �ʵ�
    public Text cash;
    public InputField input_cash;
    public Text editedCash;

    void Update()
    {
        cash.text = myPortfolio.Cash.ToString();
    }

    public void plusBtnClick()
    {
        editedCash.text = (double.Parse(cash.text) + double.Parse(input_cash.text)).ToString();
    }
    public void minusBtnClick()
    {
        editedCash.text = (double.Parse(cash.text) - double.Parse(input_cash.text)).ToString();
    }
    public void editEndBtnClick()
    {
        if (input_cash.text == "")
        {
            Debug.Log("��� �ʵ忡 ���� �Է��ϼ���.");
        }
        else
        {
            if(cash.text == editedCash.text) { Debug.Log("plus �Ǵ� minus ��ư�� Ŭ���ϼ���."); }
            if(double.Parse(editedCash.text) < 0) { Debug.Log("��� ������ ������ �����մϴ�."); }
            else
            {
                myPortfolio.Cash = double.Parse(editedCash.text);

                //�ʵ� �ʱ�ȭ
                input_cash.text = "";
                editedCash.text = "";
            }
        }
    }
}