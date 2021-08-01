using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InGame : MonoBehaviour
{
    public StockList stockList;
    public portfolio myPortfolio;

    public Text totalGain;

    public Toggle mystockCk; //only my stocks ��� üũ
    public GameObject tog0; //��� on,off üũ ǥ��

    public GameObject[] totalMode; //totalMode �ǹ� ������Ʈ
    public GameObject[] portfolioMode; //portfolioMode �ǹ� ������Ʈ
    public GameObject[] others; //�ٹ̱�� �ǹ� ������Ʈ

    public Toggle weather; //weather �ɼ� üũ
    public GameObject tog1; //��� on,off üũ ǥ��
    public Toggle mktime; //market time �ɼ� üũ
    public GameObject tog2; //��� on,off üũ ǥ��
    public Toggle scale; //scaling �ɼ� üũ
    public GameObject tog3; //��� on,off üũ ǥ��

    void Start()
    {
        myPortfolio.renew = false; //��Ʈ������ ���� �÷��� false
        totalBtnClick();
    }
    void Update()
    {
        if (mystockCk.isOn)
        {
            tog0.SetActive(true);
            portfolioBtnClick();
        }
        else
        {
            tog0.SetActive(false);
            totalBtnClick();
        }
        //��Ʈ�������� ���ŵ� ���
        if (myPortfolio.renew)
        {
            settingPortfolio();
            myPortfolio.renew = false;
        }
        toggleControl();
        
    }
    //totalMode
    public void totalBtnClick()
    {
        //portfolioMode �±� ��Ȱ��ȭ
        foreach (GameObject tmp in myPortfolio.pfBuildings)
        {
            tmp.SetActive(false);
        }
        //totalMode �±� Ȱ��ȭ
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(true);
        }
        //others �±� Ȱ��ȭ
        for (int i = 0; i < others.Length; i++)
        {
            others[i].SetActive(true);
        }
    }

    //portfolioMode
    public void portfolioBtnClick()
    {
        //totalMode �±� ��Ȱ��ȭ
        for (int i = 0; i < totalMode.Length; i++)
        {
            totalMode[i].SetActive(false);
        }
        //others �±� ��Ȱ��ȭ
        for (int i = 0; i < others.Length; i++)
        {
            others[i].SetActive(false);
        }
        //portfolioMode �±� Ȱ��ȭ
        for (int i = 0; i < portfolioMode.Length; i++)
        {
            portfolioMode[i].SetActive(true);
        }
        settingPortfolio(); //��Ʈ������ �ǹ� ��ġ
    }

    //��Ʈ������ �ǹ� ��ġ
    void settingPortfolio()
    {
        string path = "";

        //��Ʈ������ �ǹ� ��ü ����
        foreach (GameObject tmp in myPortfolio.pfBuildings) { Destroy(tmp); }
        myPortfolio.pfBuildings.Clear();

        //��ü ���ͱ� = ��ü �򰡱ݾ� - ���� ��ü ���ڱݾ�
        totalGain.text = (myPortfolio.update_Total_Stprice() - myPortfolio.update_Total_StInvest()).ToString();

        //�ǹ� ��ġ
        foreach (var key in myPortfolio.stockInfo.Keys.ToList())
        {
            if (myPortfolio.stockInfo[key].shares == 0) { continue; } //���������� 0�� ��� ����

            //���� �ǹ� ��ġ ��ȯ
            Vector3 pos;
            pos = totalMode[0].transform.Find(key).position;

            //�ǹ� ��ġ
            path = "Prefabs/Buildings/" + key;
            GameObject a = (GameObject)Instantiate(Resources.Load(path));
            a.name = key;
            a.gameObject.tag = "portfolioMode";
            myPortfolio.pfBuildings.Add(a);

            a.transform.position = new Vector3(pos.x, pos.y, pos.z);

            //scale �ɼ��� ����������
            if (scale.isOn)
            {
                Debug.Log("scale option ON !");
                /*
                //���ͷ��� ���� ũ�� �����ϸ� (1 ~ 5�ܰ�)
                double ratio = (myPortfolio.updateCost(key) - myPortfolio.updateInvest(key)) / myPortfolio.updateInvest(key) * 100; // (�򰡱ݾ� - ��� �ż� �ݾ�) / ��� �ż� �ݾ� * 100
                double scale = 1f;
                if (ratio <= -10) { scale = 0.5f; }
                else if (ratio < 0) { scale = 0.75f; }
                else if (ratio >= 0 && ratio < 10) { scale = 1f; }
                else if (ratio >= 10 && ratio <= 30) { scale = 1.25f; }
                else { scale = 1.5f; }
            
                a.transform.localScale = new Vector3(scale * a.transform.localScale.x, scale * a.transform.localScale.y, scale * a.transform.localScale.z);
                */
            }
            else
            {
                Debug.Log("scale option OFF !");
            }
        }
    }
    void toggleControl()
    {
        //weather �ɼ�
        if (weather.isOn) {
            tog1.SetActive(true);
        }
        else
        {
            tog1.SetActive(false);
        }

        //market time �ɼ�
        if (mktime.isOn)
        {
            tog2.SetActive(true);
        }
        else
        {
            tog2.SetActive(false);
        }
        //scale �ɼ�
        if (scale.isOn)
        {
            tog3.SetActive(true);
            settingPortfolio();
        }
        else
        {
            tog3.SetActive(false);
            settingPortfolio();
        }
    }
}
