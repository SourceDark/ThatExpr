
# 后端无状态API

## Expr

### [get] /api/{idString}/tags/abstract/all

Response:
	- 
	- <Sample> []

### [get] /api/{idString}/tags/abstract
Parameters:
	- [boolean] my: 筛选域：是否仅返回当前用户的标签
	- [string] content: 筛选域：标签的内容

Response:
	- 符合筛选要求的

### [get] /api/{idString}/tags/{tagsId}

### [get] /api/{idString}/tags/{tagContent}/my

### [get] /api/