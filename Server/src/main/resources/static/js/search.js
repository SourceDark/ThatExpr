var currentFile;
var idString;

if (window.FileReader) { 
	addEventHandler(window, 'load', function() {
		var status = document.getElementById('status');
		var drop   = document.getElementById('drop');
		var list   = document.getElementById('list');
		var display = document.getElementById('display');
		idString = document.getElementById('id').value;
  	
		function cancel(e) {
			if (e.preventDefault) { e.preventDefault(); }
			return false;
		}
  
		// Tells the browser that we *can* drop on this target
		addEventHandler(drop, 'dragover', cancel);
		addEventHandler(drop, 'dragenter', cancel);
		addEventHandler(drop, 'drop', function (e) {
			  e = e || window.event; // get window.event if e argument missing (in IE)   
			  if (e.preventDefault) { e.preventDefault(); } // stops the browser from redirecting off to the image.

			  var dt    = e.dataTransfer;
			  var files = dt.files;
			  for (var i=0; i<files.length; i++) {
			    var file = files[i];
			    var reader = new FileReader();
			      
			    //attach event handlers here...
			    Function.prototype.bindToEventHandler = function bindToEventHandler() {
			    	  var handler = this;
			    	  var boundParameters = Array.prototype.slice.call(arguments);
			    	  //create closure
			    	  return function(e) {
			    	      e = e || window.event; // get window.event if e argument missing (in IE)   
			    	      boundParameters.unshift(e);
			    	      handler.apply(this, boundParameters);
			    	  }
		    	};
			    addEventHandler(reader, 'loadend', function(e, file) {
			        var bin           = this.result; 
			        var newFile       = document.createElement('div');
			        newFile.innerHTML = 'Loaded : '+file.name+' size '+file.size+' B';
			        list.appendChild(newFile);  
			        var fileNumber = list.getElementsByTagName('div').length;
			        status.innerHTML = fileNumber < files.length 
			                         ? 'Loaded 100% of file '+fileNumber+' of '+files.length+'...' 
			                         : 'Done loading. processed '+fileNumber+' files.';

			        var img = document.createElement("img"); 
			        img.file = file;   
			        img.src = bin;
			        currentFile = file;
			        
			        list.appendChild(img);
			        display.innerHTML = "";
			        display.appendChild(img);
			    }.bindToEventHandler(file));
			   
			    reader.readAsDataURL(file);
			  }
			  return false;
			});
		document.getElementById('status').innerHTML = 'Ready to drop';
	});
} 
else { 
	document.getElementById('status').innerHTML = 'Your browser does not support the HTML5 FileReader.';
}

function uploadButtonOnClick(url) {
	if (!currentFile) {
		alert("还没有选中图片哟~");
		return;
	}
	var form = new FormData();
	form.append("expr", currentFile);
	form.append("tag", document.getElementById('tagInput').value);
	console.log(document.getElementById('tagInput').value);
	
	var xhr = new XMLHttpRequest();
	xhr.open("post", "/api/" + idString + "/expr/search");
	
	xhr.onreadystatechange = function() {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log(xhr.responseText);
        }
    }

    //侦查当前附件上传情况
    xhr.upload.onprogress = function(evt) {
        loaded = evt.loaded;
        tot = evt.total;
        per = Math.floor(100 * loaded / tot); //已经上传的百分比
        console.log(per);
    };

    xhr.send(form);
}

function uploadZip() {
	var filename = $('#zipName').val();
	if (filename.length < 1) {
		alert('no file');
		return ;
	}
	$.ajaxFileUpload({
		url : '/api/'+ idString+ '/uploadZip',
		secureuri : false,//安全协议
		fileElementId:'btnFile',//id
		type : 'POST',
		dataType : 'json',
		data: null,
		async : false,
		error : function(data,status,e) {
			alert(data.responseText);
		},
		success : function(json) {
			if (json.resultFlag==false){
				alert(json.resultMessage);
			}else{
				alert('文件上传成功!');
			}
		}
	});
}

window.onload=function() {
	function paste_img(e) {
		if ( e.clipboardData.items ) {
		// google-chrome 
			//alert('support clipboardData.items(chrome ...)');
			ele = e.clipboardData.items
			for (var i = 0; i < ele.length; ++i) {
				if ( ele[i].kind == 'file' && ele[i].type.indexOf('image/') !== -1 ) {
					var blob = ele[i].getAsFile();
					window.URL = window.URL || window.webkitURL;
					var blobUrl = window.URL.createObjectURL(blob);
					console.log(blobUrl);

					var new_img= document.createElement('img');
					new_img.setAttribute('src', blobUrl);
					//var new_img_intro = document.createElement('p');
					//new_img_intro.innerHTML = 'the pasted img url(open it in new tab): <br /><a target="_blank" href="' + blobUrl + '">' + blobUrl + '</a>';
					
					//var reader = new FileReader();
					 
					var display = document.getElementById('display');
					var list   = document.getElementById('list');
					var img = document.createElement("img"); 
			        img.file = blob;   
			        img.src = blobUrl;
			        currentFile = blob;
			        
			        list.appendChild(img);
			        display.innerHTML = "";
			        display.appendChild(img);
			        
					//document.getElementById('pasteBoard').appendChild(new_img);
					//document.getElementById('pasteBoard').appendChild(new_img_intro);
				}

			}
		} else {
			alert('non-chrome');
		}
	}
	document.getElementById('pasteBoard').onpaste=function(){paste_img(event);return false;};
}