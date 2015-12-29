package com.ideasource.Util.test;

import static org.junit.Assert.assertEquals;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

import java.io.File;

import org.junit.Test;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.multipart.MultipartFile;

import com.ideasource.Util.FileUtil;

public class FileUtilTest {
    
    FileUtil fileUtil;
    String exprFolder;
    
    @Value("${lcbq.expr.path}")
    public void setExprFolder(String path) {
        exprFolder = path;
    }
    
    @Test
    public void testGetExtention() {
        MultipartFile file = mock(MultipartFile.class);
        when(file.getContentType()).thenReturn(new String("image/gif"));
        assertEquals(fileUtil.getExtention(file), ".gif");
        when(file.getContentType()).thenReturn(new String("image/jpeg"));
        assertEquals(fileUtil.getExtention(file), ".jpg");
        when(file.getContentType()).thenReturn(new String("image/png"));
        assertEquals(fileUtil.getExtention(file), ".png");
        when(file.getContentType()).thenReturn(new String("nothing"));
        assertEquals(fileUtil.getExtention(file), ".err");
    }
    
    @Test
    public void testSaveExpr() {
    }
    
    @Test
    public void testLoadExpr() {
        File ret = fileUtil.loadExpr("1.jpg");
        assertEquals(ret.getPath(), exprFolder + "1.jpg");
    }
}
