package com.ideasource.Controller;

import java.io.IOException;
import java.util.ArrayList;
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

import com.ideasource.Model.Collection;
import com.ideasource.Model.CollectionDTO;
import com.ideasource.Model.CollectionRepository;
import com.ideasource.Model.Expr;
import com.ideasource.Model.ExprRepository;
import com.ideasource.Model.Visit;
import com.ideasource.Model.VisitRepository;

@Controller
public class CollectionApiController {
	
	@Autowired
	private VisitRepository visitRepository;
	
	@Autowired
	private ExprRepository exprRepository;
	
	@Autowired
	private CollectionRepository collectionRepository;
	
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
		
		List<Collection> collections = collectionRepository.findAllByOwnerAndContentAndExprId(idString, content, exprId);
		if (collections.size() > 0) {
			Map<String, Object> response = new HashMap<String, Object>();
			response.put("status", 0);
			response.put("reason", "这样的收藏已经存在");
			response.put("response", null);
			System.err.println(response.toString());
			return response;
		}
		
		Collection collection = new Collection();
		collection.setContent(content);
		collection.setExprId(exprId);
		collection.setOwner(idString);
		collectionRepository.save(collection);
		Map<String, Object> response = new HashMap<String, Object>();
		response.put("status", 1);
		response.put("reason", null);
		response.put("response", collection);
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
		
		Collection collection = collectionRepository.findOne(collectionId);
		if (collection == null || !collection.getOwner().equals(idString)) {
			Map<String, Object> response = new HashMap<String, Object>();
			response.put("status", 0);
			response.put("reason", "该收藏不存在");
			response.put("response", null);
			return response;
		}
		
		collectionRepository.delete(collection);
		Map<String, Object> response = new HashMap<String, Object>();
		response.put("status", 1);
		response.put("reason", null);
		response.put("response", null);
		return response;
	}
	
	@RequestMapping(value = "api/{idString}/collections", method = RequestMethod.GET)
	public @ResponseBody List<CollectionDTO> getCollections(@PathVariable("idString") String idString, @PathVariable("onlyMine") Boolean onlyMine, @PathVariable("content") String content, HttpServletRequest request) {
		List<Collection> collections;
		if (onlyMine) {
			collections = collectionRepository.findAllByOwnerAndContent(idString, content);
		}
		else {
			collections = collectionRepository.findAllByContent(content);
		}
		List<Long> exprIds = new ArrayList<Long>();
		for (Collection collection : collections) {
			exprIds.add(collection.getExprId());
		}
		List<Expr> exprs = exprRepository.findAllByIdIn(exprIds);
		Map<Long, Expr> exprsMap = new HashMap<Long, Expr>();
		for (Expr expr : exprs) {
			expr.eraseDangerousInfo();
			exprsMap.put(expr.getId(), expr);
		}
		List<CollectionDTO> collectionDTOs = new ArrayList<CollectionDTO>();
		for (Collection collection : collections) {
			CollectionDTO collectionDTO = new CollectionDTO(collection);
			collectionDTO.setExpr(exprsMap.get(collection.getExprId()));;
			collectionDTOs.add(new CollectionDTO(collection));
		}
		return collectionDTOs;
	}
}
