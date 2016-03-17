package com.ideasource.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Expr {

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;

	private String md5;

	private String extension;

	private String creator;

	public Expr() {
		
	}
	
	public Expr(Expr expr) {
		this.id = expr.id;
		this.md5 = expr.md5;
		this.extension = expr.extension;
		this.creator = expr.creator;
	}
	
	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getMd5() {
		return md5;
	}

	public void setMd5(String md5) {
		this.md5 = md5;
	}

	public String getCreator() {
		return creator;
	}

	public void setCreator(String creator) {
		this.creator = creator;
	}

	public String getExtension() {
		return extension;
	}

	public void setExtension(String extension) {
		this.extension = extension;
	}

	public Expr eraseDangerousInfo() {
		this.creator = null;
		return this;
	}
}
