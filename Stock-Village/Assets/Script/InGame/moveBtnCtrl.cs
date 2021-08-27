using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moveBtnCtrl : MonoBehaviour
{
    public Camera[] sectorCamera; //���ͺ� ī�޶� ����Ʈ
    public Text SectorName; //ȭ�鿡 ��� ���͸�

    string[] SectorNames = { "Full Shot", "IT", "Consumer", "Financial", "Health Care", "Industrial", "Real Estate" };
    private int SectorIndex;

    void Start()
    {
        SectorIndex = 0;
    }
    void FixedUpdate()
    {
        UpdateKeyboard();
        
        //Ǯ�� ī�޶� �ƴ� ���, ī�޶� �̵��� ���� ���͸� ������Ʈ
        if(SectorIndex != 0)
        {
           UpdateSectorName();
        }
    }
    void UpdateKeyboard()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //�����ִ� �˾�â�� �ִٸ� �������� ����

        if (Input.GetKey(KeyCode.LeftArrow)) { Camera.main.transform.Translate(-0.3f, 0.0f, 0.0f); } // �������� �̵�
        if (Input.GetKey(KeyCode.RightArrow)) { Camera.main.transform.Translate(0.3f, 0.0f, 0.0f); } // ���������� �̵�
        if (Input.GetKey(KeyCode.UpArrow)) { Camera.main.transform.Translate(0.0f, 0.2f, 0.3f); } // ������ �̵�
        if (Input.GetKey(KeyCode.DownArrow)) { Camera.main.transform.Translate(0.0f, -0.2f, -0.3f); } // �ڷ� �̵�

    }
    public void leftBtnClick()
    {
        //���� ī�޶� �ε���
        SectorIndex = (SectorIndex - 1);
        if (SectorIndex <= 0) { SectorIndex = 6; }
        SectorName.text = SectorNames[SectorIndex];

        //�̵��� ī�޶� position, rotation ��ȯ
        Vector3 pos = sectorCamera[SectorIndex].transform.position;
        Quaternion rot = sectorCamera[SectorIndex].transform.rotation;

        //ī�޶� �̵�
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rot, 1);
    }
    public void rightBtnClick()
    {
        //���� ī�޶� �ε���
        SectorIndex = (SectorIndex + 1);
        if (SectorIndex > 6) { SectorIndex = 1; }
        SectorName.text = SectorNames[SectorIndex];

        //�̵��� ī�޶� position, rotation ��ȯ
        Vector3 pos = sectorCamera[SectorIndex].transform.position;
        Quaternion rot = sectorCamera[SectorIndex].transform.rotation;

        //ī�޶� �̵�
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rot, 1);
    }
    public void fullShotBtnClick()
    {
        //���� ī�޶� �ε���(Ǯ�� ī�޶�)
        SectorIndex = 0;
        SectorName.text = SectorNames[SectorIndex];

        //�̵��� ī�޶� position, rotation ��ȯ
        Vector3 pos = sectorCamera[SectorIndex].transform.position;
        Quaternion rot = sectorCamera[SectorIndex].transform.rotation;

        //ī�޶� �̵�
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rot, 1);
    }

    //ī�޶� �̵��� ���� ���͸� ������Ʈ
    void UpdateSectorName()
    {
        float minDist = 100000000000;
        float dist = 0f;
        int tmpIdx = 1;

        for (int i = 1; i <= 6; i++)
        {
            dist = Vector3.Distance(Camera.main.transform.position, sectorCamera[i].transform.position);
            if (minDist > dist)
            {
                tmpIdx = i;
                minDist = dist;
            }
        }
        SectorIndex = tmpIdx;
        SectorName.text = SectorNames[SectorIndex]; //���͸� ������Ʈ
    }
}