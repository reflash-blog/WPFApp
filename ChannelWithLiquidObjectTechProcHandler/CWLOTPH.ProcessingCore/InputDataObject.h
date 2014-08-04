#pragma once
#include "stdafx.h"

class InputDataObject
{
public:
	InputDataObject();
	~InputDataObject();

	// Эмпирический вектор
	double Mu;
	double n;
	double Tgamma;
	double alphaU;
	double b;

	// Геометрический вектор
	double L;
	double W;
	double H;

	// Режимный вектор
	double Vu;
	double Tu;

	// Свойства материала
	double Ro;
	double T0;
	double C;

	// Шаг
	double step;

};

