#pragma once
#include "stdafx.h"

class ProcessingCore
{
public:
	ProcessingCore();
	~ProcessingCore();
	InputDataObject* parseJSONString(std::string JSONString); // ����� �������� ������
	OutputDataObject* processData(InputDataObject* inDObj); // ����� ��������� ������
	std::string createJSONString(OutputDataObject* outDObj); // ����� ������������ �������� JSON ������
	void setOutputJSONFile(std::string JSONString); // ����� ������ � ����
private:
	double Temperature(double Q, double qa, double qr, double coord, InputDataObject* inpObj);
	double Viscosity(double T, InputDataObject* inpObj);

};

