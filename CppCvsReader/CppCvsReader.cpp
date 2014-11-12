// CppCsvReader.cpp : Defines the entry point for the console application.
//

#include <Windows.h>
#include "stdafx.h"
#include <fstream>
#include <string>
#include <iostream>
#include <direct.h>
#include <ctime>

/// this program reads a text file line by line to determine the best case
/// time required to read a file without actually processing it
int _tmain(int argc, _TCHAR* argv[])
{
	using namespace std;
	string filePath = "..\\TestData\\";
	//string fileName = "simple5.csv";
	string fileName = "test1.csv";
	string File = filePath + fileName;
	ifstream file;
	string str;
	const int BUF_SIZE = 2048;
	char line[BUF_SIZE];
	long cbFile = 0;

	cout << "Using C++ ifstream reader\n";
	cout << "File: " + fileName + "\n";
	cout << "Starting\n";

	file.open(File);
	if (!file.is_open())
	{ // might not be executing from IDE
		// try again from one level higher
		File = "..\\" + File;
		file.open(File);
	}
	if (file.is_open())
	{
		clock_t tStart = clock();
		while (true)
		{
			file.getline(line, BUF_SIZE);
			int cbLine = strlen(line);
			if (cbLine == 0)
				break;
			cbFile += cbLine;
		}
		clock_t tDone = clock();
		cout << "Done\n";
		cout << "Bytes Read: " + to_string(cbFile) + "\n";
		cout << "ET:" + to_string((tDone - tStart) / CLOCKS_PER_SEC) + " seconds";

	}
	else
	{
		cout << "File Open failed";
	}
	char foo;
	cin >> foo;
	return 0;
}


