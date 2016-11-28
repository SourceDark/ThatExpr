#include "ExpressionComparer.h"
#include "Expression.h"

ExpressionComparer::ExpressionComparer()
{
}


ExpressionComparer::~ExpressionComparer()
{
}

float ExpressionComparer::expWithExp(const string& srcExpPath, const string& dstExpPath)
{
	Expression srcExp(srcExpPath);
	Expression dstExp(dstExpPath);
	return srcExp.compare(&dstExp);
}
#ifdef WINDOWS
string& ExpressionComparer::expWithFolder(const string& srcExpPath, const string& folderPath)
{
	string* ret = new string;
	float max = 0;

	string path(folderPath);
	int idx = path.length() - 1;
	if (path[idx] != '*')
	{
		return *ret;
	}
	path.erase(path.length() - 1, 1);

	_finddata_t file;
	long lf;

	if ((lf = _findfirst(folderPath.c_str(), &file)) == -1)
	{
		//cout << "没有文件" << endl;
	}
	else
	{
		//cout << "in " << folderPath << ":" << endl;
		while (_findnext(lf, &file) == 0)
		{
			string name(file.name);
			cout << name << endl;

			float result = expWithExp(srcExpPath, path + name);
			if (result >= 0 /*&& result > max*/)
			{
				cout << result << endl;
				max = result;
				*ret = (path + name);
			}
			else if (result == -1)
			{
				//cout << "Unknown type" << endl;
			}
			else if (result == -2)
			{
				//cout << "Invalid path" << endl;
			}
			else{
				//cout << "Unknown error" << endl;
			}
		}
		_findclose(lf);
	}

	return *ret;
}
#endif

