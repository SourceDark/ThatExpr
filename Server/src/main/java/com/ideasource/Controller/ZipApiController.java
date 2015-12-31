package com.ideasource.Controller;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import com.ideasource.Util.ZipUtil;

@Controller
public class ZipApiController {
    
	@Autowired
    private ZipUtil zipUtil;
    
    @RequestMapping(value = "api/{idString}/getZip", method = RequestMethod.POST)
    public @ResponseBody String getZip(@PathVariable("idString") String idString, @RequestBody String[] filenames) throws IOException {
        if (filenames.length < 1) return "error"; 
        return zipUtil.doZip(filenames, idString);
    }
    
    @RequestMapping(value = "api/{idString}/uploadZip", method = RequestMethod.POST)
    public @ResponseBody String uploadZip(@PathVariable("idString") String idString, @RequestParam MultipartFile[] btnFile, HttpServletRequest request, HttpServletResponse response) throws IOException {
    	int uploadSuccessed = 0;
    	try{
			//文件类型:btnFile[0].getContentType()
			//文件名称:btnFile[0].getName()
			if(btnFile[0].getSize()>Integer.MAX_VALUE){//文件长度
				return "file is too large";
			}
			InputStream is = btnFile[0].getInputStream();//多文件也适用,我这里就一个文件
			//String fileName = request.getParameter("fileName");
			byte[] b = new byte[(int)btnFile[0].getSize()];
			int read = 0;
			int i = 0;
			while((read=is.read())!=-1){
				b[i] = (byte) read;
				i++;
			}
			is.close();
			File zip = new File("D://test//zip//" + btnFile[0].getOriginalFilename());
			OutputStream os = new FileOutputStream(zip);
			os.write(b);
			os.flush();
			os.close();
			uploadSuccessed = zipUtil.unZip(zip, idString);
		}catch (Exception e) {
		}
        return "upload " + uploadSuccessed + " pics successfully";
    }
}
