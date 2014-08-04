#include "stdafx.h"
#include <json/json.h>
#include "InputDataObject.h"
#include "OutputDataObject.h"
#include "ProcessingCore.h"

int _tmain(int argc, _TCHAR* argv[])
{
	std::string JSONString; 
	std::string JSONFile; // Имя файла с входными данными
	ProcessingCore* pCore = new ProcessingCore(); // Экземпляр обрабатывающего ядра
	InputDataObject* inpDObj = new InputDataObject(); // Входные данные
	OutputDataObject* outDObj = new OutputDataObject(); // Выходные данные

	if (argc>0){
		JSONFile = "data.json"; // Первый аргумент кмд строки - имя файла
	}


	inpDObj = pCore->parseJSONString(JSONFile); // Получаем объект из строки парсингом JSON
	outDObj = pCore->processData(inpDObj); // Получаем выходной объект обработкой по формулам
	JSONString = pCore->createJSONString(outDObj); // Получаем строку из объекта
	pCore->setOutputJSONFile(JSONString); // Выводим в файл

	// Чистим память
	delete pCore;
	delete inpDObj;
	delete outDObj;

	return 0;
}