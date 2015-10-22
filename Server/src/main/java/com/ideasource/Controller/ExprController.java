package com.ideasource.Controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller
public class ExprController {

	@RequestMapping(value = "/s/{idString}/upload", method = RequestMethod.GET)
	public String upload(@PathVariable("idString") String idString, Model model) {
		model.addAttribute("idString", idString);
		return "upload";
	}

	
}
