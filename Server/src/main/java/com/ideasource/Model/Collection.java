package com.ideasource.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="tag")
public class Collection {
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;
	
	private Long exprId;
	
	private String owner;
	
	private String content;

	public Collection() {
	}
	
	public Collection(Collection collection) {
		this.id = collection.id;
		this.exprId = collection.exprId;
		this.owner = collection.owner;
		this.content = collection.content;
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public Long getExprId() {
		return exprId;
	}

	public void setExprId(Long exprId) {
		this.exprId = exprId;
	}

	public String getOwner() {
		return owner;
	}

	public void setOwner(String owner) {
		this.owner = owner;
	}
}
