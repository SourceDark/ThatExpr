package com.ideasource.Util;

import java.io.BufferedInputStream;

import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;
import java.util.zip.ZipOutputStream;

import org.apache.tools.zip.ZipFile;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;

import com.ideasource.Model.Expr;
import com.ideasource.Model.ExprRepository;
import com.ideasource.Model.Collection;
import com.ideasource.Model.CollectionRepository;

import com.github.junrar.Archive;
import com.github.junrar.rarfile.FileHeader;
 
@Component
public class ZipUtil {

	@Autowired
	private ExprRepository exprRepository;

	@Autowired
	private CollectionRepository collectionRepository;
    
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
        
    private static byte[] getBytesFromFile(File f) {
        if (f == null) {
            return null;
        }
        try {
            FileInputStream stream = new FileInputStream(f);
            ByteArrayOutputStream out = new ByteArrayOutputStream(1000);
            byte[] b = new byte[1000];
            for (int n;(n = stream.read(b)) != -1;) {
				out.write(b, 0, n);
			}
            stream.close();
            out.close();
            return out.toByteArray();
        } catch (IOException e) {
        }
        return null;
    }
    
    public static String getExtensionName(String filename) {   
        if ((filename != null) && (filename.length() > 0)) {   
            int dot = filename.lastIndexOf('.');   
            if ((dot >-1) && (dot < (filename.length() - 1))) {   
                return filename.substring(dot);   
            }   
        }   
        return filename;   
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
    
    public int unZip(File zipFile, String userName) throws IOException {
        System.out.println(zipFile.getName());
    	List<String> filenames;
    	filenames = new ArrayList<String>();
    	int cnt = 0;
    	try {  
    		if (zipFile.getName().toLowerCase().endsWith(".rar")) {
    			Archive a = null;
    	        try {
    	            a = new Archive(zipFile);
    	            if (a != null) {
    	                a.getMainHeader().print(); // 打印文件信息.
    	                FileHeader fh = a.nextFileHeader();
    	                while (fh != null) {
    	                    if (fh.isDirectory()) { // 文件夹 
    	                        File fol = new File(exprFolder + fh.getFileNameString());
    	                        fol.mkdirs();
    	                    } else { // 文件
    	                        File out = new File(exprFolder + fh.getFileNameString().trim());
    	                        filenames.add(fh.getFileNameString());
    	                        //System.out.println(out.getAbsolutePath());
    	                        try {// 之所以这么写try，是因为万一这里面有了异常，不影响继续解压. 
    	                            if (!out.exists()) {
    	                                if (!out.getParentFile().exists()) {// 相对路径可能多级，可能需要创建父目录. 
    	                                    out.getParentFile().mkdirs();
    	                                }
    	                                out.createNewFile();
    	                            }
    	                            FileOutputStream os = new FileOutputStream(out);
    	                            a.extractFile(fh, os);
    	                            os.close();
    	                        } catch (Exception ex) {
    	                            ex.printStackTrace();
    	                        }
    	                    }
    	                    fh = a.nextFileHeader();
    	                }
    	                a.close();
    	            }
    	        } catch (Exception e) {
    	            e.printStackTrace();
    	        }
            }	else if (zipFile.getName().toLowerCase().endsWith(".zip")) {
	    		File pathFile = new File(exprFolder);  
	            if(!pathFile.exists()){  
	                pathFile.mkdirs();  
	            }  
	            ZipFile zip = new ZipFile(zipFile);  
	            for(Enumeration entries = zip.getEntries();entries.hasMoreElements();){  
	                ZipEntry entry = (ZipEntry)entries.nextElement();  
	                String zipEntryName = entry.getName();  
	                filenames.add(entry.getName());
	                InputStream in = zip.getInputStream((org.apache.tools.zip.ZipEntry) entry);  
	                String outPath = (exprFolder+zipEntryName).replaceAll("\\*", "/");;  
	                //判断路径是否存在,不存在则创建文件路径  
	                File file = new File(outPath.substring(0, outPath.lastIndexOf('/')));  
	                if(!file.exists()){  
	                    file.mkdirs();  
	                }  
	                //判断文件全路径是否为文件夹,如果是上面已经上传,不需要解压  
	                if(new File(outPath).isDirectory()){  
	                    continue;  
	                }  
	                //输出文件路径信息  
	                System.out.println(outPath);  
	                  
	                OutputStream out = new FileOutputStream(outPath);  
	                byte[] buf1 = new byte[1024];  
	                int len;  
	                while((len=in.read(buf1))>0){  
	                    out.write(buf1,0,len);  
	                }  
	                in.close();  
	                out.close();
	           	}  
            }	else {
            	return -1;
            }
        	System.out.println("******************解压完毕********************");  
        	
        	// save expr
        	for (String filename:filenames) {
        		File file = new File(exprFolder + filename);
        		String md5 = StringUtil.MD5(getBytesFromFile(file));
    			String extension = getExtensionName(file.getName());
    			File newFile = new File(exprFolder + md5 + extension);
    			if (newFile.exists()) {
    				System.out.println("The file is existed");
    				continue;
    			}
    			++cnt;
    			file.renameTo(newFile);
            	System.out.println(md5);
            	System.out.println(extension);
            	System.out.println(userName);
            	
				Expr expr = new Expr();
				expr.setMd5(md5);
				expr.setCreator(userName);
				expr.setExtension(extension);
				System.out.println("save " + md5 + extension);
				exprRepository.save(expr);
				System.out.println("saved " + md5 + extension);
				Collection collection = new Collection();
				collection.setExprId(expr.getId());
				collection.setOwner(userName);
				collection.setContent("");
				collectionRepository.save(collection);
				
        	}
        } catch (IOException e) {  
            // TODO Auto-generated catch block  
            e.printStackTrace();  
        }  
		return cnt;
    }
}