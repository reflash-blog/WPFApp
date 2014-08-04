#pragma once
#include <string>

using namespace std;

extern "C"
{
	__declspec(dllexport) string solveProcess(string dObj);
}

/*Реализовать, подсчет строки, количество необходимой памяти, если надо подгрузить, загружать в буффер*/
/*
..CPP file:
#include <windows.h>

char m_Text[1000];

extern "C" __declspec(dllexport)
int AddString(const char* someStr)
{
strcpy(m_Text, someStr);
return 0;
}

extern "C" __declspec(dllexport)
int GetString(char* rntStr)
{
strcpy(rntStr, m_Text);
return 0;
}

..CS file:
using System.Text;
using System.Runtime.InteropServices;

class Program
{
[DllImport("CoolStringLibrary.dll")]
static extern int AddString(string someStr);

[DllImport("CoolStringLibrary.dll")]
static extern int GetString(StringBuilder rntStr);

static void Main(string[] args)
{
AddString("aaaa");
StringBuilder rntStr = new StringBuilder();
GetString(rntStr);
}
}
*/