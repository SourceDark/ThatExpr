#include "CmpPic.h"
#include "Expression.h"


CmpPic::CmpPic()
{
}


CmpPic::~CmpPic()
{
}

float CmpPic::getSimilarity(Expression* srcExp, Expression* dstExp)
{
	CxImage* srcImgs;
	CxImage* dstImgs;
	srcImgs = *(srcExp->getImgs());
	dstImgs = *(dstExp->getImgs());
	return getSimilarity(srcImgs, dstImgs);
}

float CmpPic::getSimilarity(CxImage* srcImg, CxImage* dstImg)
{
	if (srcImg == NULL || dstImg == NULL)
	{
		return -2;
	}
	if (srcImg->IsValid() == false || dstImg->IsValid() == false)
	{
		return -2;
	}


	srcImg->GrayScale();
	dstImg->GrayScale();

	int srcWidth = srcImg->GetWidth();
	int srcHeight = srcImg->GetHeight();
	int dstWidth = dstImg->GetWidth();
	int dstHeight = dstImg->GetHeight();
	int width, height;

	if ((srcWidth != dstWidth) || (srcHeight != dstHeight))
	{
		if (srcWidth * srcHeight > dstWidth * dstHeight)
		{
			srcImg->Resample(dstWidth, dstHeight, 0, srcImg);
			width = dstWidth;
			height = dstHeight;
		}
		else
		{
			dstImg->Resample(srcWidth, srcHeight, 0, dstImg);
			width = srcWidth;
			height = srcHeight;
		}
	}
	else{
		width = srcWidth;
		height = srcHeight;
	}
	int padding = 0;
	if(width % 4)
		padding = 4 - width % 4;

	BYTE* srcBits = srcImg->GetBits();
	BYTE* dstBits = dstImg->GetBits();

	long int srcHistogram[256] = { 0 };
	long int dstHistogram[256] = { 0 };
	long int idx = 0;

	for (int row = 0; row < height; row++)
	{
		for (int col = 0; col < width; col++)
		{
			srcHistogram[srcBits[idx]]++;
			dstHistogram[dstBits[idx]]++;
			idx++;
		}
		idx += padding;
	}

	float similar = 0;
	int sumS = 0;
	int sumD = 0;
	for (int i = 0; i < 256; i++)
	{

		int delta = abs(srcHistogram[i] - dstHistogram[i]);
		long int threshold = width * height / 400;
		if (delta < threshold)
		{
			similar++;
		}
	}
	return similar / 256;
}

