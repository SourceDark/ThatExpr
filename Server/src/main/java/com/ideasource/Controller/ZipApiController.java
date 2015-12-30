package com.ideasource.Controller;

import java.io.IOException;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ideasource.Util.ZipUtil;

@Controller
public class ZipApiController {
    
    private ZipUtil zipUtil;
    
    @RequestMapping(value = "api/{idString}/getZip", method = RequestMethod.POST)
    public @ResponseBody String getZip(@PathVariable("idString") String idString, @RequestBody String[] filenames) throws IOException {
        if (filenames.length < 1) return "error"; 
        return zipUtil.doZip(filenames, idString);
    }
}
