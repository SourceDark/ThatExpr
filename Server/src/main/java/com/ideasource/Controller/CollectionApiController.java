package com.ideasource.Controller;

import java.io.IOException;
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
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ideasource.Model.Expr;
import com.ideasource.Model.ExprRepository;
import com.ideasource.Model.Tag;
import com.ideasource.Model.TagRepository;
import com.ideasource.Model.Visit;
import com.ideasource.Model.VisitRepository;

@Controller
public class CollectionApiController {
	
	@Autowired
	private TagRepository tagRepository;
	
	@Autowired
	private VisitRepository visitRepository;
	
	@Autowired
	private ExprRepository exprRepository;
	
	@RequestMapping(value = "api/{idString}/collections", method = RequestMethod.POST)
	public @ResponseBody Map<String, Object> createCollection(@PathVariable("idString") String idString, @RequestParam("content") String content, @RequestParam("exprId") Long exprId, HttpServletRequest request)
			throws IOException {

		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);
		
		Expr expr = exprRepository.findOne(exprId);
		if (expr == null) {
			Map<String, Object> response = new HashMap<String, Object>();
			response.put("status", 0);
			response.put("reason", "想要收藏的表情不存在");
			response.put("response", null);
			System.err.println(response.toString());
			return response;
		}
		
		List<Tag> tags = tagRepository.findAllByOwnerAndContentAndExprId(idString, content, exprId);
		if (tags.size() > 0) {
			Map<String, Object> response = new HashMap<String, Object>();
			response.put("status", 0);
			response.put("reason", "这样的收藏已经存在");
			response.put("response", null);
			System.err.println(response.toString());
			return response;
		}
		
		Tag tag = new Tag();
		tag.setContent(content);
		tag.setExprId(exprId);
		tag.setOwner(idString);
		tagRepository.save(tag);
		Map<String, Object> response = new HashMap<String, Object>();
		response.put("status", 1);
		response.put("reason", null);
		response.put("response", tag);
		return response;
	}
	
	@RequestMapping(value = "api/{idString}/collections/{collectionId}", method = RequestMethod.DELETE)
	public @ResponseBody Map<String, Object> removeCollection(@PathVariable("idString") String idString, @PathVariable("collectionId") Long collectionId, HttpServletRequest request)
			throws IOException {

		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);
		
		Tag tag = tagRepository.findOne(collectionId);
		if (tag == null || !tag.getOwner().equals(idString)) {
			Map<String, Object> response = new HashMap<String, Object>();
			response.put("status", 0);
			response.put("reason", "该收藏不存在");
			response.put("response", null);
			return response;
		}
		
		tagRepository.delete(tag);
		Map<String, Object> response = new HashMap<String, Object>();
		response.put("status", 1);
		response.put("reason", null);
		response.put("response", null);
		return response;
	}
}
