#include "Expression.h"
#include "CmpGIF.h"
#include "CmpPic.h"


Expression::Expression()
{
	images = new CxImage*();
	imgCnt = 0;
	strategy = NULL;
}

Expression::Expression(const string& fileName)
{
	images = new CxImage*();
	imgCnt = 0;
	strategy = NULL;
	int type = getType(fileName);

	switch (type)
	{
	case CXIMAGE_FORMAT_JPG:
	case CXIMAGE_FORMAT_PNG:
		setStrategy(new CmpPic());
		break;
	case CXIMAGE_FORMAT_GIF:
		setStrategy(new CmpGIF());
		break;
	default:
		type = CXIMAGE_FORMAT_UNKNOWN;
		break;
	}

	CxImage* img = new CxImage(fileName.c_str(), type);
	if (img->IsValid() == false || type == CXIMAGE_FORMAT_UNKNOWN)
	{
		imgCnt = 0;
		return;
	}

	if (type == CXIMAGE_FORMAT_GIF)
	{
		imgCnt = img->GetNumFrames();
		images = new CxImage*[imgCnt]();
		for (int i = 0; i < imgCnt; i++)
		{
			images[i] = new CxImage();
			images[i]->SetFrame(i);
			images[i]->Load(fileName.c_str(), type);
		}
	}
	else
	{
		*images = img;
		imgCnt = 1;
	}
}


Expression::~Expression()
{
}

CxImage** Expression::getImgs()
{
	return images;
}


int Expression::getImgCnt()
{
	return imgCnt;
}

int Expression::getType(string fileName)
{
	int idx = fileName.find_last_of(".") + 1;
	string ext = fileName.substr(idx, fileName.length() - idx);
	int type = 0;
	if (ext == "bmp")     type = CXIMAGE_FORMAT_BMP;
#if CXIMAGE_SUPPORT_JPG
	else if (ext == "jpg" || ext == "JPG" || ext == "JPEG" || ext == "jpeg") type = CXIMAGE_FORMAT_JPG;
#endif
#if CXIMAGE_SUPPORT_GIF
	else if (ext == "gif")    type = CXIMAGE_FORMAT_GIF;
#endif
#if CXIMAGE_SUPPORT_PNG
	else if (ext == "png" || ext == "PNG")    type = CXIMAGE_FORMAT_PNG;
#endif
#if CXIMAGE_SUPPORT_MNG
	else if (ext == "mng" || ext == "jng") type = CXIMAGE_FORMAT_MNG;
#endif
#if CXIMAGE_SUPPORT_ICO
	else if (ext == "ico")    type = CXIMAGE_FORMAT_ICO;
#endif
#if CXIMAGE_SUPPORT_TIF
	else if (ext == "tiff" || ext == "tif") type = CXIMAGE_FORMAT_TIF;
#endif
#if CXIMAGE_SUPPORT_TGA
	else if (ext == "tga")    type = CXIMAGE_FORMAT_TGA;
#endif
#if CXIMAGE_SUPPORT_PCX
	else if (ext == "pcx")    type = CXIMAGE_FORMAT_PCX;
#endif
#if CXIMAGE_SUPPORT_WBMP
	else if (ext == "wbmp")    type = CXIMAGE_FORMAT_WBMP;
#endif
#if CXIMAGE_SUPPORT_WMF
	else if (ext == "wmf" || ext == "emf") type = CXIMAGE_FORMAT_WMF;
#endif
#if CXIMAGE_SUPPORT_J2K
	else if (ext == "j2k" || ext == "jp2") type = CXIMAGE_FORMAT_J2K;
#endif
#if CXIMAGE_SUPPORT_JBG
	else if (ext == "jbg")    type = CXIMAGE_FORMAT_JBG;
#endif
#if CXIMAGE_SUPPORT_JP2
	else if (ext == "jp2" || ext == "j2k") type = CXIMAGE_FORMAT_JP2;
#endif
#if CXIMAGE_SUPPORT_JPC
	else if (ext == "jpc" || ext == "j2c") type = CXIMAGE_FORMAT_JPC;
#endif
#if CXIMAGE_SUPPORT_PGX
	else if (ext == "pgx")    type = CXIMAGE_FORMAT_PGX;
#endif
#if CXIMAGE_SUPPORT_RAS
	else if (ext == "ras")    type = CXIMAGE_FORMAT_RAS;
#endif
#if CXIMAGE_SUPPORT_PNM
	else if (ext == "pnm" || ext == "pgm" || ext == "ppm") type = CXIMAGE_FORMAT_PNM;
#endif
	else type = CXIMAGE_FORMAT_UNKNOWN;

	return type;
}


void Expression::setStrategy(CmpStrategy* m_strategy)
{
	if (strategy != NULL)
	{
		delete strategy;
	}
	strategy = m_strategy;
}


float Expression::compare(Expression* iDst)
{
	return strategy->getSimilarity(this, iDst);
}

bool Expression::isValid()
{
	if (imgCnt > 0)
	{
		return true;
	}
	return false;
}
#ifdef GIFWINDOWS
void Expression::toGif()
{
	CxIOFile hFile;
	hFile.Open("t.gif", "wb");

	CxImageGIF multiimage;

	int h = images[0]->GetHeight() / 2;
	int w = images[0]->GetWidth() / 2;
	for (int i = 0; i < imgCnt; i++)
	{
		images[i]->Resample(w, h, 0, images[i]);
	}

	multiimage.SetLoops(0);
	multiimage.SetDisposalMethod(2);
	multiimage.Encode(&hFile, images, imgCnt, false, false);

	hFile.Close();
}


#endif
