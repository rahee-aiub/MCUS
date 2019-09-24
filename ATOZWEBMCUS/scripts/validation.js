function isFireFox(){
    return navigator.appName == "Netscape";
}

function IsNumberKey(evt) {
	var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode > 31) && (charCode < 48 || charCode > 57) )
        return false;
    return true;
}

function IsNotNumberKey(evt) {
	var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode > 31) && (charCode < 48 || charCode > 57) )
        return true;
    return false;
}

function IsDecimalKey(evt, textBox) {
	var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode != 46 && charCode > 31) && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        var CellNo = textBox.value;
        if (charCode == 46 && (CellNo.indexOf(".") > -1))
            return false;
        return true;
    }
}

function IsDecimalKeyWithSign(evt, textBox) {
	var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode != 46 && charCode != 45 && charCode > 31) && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        var CellNo = textBox.value;
        if ((charCode == 46 && (CellNo.indexOf(".") > -1)) || (charCode == 45 && (CellNo.indexOf("-") > -1)))
            return false;
        return true;
    }
}

function isNumber(val){
if(isNaN(val)){return false;}
else {return true;}
} 

function ValidateCellNo(textBox) {
    var CellNo = textBox.value;
    if ((CellNo.indexOf("0") != 0) || (CellNo.indexOf("1") != 1)) {        
            alert('Invalid Mobile No.Mobile Number should start 01XXXXXXXXX');             
            textBox.focus();        
    }  
    else if (CellNo.length == 0) {
            alert('Invalid Mobile No.Mobile Number should be 11 digit(01XXXXXXXXX).');
            textBox.focus();    
        }  
    else if (CellNo.length != 11){
         alert('Invalid Mobile No.Mobile Number should be 11 digit(01XXXXXXXXX).');
            textBox.focus();  
    }    
}


function QuantityRange(evt, textBox, before, after) {
	var status = IsDecimalKey(evt, textBox);
	if(status) {
		var charCode = (evt.which) ? evt.which : event.keyCode;
		if (charCode == 46) {
			return true;
		}
		var txtValue = textBox.value;
		if(txtValue.indexOf(".") > -1){
			before = before - 1;
			after = after - 1;
			var aa = txtValue.split(".");
			var b = aa[0];
			var c = aa[1];
			if((b.length <= before) || (c.length <= after) && txtValue.length <= (before + after + 1))
				status = true;
		} else {
			 if((txtValue.length >= before) && (charCode != 8)) {
			 	return false;
			 }
		}
	}
	return status;
}

function NumericLenth(evt, textBox, maxLength) {
	var charCode = (evt.which) ? evt.which : event.keyCode;
		if (charCode == 8) {
			return true;
		}
	var status = IsNumberKey(evt);
	if (status) {
		var code = textBox.value;
		if(code.length >= maxLength){
				status = false;
			}
		} else {
			status = false;
		}	
	return status;
	}


 function NumericRange(evt,textBox,minimum,maximum) {
 	
 	var renge = textBox.value;
    	if ((renge < minimum) || (renge > maximum) ) {
    		alert("Please Input must be with in " + minimum + " to " + maximum + ".");
    		textBox.focus();
    		textBox.value = "";
    	} else {
    		return true;
    	}
 	return false;
    }

