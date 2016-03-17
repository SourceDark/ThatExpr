package com.ideasource.Model;

import java.util.ArrayList;
import java.util.List;

public class ExprDTO extends Expr {
	
	public ExprDTO(Expr expr) {
		super(expr);
		tags = new ArrayList<String>();
	}
	
	public List<String> tags;
}
