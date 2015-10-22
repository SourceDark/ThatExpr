package com.ideasource.Model;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

public interface TagRepository extends JpaRepository<Tag, Long> {
	
	public List<Tag> findAllByOwnerAndContent(String owner, String content);

	public List<Tag> findAllByOwner(String owner);
	
	public List<Tag> findAllByContent(String content);
	
	public List<Tag> findAllByOwnerAndContentAndExprId(String owner, String content, Long ExprId);
}
