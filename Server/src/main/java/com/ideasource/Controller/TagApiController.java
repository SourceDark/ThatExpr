package com.ideasource.Controller;

import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ideasource.Model.Tag;
import com.ideasource.Model.TagRepository;
import com.ideasource.Model.Visit;
import com.ideasource.Model.VisitRepository;

@Controller
public class TagApiController {

	@Autowired 
	private TagRepository tagRepository;
	
	@Autowired
	private VisitRepository visitRepository;
	
	@RequestMapping(value = "api/{idString}/tags/my", method = RequestMethod.GET)
	public @ResponseBody Map<String, Integer> getMyTags(@PathVariable("idString") String idString) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl("api/{idString}/tags/my");
		visitRepository.save(visit);
		
		List<Tag> list = tagRepository.findAllByOwner(idString);
		Map<String, Integer> map = new HashMap<String, Integer>();
		for (Tag tag : list) {
			Integer value = map.get(tag.getContent());
			if (value == null) {
				map.put(tag.getContent(), 1);
			}
			else {
				map.put(tag.getContent(), value + 1);
			}
		}
		return map;
	}
	
	@RequestMapping(value = "api/{idString}/tags/all", method = RequestMethod.GET)
	public @ResponseBody Map<String, Integer> getAllTags(@PathVariable("idString") String idString) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl("api/{idString}/tags/all");
		visitRepository.save(visit);
		
		List<Tag> list = tagRepository.findAll();
		Map<String, Integer> map = new HashMap<String, Integer>();
		for (Tag tag : list) {
			Integer value = map.get(tag.getContent());
			if (value == null) {
				map.put(tag.getContent(), 1);
			}
			else {
				map.put(tag.getContent(), value + 1);
			}
		}
		return map;
	}
}
