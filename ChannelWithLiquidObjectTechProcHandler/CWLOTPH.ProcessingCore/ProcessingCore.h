#pragma once
#include "stdafx.h"

class ProcessingCore
{
public:
	ProcessingCore();
	~ProcessingCore();
	InputDataObject* parseJSONString(std::string JSONString); // Метод парсинга строки
	OutputDataObject* processData(InputDataObject* inDObj); // Метод обработки данных
	std::string createJSONString(OutputDataObject* outDObj); // Метод формирования выходной JSON строки
	void setOutputJSONFile(std::string JSONString); // Метод вывода в файл
private:
	double Temperature(double Q, double qa, double qr, double coord, InputDataObject* inpObj);
	double Viscosity(double T, InputDataObject* inpObj);

};

