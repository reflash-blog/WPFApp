#include "stdafx.h"
#include <json/json.h>
#include "InputDataObject.h"
#include "OutputDataObject.h"
#include "ProcessingCore.h"

int _tmain(int argc, _TCHAR* argv[])
{
	std::string JSONString; 
	std::string JSONFile; // ��� ����� � �������� �������
	ProcessingCore* pCore = new ProcessingCore(); // ��������� ��������������� ����
	InputDataObject* inpDObj = new InputDataObject(); // ������� ������
	OutputDataObject* outDObj = new OutputDataObject(); // �������� ������

	if (argc>0){
		JSONFile = "data.json"; // ������ �������� ��� ������ - ��� �����
	}


	inpDObj = pCore->parseJSONString(JSONFile); // �������� ������ �� ������ ��������� JSON
	outDObj = pCore->processData(inpDObj); // �������� �������� ������ ���������� �� ��������
	JSONString = pCore->createJSONString(outDObj); // �������� ������ �� �������
	pCore->setOutputJSONFile(JSONString); // ������� � ����

	// ������ ������
	delete pCore;
	delete inpDObj;
	delete outDObj;

	return 0;
}