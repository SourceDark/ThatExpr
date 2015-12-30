#include "ExpressionComparer.h"
#include "CmpImage.h"
#include "string.h"
#include <string>
#include "stdlib.h"
using namespace std;


 jstring str2jstring(JNIEnv* env,const char* pat)
{
    //定义java String类 strClass
    jclass strClass = (env)->FindClass("Ljava/lang/String;");
    //获取String(byte[],String)的构造器,用于将本地byte[]数组转换为一个新String
    jmethodID ctorID = (env)->GetMethodID(strClass, "<init>","([BLjava/lang/String;)V");
    //建立byte数组
    jbyteArray bytes = (env)->NewByteArray(strlen(pat));
    //将char* 转换为byte数组
    (env)->SetByteArrayRegion(bytes,0, strlen(pat), (jbyte*)pat);
    // 设置String, 保存语言类型,用于byte数组转换至String时的参数
    jstring encoding = (env)->NewStringUTF("GB2312");
    //将byte数组转换为java String,并输出
    return (jstring)(env)->NewObject(strClass, ctorID, bytes, encoding);
}

string jstring2str(JNIEnv* env, jstring jstr)
{
    char*   rtn   =   NULL;
    jclass   clsstring   =   env->FindClass("java/lang/String");
    jstring   strencode   =   env->NewStringUTF("GB2312");
    jmethodID   mid   =   env->GetMethodID(clsstring,   "getBytes",  "(Ljava/lang/String;)[B");
    jbyteArray   barr=   (jbyteArray)env->CallObjectMethod(jstr,mid,strencode);
    jsize   alen   =   env->GetArrayLength(barr);
    jbyte*   ba   =   env->GetByteArrayElements(barr,JNI_FALSE);
    if(alen   >   0)
    {
        rtn   =   (char*)malloc(alen+1);
        memcpy(rtn,ba,alen);
        rtn[alen]=0;
    }
    env->ReleaseByteArrayElements(barr,ba,0);
    std::string stemp(rtn);
    free(rtn);
    return   stemp;
}

/*
 * Class:     CmpImage
 * Method:    expWithExp
 * Signature: (Ljava/lang/String;Ljava/lang/String;)F
 */
JNIEXPORT jfloat JNICALL Java_CmpImage_expWithExp
  (JNIEnv *env, jobject obj, jstring string1, jstring string2){
  const string a = jstring2str(env,string1);
  const string b = jstring2str(env,string2);
  return ExpressionComparer::expWithExp(a, b);


  }

/*
 * Class:     CmpImage
 * Method:    expWithFolder
 * Signature: (Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
 */
 /*
JNIEXPORT jstring JNICALL Java_CmpImage_expWithFolder
  (JNIEnv *env, jobject obj, jstring string1, jstring string2){
  const string a = jstring2str(env,string1);
  const string b = jstring2str(env,string2);
  const string c = ExpressionComparer::expWithFolder(a,b);
  return str2jstring(env,c.c_str());

  }
  */
