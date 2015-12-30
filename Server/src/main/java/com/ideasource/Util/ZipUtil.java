package com.ideasource.Util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
@Component
public class ZipUtil {
    
    private static String zipFloder;
    private static String exprFolder;
    
    @Value("${lcbq.expr.path}")
    public void setExprFolder(String path) {
        exprFolder = path;
    }
    
    @Value("${lcbq.zip.path}")
    public void setZipFloder(String path) {
        zipFloder = path;
    }
        
    public static String doZip(String[] filenames, String userName) throws IOException {
        String zipName;
        zipName = userName + "_" + System.currentTimeMillis() + ".zip";
        System.out.println(zipName);
        File zipFile = new File(zipFloder + zipName);
        InputStream input = null;
        ZipOutputStream zipOut = new ZipOutputStream(new FileOutputStream(zipFile));
        zipOut.setComment(zipName);
        for (int i = 0; i < filenames.length; ++i) {
            try {
                System.out.println(exprFolder + filenames[i]);
                input = new FileInputStream(new File(exprFolder + filenames[i]));
                zipOut.putNextEntry(new ZipEntry(filenames[i]));
                int temp = 0;  
                while((temp = input.read()) != -1){  
                    zipOut.write(temp);  
                }  
                input.close();
            } catch (Exception e) {
                e.printStackTrace();
                return null;
            }
        }
        zipOut.close();
        return zipName;
    }
}
