#include <iostream>
//#include<string>
//#include "Expression.h"
//#include "io.h"
//#include "ximage.h"
#include "ExpressionComparer.h"
using namespace std;


int main(){
//    CxImage image;
//    image.Load(_T("1.jpg"), CXIMAGE_FORMAT_JPG);
//    int width = image.GetWidth();
//    int height = image.GetHeight();
//    image.Resample(width / 2, height / 2, 0, &image);
//	image.Save(_T("3.jpg"), CXIMAGE_FORMAT_JPG);

	/*CxImage a, b;
	a.Load(_T(" "), CXIMAGE_FORMAT_PNG);
	a.Save(_T("chaos.jpg"), CXIMAGE_FORMAT_JPG);
	b.Load(_T("screen.png"), CXIMAGE_FORMAT_PNG);

	a.getb
	BYTE* aBits = a.GetBits();
	BYTE* bBits = b.GetBits();*/
	//a.Save(_T("screen(1).jpg"), CXIMAGE_FORMAT_JPG);
	//a.Save(_T("screen(1).png"), CXIMAGE_FORMAT_PNG);
	//cout << expressionComparer::expWithFolder("13.jpg", "C:\\Users\\Yangxin\\Desktop\\test\\*") << endl;
	string a ="1.jpg";
	string b = "2.jpg";
	cout << ExpressionComparer::expWithExp("1.jpg", "2.jpg") << endl;
	cout<<"success!"<<endl;
//	getchar();
	return 0;
	/*char* input = "13.jpg";
	Expression b(input);

	const char *dir = "C:\\Users\\Yangxin\\Desktop\\test\\*";
	string path(dir);
	path.erase(path.length() - 1, 1);

	_finddata_t file;
	long lf;

	if ((lf = _findfirst(dir, &file)) == -1)
	{
		cout << "没有文件" << endl;
	}
	else
	{
		cout << "in " << dir << ":" << endl;
		while (_findnext(lf, &file) == 0)
		{
			string name(file.name);
			cout << name << endl;
			Expression a(path + name);
			if (a.isValid() == false)
			{
				continue;
			}
			Expression tmp(input);
			float result = tmp.compare(&a);
			if (result > 0)
			{
				cout << "相似度：" << result << endl;
			}
			else if (result == -1)
			{
				cout << "Unknown type" << endl;
			}
			else if (result == -2)
			{
				cout << "Invalid path" << endl;
			}
			else{
				cout << "Unknown error" << endl;
			}
		}
		_findclose(lf);
	}
	getchar();*/





	//getchar();
	/*CxImage image;
	string fileName = "g.gif";
	//image.Load(_T("13.jpg"), CXIMAGE_FORMAT_JPG);

	image.Load(fileName.c_str(), CXIMAGE_FORMAT_GIF);
	cout << image.GetNumFrames() << endl;

	if (image.IsValid())
	{
		image.GrayScale();


		int width = image.GetWidth();
		int height = image.GetHeight();
		//int iNumFrames = image.GetNumFrames();
		cout << image.GetBpp() << endl;
		BYTE * pDib;
		pDib = image.GetBits();
		cout << width << " " << height << endl;
		for (int i = 0; i < width; i++)
		{
			//pDib[i + width + 3] = 0;
			for (int j = 0; j < height; j++)
			{
				pDib[i + j * (width + 3)] = 0;
			}

		}



		//image.Resample(width / 2, height / 2, 0, &image);

		image.Save(_T("test.jpg"), CXIMAGE_FORMAT_JPG);
		//image.Save(_T("t.gif"), CXIMAGE_FORMAT_GIF);
	}
	cout << "Done!" << endl;
	getchar();*/
}
