#pragma once
#include "stdafx.h"

class OutputDataObject
{
public:
	OutputDataObject();
	~OutputDataObject();

	double channelPerformance;
	double productFinalTemperature;
	double productFinalViscosity;
	double *lengthCoordinates;
	double *temperature;
	double *viscosity;
	int arrSize;
	int tickCount;
};

