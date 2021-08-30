using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class movePosCtrl : MonoBehaviour
{
    public GameObject movePosPage; //�ǹ� ��ġ ���� ������
    private RaycastHit hit; //���콺�� Ŭ���� ��ü
    public Text SectorName;

    public Button[] posBtn; //��ư �迭
    public Text[] btnText; //��ư ���� �ؽ�Ʈ �迭

    Dictionary<string, List<string>> sectorCtrl
        = new Dictionary<string, List<string>>() { { "IT", new List<string> { "MSFT", "ORCL", "AAPL", "IBM", "GOOGL", "FB", "DIS", "NFLX" } },
                                                   { "Industrial", new List<string> { "HON", "UNP", "LMT" } },
                                                   { "Consumer", new List<string> { "AMZN", "TSLA", "SBUX", "NKE", "WMT", "COST", "KO", "PEP" } },
                                                   { "Health Care", new List<string> { "JNJ", "PFE", "UNH" } },
                                                   { "Real Estate", new List<string> { "AMT", "EQIX", "PLD", "O" } },
                                                   { "Financial", new List<string> { "V", "PYPL", "BAC", "C" } } };

    void FixedUpdate()
    {
        rigntClick();
    }

    void rigntClick()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //�����ִ� �˾�â�� �ִٸ� �������� ����

        //��Ŭ�� ��
        if (Input.GetMouseButtonDown(1))
        {
            string symbol = "";
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "stock" || hit.collider.gameObject.tag == "portfolioMode")//�ǹ� ������Ʈ�� Ŭ���� ���
                {
                    movePageSet();
                }
            }
        }

    }

    void movePageSet()
    {
        List<string> secList; //���� ���� ���� ����Ʈ
        string clicekd = hit.collider.gameObject.name; //Ŭ���� �ǹ� ������Ʈ �̸�
        string curSector = SectorName.text; //���� ���͸�

        if (!sectorCtrl.ContainsKey(curSector)) { return; } //Full Shot ����� ���, ���� X
        else
        {
            movePosPage.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = true;

            secList = sectorCtrl[SectorName.text]; //���� ���� ���� ����Ʈ

            //��ư �ʱ� ����
            for (int i = 0; i < 8; i++)
            {
                btnText[i].text = "-"; //�ؽ�Ʈ �ʱ�ȭ
                posBtn[i].interactable = false; //��ư ��Ȱ��ȭ
            }

            //���ͺ� ��ư ����
            for (int i = 0; i < secList.Count; i++)
            {
                btnText[i].text = secList[i];
                if (!btnText[i].text.Equals(clicekd)) { posBtn[i].interactable = true; } //���� ������ �ƴ� ��츸 ��ư Ȱ��ȭ
            }
        }
    }

    public void moveBtnClick()
    {
        Vector3 targetPos, beforePos;
        int index = -1;

        //Ŭ���� ��ư ã��
        string clicked = EventSystem.current.currentSelectedGameObject.name;
        for (int i = 0; i < 8; i++)
        {
            if (clicked.Equals("pos" + i))
            {
                index = i;
                break;
            }
        }

        //�ǹ� ��ġ ��ȯ
        targetPos = hit.collider.gameObject.transform.position;
        beforePos = GameObject.Find(btnText[index].text).gameObject.transform.position;

        //�ǹ� �̵�
        GameObject.Find(btnText[index].text).gameObject.transform.position = new Vector3(targetPos.x, beforePos.y, targetPos.z);
        hit.collider.gameObject.transform.position = new Vector3(beforePos.x, targetPos.y, beforePos.z);

        //������ �ݱ�
        movePosPage.SetActive(false);
        GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = false;
    }

    public void exitBtnClick()
    {
        movePosPage.SetActive(false);
        GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = false;
    }
}
