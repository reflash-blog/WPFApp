// CWLOTPH.ProcessingCore.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <json/json.h>
#include <cmath>
#include <time.h>

ProcessingCore::ProcessingCore()
{
}

ProcessingCore::~ProcessingCore()
{
}


InputDataObject* ProcessingCore::parseJSONString(std::string JSONFile)
{
	InputDataObject* inpDObj = new InputDataObject(); 
	Json::Value root;   // will contains the root value after parsing.
	Json::Reader reader;
	std::ifstream _JSONFile(JSONFile, std::ifstream::binary);
	bool parsingSuccessful = reader.parse(_JSONFile, root);
	if (!parsingSuccessful)
	{
		// report to the user the failure and their locations in the document.
		std::cout << "Failed to parse configuration\n" << reader.getFormatedErrorMessages();
		return inpDObj;
	}
	inpDObj->Mu = root["EmpiricalVector"].get("Mu", 0).asDouble();
	inpDObj->n = root["EmpiricalVector"].get("n", 0).asDouble();
	inpDObj->Tgamma = root["EmpiricalVector"].get("Tgamma", 0).asDouble();
	inpDObj->alphaU = root["EmpiricalVector"].get("alphaU", 0).asDouble();
	inpDObj->b = root["EmpiricalVector"].get("b", 0).asDouble();
	inpDObj->L = root["GeometryVector"].get("L", 0).asDouble();
	inpDObj->W = root["GeometryVector"].get("W", 0).asDouble();
	inpDObj->H = root["GeometryVector"].get("H", 0).asDouble();
	inpDObj->Vu = root["ModeVector"].get("Vu", 0).asDouble();
	inpDObj->Tu = root["ModeVector"].get("Tu", 0).asDouble();
	inpDObj->Ro = root["MaterialProperties"].get("Ro", 0).asDouble();
	inpDObj->T0 = root["MaterialProperties"].get("T0", 0).asDouble();
	inpDObj->C = root["MaterialProperties"].get("C", 0).asDouble();
	inpDObj->step = root.get("DiscretizationStep", 0).asDouble();


	return inpDObj;
}

OutputDataObject* ProcessingCore::processData(InputDataObject* inpDObj)
{
	// Выделяем память
	OutputDataObject* oDObj = new OutputDataObject();
	double Fd, gamma, Q, Qk, qa, qr, coord;
	int i;
	int arrSize = (int)(inpDObj->L / inpDObj->step) + 1;
	if ((arrSize * inpDObj->step) < (inpDObj->L + inpDObj->step / 2))
		arrSize++;
	double *T = new double[arrSize];
	double *X = new double[arrSize];
	double *nu = new double[arrSize];
	double *V = new double[arrSize];
	long startTime, endTime;

	// Расчеты
	Fd = 0.125 * (inpDObj->H / inpDObj->W) * (inpDObj->H / inpDObj->W) 
		- 0.625 * (inpDObj->H / inpDObj->W) + 1;
	// скорость диффер. вывода
	gamma = inpDObj->Vu / inpDObj->H;
	// производительность канала
	Q = (inpDObj->W * inpDObj->H / 2) * inpDObj->Vu * Fd;
	Qk = inpDObj->Ro * Q;
	//Расчёт удельных тепловых потоков
	qa = inpDObj->W * (inpDObj->alphaU / inpDObj->b - inpDObj->alphaU * inpDObj->Tu
		+ inpDObj->alphaU * inpDObj->Tgamma); //heatFluxW
	qr = inpDObj->W * inpDObj->H * inpDObj->Mu * pow(gamma, inpDObj->n + 1); //heatFluxN

	coord = 0;

	startTime = clock();
	i = 0;
	while (coord < (inpDObj->L + inpDObj->step / 2))
	{
		X[i] = coord;
		T[i] = Temperature(Q, qa, qr, coord,inpDObj);
		V[i] = Viscosity(T[i],inpDObj);
		coord += inpDObj->step;
		i++;
	}
	endTime = clock();

	// Запись данных в объект
	oDObj->lengthCoordinates = X;
	oDObj->temperature = T;
	oDObj->viscosity = V;
	oDObj->channelPerformance = Qk;
	oDObj->productFinalTemperature = T[i - 1];
	oDObj->productFinalViscosity = V[i - 1];
	oDObj->tickCount = endTime - startTime;
	oDObj->arrSize = arrSize;

	return oDObj;
}

std::string ProcessingCore::createJSONString(OutputDataObject* outDObj)
{
	Json::Value jsonRoot;
	Json::Value jsonValue;
	Json::Value jsonArray(Json::arrayValue);


	jsonRoot["Qk"] = outDObj->channelPerformance;
	jsonRoot["Tend"] = outDObj->productFinalTemperature;
	jsonRoot["Vend"] = outDObj->productFinalViscosity;
	jsonRoot["tickCount"] = outDObj->tickCount;

	jsonArray.clear();
	for (int i = 0; i < outDObj->arrSize; i++)
	{
		jsonArray.append(Json::Value(outDObj->lengthCoordinates[i]));
	}

	jsonRoot["lengthCoordinates"] = jsonArray;

	jsonArray.clear();
	for (int i = 0; i < outDObj->arrSize; i++)
	{
		jsonArray.append(Json::Value(outDObj->temperature[i]));
	}

	jsonRoot["temperature"] = jsonArray;

	jsonArray.clear();
	for (int i = 0; i < outDObj->arrSize; i++)
	{
		jsonArray.append(Json::Value(outDObj->viscosity[i]));
	}

	jsonRoot["viscosity"] = jsonArray;


	Json::StyledWriter writer;
	std::string out_string = writer.write(jsonRoot);
	std::cout << out_string;
	return out_string;
}

void ProcessingCore::setOutputJSONFile(std::string JSONString)
{
	std::ofstream ofs("data.json.result"); // Поток вывода

	if (ofs)
	{
		ofs << JSONString;  // Вывод строки в файл
	}
	else std::cerr << "Can't open file\n";
	ofs.close(); // Закрытие потока вывода
}

double ProcessingCore::Temperature(double Q, double qa, double qr, double coord, InputDataObject* inpObj)
{
	return inpObj->Tgamma + (1 / inpObj->b) * log(((inpObj->b * qr + inpObj->W * inpObj->alphaU) /
		(inpObj->b * qa)) * (1 - exp(-(inpObj->b * qa * coord) / (inpObj->Ro * inpObj->C * Q)))
		+ exp(inpObj->b * (inpObj->T0 - inpObj->Tgamma - (qa * coord) / (inpObj->Ro * inpObj->C * Q))));
}
		 // рассчёт вязкости материала 
double ProcessingCore::Viscosity(double T, InputDataObject* inpObj)
{
	return inpObj->Mu * exp(-inpObj->b * (T - inpObj->Tgamma)) * pow((inpObj->Vu / inpObj->H), inpObj->n - 1);
}

