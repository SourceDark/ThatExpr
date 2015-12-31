package com.ideasource.Controller;

import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ideasource.Model.Collection;
import com.ideasource.Model.CollectionRepository;
import com.ideasource.Model.Visit;
import com.ideasource.Model.VisitRepository;

@Controller
public class TagApiController {

	@Autowired 
	private CollectionRepository collectionRepository;
	
	@Autowired
	private VisitRepository visitRepository;
	
	@RequestMapping(value = "api/{idString}/tags/my", method = RequestMethod.GET)
	public @ResponseBody Map<String, Integer> getMyCollections(@PathVariable("idString") String idString, HttpServletRequest request) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);
		
		List<Collection> list = collectionRepository.findAllByOwner(idString);
		Map<String, Integer> map = new HashMap<String, Integer>();
		for (Collection collection : list) {
			Integer value = map.get(collection.getContent());
			if (value == null) {
				map.put(collection.getContent(), 1);
			}
			else {
				map.put(collection.getContent(), value + 1);
			}
		}
		return map;
	}
	
	@RequestMapping(value = "api/{idString}/tags/all", method = RequestMethod.GET)
	public @ResponseBody Map<String, Integer> getAllCollections(@PathVariable("idString") String idString, HttpServletRequest request) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);
		
		List<Collection> list = collectionRepository.findAll();
		Map<String, Integer> map = new HashMap<String, Integer>();
		for (Collection collection : list) {
			Integer value = map.get(collection.getContent());
			if (value == null) {
				map.put(collection.getContent(), 1);
			}
			else {
				map.put(collection.getContent(), value + 1);
			}
		}
		return map;
	}
}
