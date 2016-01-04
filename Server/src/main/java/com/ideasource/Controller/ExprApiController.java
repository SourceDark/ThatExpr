package com.ideasource.Controller;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import com.ideasource.Model.Collection;
import com.ideasource.Model.CollectionRepository;
import com.ideasource.Model.Expr;
import com.ideasource.Model.ExprRepository;
import com.ideasource.Model.Visit;
import com.ideasource.Model.VisitRepository;
import com.ideasource.Util.FileUtil;
import com.ideasource.Util.SearchUtil;
import com.ideasource.Util.StringUtil;

@Controller
public class ExprApiController {

	@Autowired
	private ExprRepository exprRepository;

	@Autowired
	private CollectionRepository collectionRepository;

	@Autowired
	private VisitRepository visitRepository;

	@Autowired
	private SearchUtil searchUtil;

	@RequestMapping(value = "api/{idString}/exprs/all", method = RequestMethod.GET)
	public @ResponseBody List<Expr> getAllExprsBycollection(@PathVariable("idString") String idString,
			@RequestParam(value = "tag") String content, HttpServletRequest request) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);

		List<Collection> collections = collectionRepository.findAllByContent(content);
		List<Long> exprIds = new ArrayList<Long>();
		for (Collection collection : collections) {
			exprIds.add(collection.getExprId());
		}
		List<Expr> exprs = exprRepository.findAllByIdIn(exprIds);
		for (Expr expr : exprs) {
			expr.eraseDangerousInfo();
		}
		return exprs;
	}

	@RequestMapping(value = "api/{idString}/exprs/my", method = RequestMethod.GET)
	public @ResponseBody List<Expr> getMyExprsBycollection(@PathVariable("idString") String idString,
			@RequestParam(value = "tag") String content, HttpServletRequest request) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);

		List<Collection> collections = collectionRepository.findAllByOwnerAndContent(idString, content);
		List<Long> exprIds = new ArrayList<Long>();
		for (Collection collection : collections) {
			exprIds.add(collection.getExprId());
		}
		List<Expr> exprs = exprRepository.findAllByIdIn(exprIds);
		for (Expr expr : exprs) {
			expr.eraseDangerousInfo();
		}
		return exprs;
	}

	@RequestMapping(value = "api/{idString}/expr/search", method = RequestMethod.POST)
	public @ResponseBody List<Expr> searchExpr(@PathVariable("idString") String idString,
			@RequestParam("expr") MultipartFile exprFile, @RequestParam("tag") String content,
			HttpServletRequest request) throws Exception {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);

		String md5 = StringUtil.MD5(exprFile.getBytes());
		List<Expr> exprs = exprRepository.findAllByMd5(md5);

		if (exprs.isEmpty()) {
			String extension = FileUtil.getExtention(exprFile);
			if (FileUtil.saveExpr(md5 + extension, exprFile.getBytes())) {
				Expr expr = new Expr();
				expr.setMd5(md5);
				expr.setCreator(idString);
				expr.setExtension(extension);
				exprRepository.save(expr);
				Collection collection = new Collection();
				collection.setExprId(expr.getId());
				collection.setOwner(idString);
				collection.setContent("");
				collectionRepository.save(collection);
				if (content.length() > 0) {
					collection = new Collection();
					collection.setExprId(expr.getId());
					collection.setOwner(idString);
					collection.setContent(content);
					collectionRepository.save(collection);
				}
				
			} else {
			}
		} else {
			List<Collection> collections = collectionRepository.findAllByOwnerAndContentAndExprId(idString, "",
					exprs.get(0).getId());
			if (collections.isEmpty()) {
				Collection collection = new Collection();
				collection.setExprId(exprs.get(0).getId());
				collection.setOwner(idString);
				collection.setContent("");
				collectionRepository.save(collection);
			}
			collections = collectionRepository.findAllByOwnerAndContentAndExprId(idString, content,
					exprs.get(0).getId());
			if (collections.isEmpty()) {
				Collection collection = new Collection();
				collection.setExprId(exprs.get(0).getId());
				collection.setOwner(idString);
				collection.setContent(content);
				collectionRepository.save(collection);
			}
		}
		return searchUtil.expWithFolder(md5);
	}

	@RequestMapping(value = "api/{idString}/expr/new", method = RequestMethod.POST)
	public @ResponseBody Expr createExpr(@PathVariable("idString") String idString,
			@RequestParam("expr") MultipartFile exprFile, @RequestParam("tag") String content,
			HttpServletRequest request) throws IOException {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl(request.getRequestURL().toString());
		visit.setClientIp(request.getRemoteAddr());
		visitRepository.save(visit);

		String md5 = StringUtil.MD5(exprFile.getBytes());
		List<Expr> exprs = exprRepository.findAllByMd5(md5);
		if (exprs.isEmpty()) {
			String extension = FileUtil.getExtention(exprFile);
			if (FileUtil.saveExpr(md5 + extension, exprFile.getBytes())) {
				Expr expr = new Expr();
				expr.setMd5(md5);
				expr.setCreator(idString);
				expr.setExtension(extension);
				exprRepository.save(expr);
				Collection collection = new Collection();
				collection.setExprId(expr.getId());
				collection.setOwner(idString);
				collection.setContent("");
				collectionRepository.save(collection);
				if (content.length() > 0) {
					collection = new Collection();
					collection.setExprId(expr.getId());
					collection.setOwner(idString);
					collection.setContent(content);
					collectionRepository.save(collection);
				}
				return expr.eraseDangerousInfo();
			} else {
				return null;
			}
		} else {
			List<Collection> collections = collectionRepository.findAllByOwnerAndContentAndExprId(idString, "",
					exprs.get(0).getId());
			if (collections.isEmpty()) {
				Collection collection = new Collection();
				collection.setExprId(exprs.get(0).getId());
				collection.setOwner(idString);
				collection.setContent("");
				collectionRepository.save(collection);
			}
			collections = collectionRepository.findAllByOwnerAndContentAndExprId(idString, content,
					exprs.get(0).getId());
			if (collections.isEmpty()) {
				Collection collection = new Collection();
				collection.setExprId(exprs.get(0).getId());
				collection.setOwner(idString);
				collection.setContent(content);
				collectionRepository.save(collection);
			}
			return exprs.get(0).eraseDangerousInfo();
		}
	}
}
