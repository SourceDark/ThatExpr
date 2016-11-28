#pragma once
#include "CmpStrategy.h"
class CmpPic :
	public CmpStrategy
{
public:
	CmpPic();
	~CmpPic();
	virtual float getSimilarity(Expression* srcExp, Expression* dstExp);
	float getSimilarity(CxImage* srcImg, CxImage* dstImg);
};

