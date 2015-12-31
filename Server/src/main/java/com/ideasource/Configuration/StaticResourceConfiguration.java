package com.ideasource.Configuration;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.autoconfigure.AutoConfigureAfter;
import org.springframework.boot.autoconfigure.web.DispatcherServletAutoConfiguration;
import org.springframework.boot.autoconfigure.web.WebMvcAutoConfiguration;
import org.springframework.context.annotation.Configuration;
import org.springframework.stereotype.Component;
import org.springframework.web.servlet.config.annotation.ResourceHandlerRegistry;

@Component
@Configuration
@AutoConfigureAfter(DispatcherServletAutoConfiguration.class)
public class StaticResourceConfiguration extends WebMvcAutoConfiguration.WebMvcAutoConfigurationAdapter {

	static private String myExternalFilePath;
    static private String myExternalZipPath;
	
	@Value("${lcbq.expr.path}")
	public void setMyExternalFilePath(String path) {
		myExternalFilePath = "file:///" + path;
	}
	
	@Value("${lcbq.zip.path}")
    public void setMyExternalZipPath(String path) {
	    myExternalZipPath = "file:///" + path;
    }
	
	@Override
	public void addResourceHandlers(ResourceHandlerRegistry registry) {
		registry.addResourceHandler("expr/**").addResourceLocations(myExternalFilePath);
        registry.addResourceHandler("zip/**").addResourceLocations(myExternalZipPath);
		super.addResourceHandlers(registry);
	}

}