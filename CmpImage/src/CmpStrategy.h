#pragma once
#include "ximage.h"
#include "string"
using namespace std;

class Expression;
class CmpStrategy
{
public:
	CmpStrategy();
	~CmpStrategy();
	virtual float getSimilarity(Expression* srcExp, Expression* dstExp);
};

