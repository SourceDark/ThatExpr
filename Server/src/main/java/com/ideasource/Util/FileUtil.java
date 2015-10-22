package com.ideasource.Util;

import java.io.File;

import java.io.FileOutputStream;
import java.io.IOException;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

@Component
public class FileUtil {
	
	private static String exprFolder;
	
	@Value("${lcbq.expr.path}")
	public void setExprFolder(String path) {
		exprFolder = path;
	}
	
	public static String getExtention(MultipartFile file) {
		String type = file.getContentType();
		if (type.equals("image/gif")) {
			return ".gif";
		}
		if (type.equals("image/jpeg")) {
			return ".jpg";
		}
		if (type.equals("image/png")) {
			return ".png";
		}
		System.err.println(type);
		return ".err";
	}
	
	private static File load(String path) {
		return new File(path);
	}
	
	public static Boolean saveExpr(String filename, byte[] data) {
		System.out.println(exprFolder + filename);
		File file = new File(exprFolder + filename);
		if (!file.exists()) {
			try {
				file.createNewFile();
			} catch (IOException e) {
				System.err.println("Failed to create file" + e.toString());
				return false;
			}
		}
		FileOutputStream fos = null;
		try {
			fos = new FileOutputStream(file);
			fos.write(data);
			fos.flush();
		} catch (IOException e) {
			System.err.println("Failed to write data");
			return false;
		} finally {
			if (fos != null) {
				try {
					fos.close();
				} catch (IOException e) {
					System.err.println("Failed to close FileOutputStream");
					return false;
				}
			}
		}
		return true;
	}
	
	public static File loadExpr(String filename) {
		return load(exprFolder + filename);
	}
}
