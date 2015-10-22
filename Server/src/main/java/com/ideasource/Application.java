package com.ideasource;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;

@SpringBootApplication
@Controller
public class Application {
	
	@RequestMapping("/")
    public String index() {
        return "index";
    }
	
	@RequestMapping("/s/{idString}/discover")
	public String discover(@PathVariable("idString") String idString, Model model) {
		model.addAttribute("idString", idString);
		return "discover";
	}
	
	public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }
	
}
