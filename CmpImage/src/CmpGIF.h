#pragma once
#include "CmpStrategy.h"
class CmpGIF :
	public CmpStrategy
{
public:
	CmpGIF();
	~CmpGIF();
	virtual float getSimilarity(Expression* srcExp, Expression* dstExp);

	int decodeGIF(string strGifName, string strSavePath = "");
	int traverseFolder(const string strFilePath, string strImageNameSets[]);
};

