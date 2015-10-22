package com.ideasource.Model;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

public interface ExprRepository extends JpaRepository<Expr, Long> {
	
	public List<Expr> findAllByIdIn(List<Long> ids);
	
	public List<Expr> findAllByMd5(String Md5);
}
