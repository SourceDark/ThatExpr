package com.ideasource.Controller;

import java.io.IOException;
import java.util.Date;
import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import com.ideasource.Model.Expr;
import com.ideasource.Model.ExprRepository;
import com.ideasource.Model.Tag;
import com.ideasource.Model.TagRepository;
import com.ideasource.Model.Visit;
import com.ideasource.Model.VisitRepository;
import com.ideasource.Util.FileUtil;
import com.ideasource.Util.StringUtil;

@Controller
public class ExprApiController {

	@Autowired
	private ExprRepository exprRepository;

	@Autowired
	private TagRepository tagRepository;
	
	@Autowired
	private VisitRepository visitRepository;

	@RequestMapping(value = "api/{idString}/exprs/all", method = RequestMethod.GET)
	public @ResponseBody List<Expr> getAllExprsByTag(@PathVariable("idString") String idString,
			@RequestParam(value = "tag") String content) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl("api/{idString}/exprs/all");
		visitRepository.save(visit);
		
		List<Tag> tags = tagRepository.findAllByContent(content);
		List<Long> exprIds = new ArrayList<Long>();
		for (Tag tag : tags) {
			exprIds.add(tag.getExprId());
		}
		List<Expr> exprs = exprRepository.findAllByIdIn(exprIds);
		for (Expr expr : exprs) {
			expr.eraseDangerousInfo();
		}
		return exprs;
	}
	
	@RequestMapping(value = "api/{idString}/exprs/my", method = RequestMethod.GET)
	public @ResponseBody List<Expr> getMyExprsByTag(@PathVariable("idString") String idString,
			@RequestParam(value = "tag") String content) {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl("api/{idString}/exprs/my");
		visitRepository.save(visit);
			
		List<Tag> tags = tagRepository.findAllByOwnerAndContent(idString, content);
		List<Long> exprIds = new ArrayList<Long>();
		for (Tag tag : tags) {
			exprIds.add(tag.getExprId());
		}
		List<Expr> exprs = exprRepository.findAllByIdIn(exprIds);
		for (Expr expr : exprs) {
			expr.eraseDangerousInfo();
		}
		return exprs;
	}

	@RequestMapping(value = "api/{idString}/expr/new", method = RequestMethod.POST)
	public @ResponseBody Expr createExpr(@PathVariable("idString") String idString, @RequestParam("expr") MultipartFile exprFile, @RequestParam("tag") String content)
			throws IOException {
		Visit visit = new Visit();
		visit.setCreated(new Date());
		visit.setUserId(idString);
		visit.setVisitUrl("api/{idString}/expr/new");
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
				Tag tag = new Tag();
				tag.setExprId(expr.getId());
				tag.setOwner(idString);
				tag.setContent("");
				tagRepository.save(tag);
				if (content.length() > 0) {
					tag = new Tag();
					tag.setExprId(expr.getId());
					tag.setOwner(idString);
					tag.setContent(content);
					tagRepository.save(tag);
				}
				return expr.eraseDangerousInfo();
			}
			else {
				return null;
			}
		}
		else {
			List<Tag> tags = tagRepository.findAllByOwnerAndContentAndExprId(idString, "", exprs.get(0).getId());
			if (tags.isEmpty()) {
				Tag tag = new Tag();
				tag.setExprId(exprs.get(0).getId());
				tag.setOwner(idString);
				tag.setContent("");
				tagRepository.save(tag);
			}
			tags = tagRepository.findAllByOwnerAndContentAndExprId(idString, content, exprs.get(0).getId());
			if (tags.isEmpty()) {
				Tag tag = new Tag();
				tag.setExprId(exprs.get(0).getId());
				tag.setOwner(idString);
				tag.setContent(content);
				tagRepository.save(tag);
			}
			return exprs.get(0).eraseDangerousInfo();
		}
	}
}
