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
	xhr.open("post", "/api/" + idString + "/expr/new");
	
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