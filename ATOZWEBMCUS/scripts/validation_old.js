function isFireFox(){
    return navigator.appName == "Netscape";
}

function IsNumberKey(evt) {
	var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode > 31) && (charCode < 48 || charCode > 57) )
        return false;
    return true;
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
///juthi
//function ValidationRenge(textBox) {
//	var renge = textBox.value;
//	if(renge < 20)
//	{
//		alert('Please Input Salary Parcentage with in 20-100.');
//		textBox.focus();
//	} 
//	else if(renge > 100)
//	{
//    	alert('Please Input Salary Parcentage with in 20-100.');	
//    	textBox.focus();
//	}
//}

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

