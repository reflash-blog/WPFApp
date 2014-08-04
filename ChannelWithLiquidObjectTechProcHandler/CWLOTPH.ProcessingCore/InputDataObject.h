#pragma once
#include "stdafx.h"

class InputDataObject
{
public:
	InputDataObject();
	~InputDataObject();

	// ������������ ������
	double Mu;
	double n;
	double Tgamma;
	double alphaU;
	double b;

	// �������������� ������
	double L;
	double W;
	double H;

	// �������� ������
	double Vu;
	double Tu;

	// �������� ���������
	double Ro;
	double T0;
	double C;

	// ���
	double step;

};

