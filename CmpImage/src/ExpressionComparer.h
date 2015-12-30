#pragma once


#include "string"
using namespace std;

class ExpressionComparer
{
public:
	ExpressionComparer();
	~ExpressionComparer();
	static float expWithExp(const string& srcExpPath, const string& dstExpPath);
	static string& expWithFolder(const string& srcExpPath, const string& folderPath);
};

