public class CmpImage{
	static{
		System.load("/home/xixy10/MyTest/ThatExpr/ThatExpr/CmpImage/src/CmpImage.so");

	}

	public native float expWithExp(String srcExpPath, String dstExpPath);
//	public native String expWithFolder(String srcExpPath, String folderPath);
	public static void main(String[] args){
	CmpImage cmpimage = new CmpImage();

	float res = cmpimage.expWithExp("g.gif","g1.gif");
	System.out.println("load so over");
	System.out.println(res);

}
}
