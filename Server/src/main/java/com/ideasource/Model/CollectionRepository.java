package com.ideasource.Model;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

public interface CollectionRepository extends JpaRepository<Collection, Long> {
	
	public List<Collection> findAllByOwnerAndContent(String owner, String content);

	public List<Collection> findAllByOwner(String owner);
	
	public List<Collection> findAllByContent(String content);
	
	public List<Collection> findAllByOwnerAndContentAndExprId(String owner, String content, Long ExprId);

}
