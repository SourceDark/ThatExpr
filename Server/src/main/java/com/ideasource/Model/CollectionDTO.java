package com.ideasource.Model;

public class CollectionDTO extends Collection {

	private Expr expr;
	
	public CollectionDTO(Collection collection) {
		super(collection);
		
		expr = null;
	}

	public Expr getExpr() {
		return expr;
	}

	public void setExpr(Expr expr) {
		this.expr = expr;
	}

}
