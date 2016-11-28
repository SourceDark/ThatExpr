#pragma once

#include "ximage.h"
#include "CmpStrategy.h"
#include "string"
using namespace std;

class Expression
{
private:
	CxImage** images;
	int imgCnt;
	CmpStrategy* strategy;


public:
	Expression();
	Expression(const string& fileName);
	~Expression();
	bool isValid();
	float compare(Expression* iDst);
	string& compare(const string& path);
//	void toGif();

	CxImage** getImgs();
	int getImgCnt();
	int getType(string fileName);
	void setStrategy(CmpStrategy* m_strategy);

};

