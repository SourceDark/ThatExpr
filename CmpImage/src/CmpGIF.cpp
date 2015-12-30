#include "CmpGIF.h"
#include "CmpPic.h"
#include "Expression.h"


CmpGIF::CmpGIF()
{
}


CmpGIF::~CmpGIF()
{
}

float CmpGIF::getSimilarity(Expression* srcExp, Expression* dstExp)
{
    int srcCnt = srcExp->getImgCnt();
    int dstCnt = dstExp->getImgCnt();
	if (srcCnt != dstCnt || srcCnt <= 0 || dstCnt <= 0)
	{
		return (float)0;
	}
	CxImage** srcImgs;
	CxImage** dstImgs;
	srcImgs = srcExp->getImgs();
	dstImgs = dstExp->getImgs();
	int imgCnt = srcExp->getImgCnt();
	float ratio = 0;
	CmpPic cmpPic;
	for (int i = 0; i < imgCnt; i++)
	{
		ratio += cmpPic.getSimilarity(srcImgs[i], dstImgs[i]);
	}
	return ratio / imgCnt;
}

int CmpGIF::decodeGIF(string strGifName, string strSavePath)
{
	CxImage img;

	img.Load(strGifName.c_str(), CXIMAGE_FORMAT_GIF);

	int iNumFrames = img.GetNumFrames();

	int idxSlash = strGifName.find_last_not_of('/') + 1;
	int idxPoint = strGifName.find_last_not_of('.') + 1;
	string gifName = strGifName.substr(idxSlash, idxSlash - idxPoint);

	CxImage* newImage = new CxImage();

	for (int i = 0; i < iNumFrames; i++) {
		newImage->SetFrame(i);
		newImage->Load(strGifName.c_str(), CXIMAGE_FORMAT_GIF);

		char tmp[64];
		sprintf(tmp, "%d", i);

		string tmp1;
		tmp1 = tmp1.insert(0, tmp);

		tmp1 = gifName + tmp1 + ".png";

		newImage->Save(tmp1.c_str(), CXIMAGE_FORMAT_PNG);
	}

	if (newImage) delete newImage;

	return iNumFrames;
}
